using UnityEngine;

public class WaitingForStartState : IBaseState
{
    public WaitingForStartState(BaseBattlePlayer parent) : base(parent) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaitingForStartState");
        EventCenter.AddListener(EventDefine.OnBattleStart , OnBattleStart);
        BattleManager.Instance.OnBattleStart();
    }

    public void OnBattleStart()
    {
        Parent.ChangeState(BattleEvent.BattleInitFinish);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }

}