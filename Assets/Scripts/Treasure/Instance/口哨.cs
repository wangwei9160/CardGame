using UnityEngine;

public class 口哨 : TreasureBase
{
    public override int ID { get; protected set; } = 1009;

    public override void OnBattleStart()
    {
        Debug.Log(treasureCfg.Description);
    }
}
