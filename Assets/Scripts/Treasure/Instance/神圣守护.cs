using UnityEngine;

public class 神圣守护 : TreasureBase
{
    public override int ID { get; protected set; } = 1001;

    public override void OnTurnStart()
    {
        base.OnTurnStart();
        Debug.Log(treasureCfg.Description);
    }
}
