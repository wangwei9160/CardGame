public class PlayerTurn : IFightState
{
    protected override string name => "PlayerTurn";

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.Show("PlayerTurnTip");
        GameManager.Instance.OnMagicPowerChange(1); // 进入玩家回合获得一点法力值
    }

}
