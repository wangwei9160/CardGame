using UnityEngine;

public class EnemyTurnState : IBaseState
{
    public EnemyTurnState(BaseBattlePlayer parent) : base(parent) { }

    public override void OnEnter()
    {
        Debug.Log("EnemyTurnState Enter");

        Parent.ChangeState(BattleEvent.FinishEnemyTurn);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }

}