using UnityEngine;

public class 绿色水晶 : TreasureBase
{
    public override int ID { get; protected set; } = 1005;
    public int value = 2;
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        Debug.Log(treasureCfg.Description);
    }
}
