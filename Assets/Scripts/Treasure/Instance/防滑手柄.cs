using UnityEngine;

public class 防滑手柄 : TreasureBase
{
    public override int ID { get; protected set; } = 1007;

    public override void OnBattleStart()
    {
        Debug.Log(treasureCfg.Description);
    }
}
