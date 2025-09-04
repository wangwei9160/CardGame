using UnityEngine;
public class PlayerTurnState : IBaseState
{
    public PlayerTurnState(BaseBattlePlayer parent) : base(parent) { }

    public override void OnEnter()
    {
        Debug.Log("PlayerTurnState Enter");
        EventCenter.Broadcast(EventDefine.OnBeforePlayerTurn); // 玩家回合检测
        EventCenter.Broadcast(EventDefine.OnPlayerTurnStart); // 进入玩家可操作回合事件广播
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }

}