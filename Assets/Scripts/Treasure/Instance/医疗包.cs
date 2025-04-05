using UnityEngine;

public class 医疗包 : TreasureBase
{
    public override int ID { get; protected set; } = 1006;

    public override void OnTurnStart()
    {
        Debug.Log(treasureCfg.Description);
    }
}
