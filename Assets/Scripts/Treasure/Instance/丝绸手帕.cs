using UnityEngine;

public class 丝绸手帕 : TreasureBase
{
    public override int ID { get; protected set; } = 1008;

    public override void OnBattleStart()
    {
        base.OnBattleStart();
        Debug.Log(treasureCfg.Description);
    }
}
