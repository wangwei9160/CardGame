using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CardSystem
{

    /// 卡牌视觉组件
    /// 处理卡牌的视觉效果，包括高亮、透明度、缩放等
    public class CardVisualComponent : CardComponent
    {
        private Outline outline;           // 轮廓组件
        private CanvasGroup canvasGroup;   // 画布组组件
        private RectTransform _rectTransform;  // 矩形变换组件


        /// 获取矩形变换组件
        public RectTransform RectTransform => _rectTransform;

        protected virtual void Awake()
        {
            outline = GetComponent<Outline>();
            canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            outline.enabled = false;
        }


        /// 设置卡牌高亮状态
        public virtual void SetHighlight(bool highlight)
        {
            outline.enabled = highlight;
        }

        /// 设置卡牌透明度
        public virtual void SetAlpha(float alpha)
        {
            canvasGroup.alpha = alpha;
        }


        /// 设置是否阻挡射线检测
        public virtual void SetBlocksRaycasts(bool blocks)
        {
            canvasGroup.blocksRaycasts = blocks;
        }


        /// 设置卡牌父物体
        public virtual void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }


        /// 设置卡牌位置
        public virtual void SetPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }


        /// 动画缩放卡牌
        public virtual void AnimateScale(Vector3 targetScale, float duration)
        {
            transform.DOScale(targetScale, duration);
        }


        /// 动画移动卡牌位置
        public virtual void AnimatePosition(Vector2 targetPosition, float duration)
        {
            _rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.OutQuad);
        }
    }
} 