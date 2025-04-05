using UnityEngine;

public class 金树叶 : TreasureBase
{
    public override int ID { get; protected set; } = 1020;

    public override void OnEnemyDeath()
    {
        print();
    }

}
// 金树叶