using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{

    /// 自由放置组件
    /// 处理可以自由放置的卡牌放置逻辑
    public class FreePlacementComponent : CardPlacementComponent
    {
        private CardVisualComponent visualComponent;  // 视觉组件引用

        protected virtual void Awake()
        {
            visualComponent = GetComponent<CardVisualComponent>();
        }


        /// 检查是否在有效区域内
        public override bool IsValidPlacement(PointerEventData eventData)
        {
            // 可以添加区域限制逻辑
            return true;
        }


        /// 执行自由放置操作
        public override void OnPlacement(PointerEventData eventData)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                card.Canvas.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out pos))
            {
                visualComponent.SetPosition(pos);
            }
        }
    }
} 