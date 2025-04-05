using UnityEngine;

public class 加固装置 : TreasureBase
{
    public override int ID { get; protected set; } = 1002;
    public int value = 1;
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        Debug.Log(treasureCfg.Description);
    }
}
