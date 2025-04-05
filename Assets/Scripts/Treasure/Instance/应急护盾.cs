using UnityEngine;

public class 应急护盾 : TreasureBase
{
    public override int ID { get; protected set; } = 1003;
    public int value = 5;
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        Debug.Log(treasureCfg.Description);
    }
}
