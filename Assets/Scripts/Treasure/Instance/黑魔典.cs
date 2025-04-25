using UnityEngine;

public class 黑魔典 : TreasureBase
{
    public override int ID { get; protected set; } = 2009;

    // 战斗开始时,巫真的剩余活力变为1,将损失的活力转化为2倍的护盾。
}
// 黑魔典