using UnityEngine;

public class 含羞草 : TreasureBase
{
    public override int ID { get; protected set; } = 1014;

    // 巫真的防御-3,但巫真每次受到攻击都会获得1防御。

    public override void OnBattleStart()
    {
        Debug.Log(treasureCfg.Description);
    }

    public override void OnBeforeHurt()
    {
        Debug.Log(treasureCfg.Description);
    }
}
// 含羞草