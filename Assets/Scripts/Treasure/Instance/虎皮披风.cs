using UnityEngine;

public class 虎皮披风 : TreasureBase
{
    public override int ID { get; protected set; } = 1010;

    public override string Description(bool isOnlyShow)
    {
        int val = BattleManager.Instance.getPlayerTeamNum();
        return string.Format("{0}（现在是：{1}）", treasureCfg.Description , val);
    }

    // 手牌中的随从获得+x/+x（x等于友方战场上剩余的空位）。
}
