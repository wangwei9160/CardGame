using UnityEngine;

public class 细长法杖 : TreasureBase
{
    public override int ID { get; protected set; } = 1012;

    // 你的法术牌获得+x/+x（x等于友方战场上剩余的空位）。
}

// 细长法杖