using UnityEngine;

public class 自转沙漏 : TreasureBase
{
    public override int ID { get; protected set; } = 2014;

    // 所有（包括敌人的）回合开始时效果和回合结束时效果多触发一次。
}
// 自转沙漏