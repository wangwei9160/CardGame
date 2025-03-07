using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{

    /// 卡牌拖拽组件
    /// 处理卡牌的拖拽相关功能
    public class CardDragComponent : CardComponent, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector2 originalPosition;    // 原始位置
        private bool isDragging;            // 是否正在拖拽
        private CardVisualComponent visualComponent;  // 视觉组件引用

        protected virtual void Awake()
        {
            visualComponent = GetComponent<CardVisualComponent>();
        }


        /// 开始拖拽时的处理
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            originalPosition = visualComponent.RectTransform.anchoredPosition;
            
            visualComponent.SetAlpha(0.6f);
            visualComponent.SetBlocksRaycasts(false);
            visualComponent.SetParent(card.Canvas.transform);
        }

        /// <summary>
        /// 拖拽过程中的处理
        /// </summary>
        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;

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

        /// <summary>
        /// 结束拖拽时的处理
        /// </summary>
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            visualComponent.SetAlpha(1f);
            visualComponent.SetBlocksRaycasts(true);

            if (card.IsValidPlacement(eventData))
            {
                card.OnCardPlaced(eventData);
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }

        /// <summary>
        /// 返回原始位置
        /// </summary>
        protected virtual void ReturnToOriginalPosition()
        {
            visualComponent.AnimatePosition(originalPosition, 0.3f);
        }
    }
} 