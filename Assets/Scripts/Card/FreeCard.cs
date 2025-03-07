using UnityEngine;

namespace CardSystem
{

    /// 自由放置卡牌类
    /// 可以自由放置在场景中的卡牌
    public class FreeCard : Card
    {
        protected override void Awake()
        {
            base.Awake();
            placementComponent = gameObject.AddComponent<FreePlacementComponent>();
        }
    }
} 