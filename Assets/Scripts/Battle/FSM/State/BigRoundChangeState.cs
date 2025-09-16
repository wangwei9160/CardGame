
using UnityEngine;

public class BigRoundChangeState : IBaseState
{
    public BigRoundChangeState(BaseBattlePlayer parent) : base(parent) { }

    public override void OnEnter()
    {
        // 大回合开始，回合切换的检测，检测完发送切换到玩家回合
        Debug.Log("BigRoundChangeState Enter");
        Parent.PerformModule.AddPerformAction(new PerformAction
        {
            actionType = PerformActionType.UIAction,
            name = "PlayerTurnTip",
            OnAnimationEnd = () => {
                UIManager.Instance.Show("PlayerTurnTip");   // 新的回合
            }
        });
        // todo : 逻辑和表现最终需要分离
        BattleManager.Instance.BaseBattlePlayer.EventCenter.Broadcast(BattleEventDefine.BigRoundStart); 
        Parent.ChangeState(BattleEvent.BigRoundCheckFinish);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }

}