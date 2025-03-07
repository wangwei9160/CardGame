using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{

    /// 卡牌放置组件基类
    /// 定义卡牌放置的基本接口
    public abstract class CardPlacementComponent : CardComponent
    {
        /// 检查放置是否有效
        public abstract bool IsValidPlacement(PointerEventData eventData);

        /// 执行放置操作
        public abstract void OnPlacement(PointerEventData eventData);
    }
} 