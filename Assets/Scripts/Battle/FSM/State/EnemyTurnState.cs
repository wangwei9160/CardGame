using UnityEngine;

public class EnemyTurnState : IBaseState
{
    public EnemyTurnState(BaseBattlePlayer parent) : base(parent) { }

    public override void OnEnter()
    {
        Debug.Log("EnemyTurnState Enter");
        Parent.OnEnemyTurnStart();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        BattleEventCenter.Broadcast(BattleEventDefine.BigRoundEnd);
    }

}