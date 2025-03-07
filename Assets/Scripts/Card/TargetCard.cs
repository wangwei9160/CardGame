using UnityEngine;

namespace CardSystem
{

    /// 目标卡牌类
    /// 需要选择目标才能使用的卡牌
    public class TargetCard : Card
    {
        protected override void Awake()
        {
            base.Awake();
            placementComponent = gameObject.AddComponent<TargetPlacementComponent>();
        }
    }
} 