using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{

    /// 卡牌悬停组件
    /// 处理卡牌的悬停效果
    public class CardHoverComponent : CardComponent, IPointerEnterHandler, IPointerExitHandler
    {
        private CardVisualComponent visualComponent;  // 视觉组件引用
        private Vector3 originalScale;                // 原始缩放值

        protected virtual void Awake()
        {
            visualComponent = GetComponent<CardVisualComponent>();
            originalScale = transform.localScale;
        }


        /// 鼠标进入时的处理
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!card.EnableHoverEffect) return;
            visualComponent.AnimateScale(originalScale * 1.5f, 0.25f);
            visualComponent.SetHighlight(true);
        }


        /// 鼠标离开时的处理
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!card.EnableHoverEffect) return;
            visualComponent.AnimateScale(originalScale, 0.25f);
            visualComponent.SetHighlight(false);
        }
    }
} 