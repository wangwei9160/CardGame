public class PlayerTurn : IFightState
{
    protected override string name => "PlayerTurn";

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.Show("PlayerTurnTip");
        GameManager.Instance.OnMagicPowerChange(1); // ������һغϻ��һ�㷨��ֵ
    }

}
