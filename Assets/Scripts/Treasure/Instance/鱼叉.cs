using UnityEngine;

public class 鱼叉 : TreasureBase
{
    public override int ID { get; protected set; } = 1017;

    // 每当一名敌人受到攻击时,一名随机的其他敌人受到1剑攻击。
    public override void OnAfterDamage()
    {
        Debug.Log(treasureCfg.Description);
    }
}
// 鱼叉