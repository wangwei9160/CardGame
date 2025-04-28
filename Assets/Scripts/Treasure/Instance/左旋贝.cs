using UnityEngine;

public class 左旋贝 : TreasureBase
{
    public override int ID { get; protected set; } = 2017;

    // 友方的所有回合开始时效果多触发一次,但回合结束时效果不再生效。
}
// 左旋贝