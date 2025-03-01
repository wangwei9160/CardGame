public class PlayerTurn : IFightState
{
    protected override string name => "PlayerTurn";

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.Show("PlayerTurnTip");
        UIManager.Instance.Show("");
    }

}
