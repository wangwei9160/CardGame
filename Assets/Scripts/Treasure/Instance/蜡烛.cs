using UnityEngine;

public class 蜡烛 : TreasureBase
{
    public override int ID { get; protected set; } = 1018;

    // 每场战斗中,第一个死亡的友方随从将回到牌堆中。
    public override void OnPlayerDeath()
    {
        Debug.Log(treasureCfg.Description);
    }
}
// 蜡烛