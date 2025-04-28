using UnityEngine;

public class 金元宝 : TreasureBase
{
    public override int ID { get; protected set; } = 1015;

    // 获得此灵物时,获得100金币。
    public override void OnGet()
    {
        Debug.Log(treasureCfg.Description);
    }
}
// 金元宝