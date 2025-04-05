using UnityEngine;

public class 皮手套 : TreasureBase
{
    public override int ID { get; protected set; } = 1004;
    public int value = 2;
    public override void OnTurnStart()
    {
        Debug.Log(treasureCfg.Description);
    }
}
