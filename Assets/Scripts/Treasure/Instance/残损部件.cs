using UnityEngine;

public class 残损部件 : TreasureBase
{
    public override int ID { get; protected set; } = 1016;

    // 获得此灵物时，可获得一次卡牌配件奖励。
    public override void OnGet()
    {
        Debug.Log(treasureCfg.Description);
    }
}
// 残损部件