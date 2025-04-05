using UnityEngine;

public class 望远镜 : TreasureBase
{
    public override int ID { get; protected set; } = 1011;

    public override void OnBeforeDamage()
    {
        Debug.Log(treasureCfg.Description);
    }
}
