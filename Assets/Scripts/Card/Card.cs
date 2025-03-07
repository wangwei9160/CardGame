using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    /// 卡牌基类
    /// 所有卡牌类型的基类，管理卡牌的所有组件
    public abstract class Card : MonoBehaviour
    {
        [SerializeField] protected bool enableHoverEffect = true;  // 是否启用悬停效果
        public bool EnableHoverEffect => enableHoverEffect;

        protected CardVisualComponent visualComponent;      // 视觉组件
        protected CardDragComponent dragComponent;         // 拖拽组件
        protected CardHoverComponent hoverComponent;       // 悬停组件
        protected CardPlacementComponent placementComponent;  // 放置组件
        protected Canvas canvas;                           // 画布引用

        protected virtual void Awake()
        {
            InitializeComponents();
        }

        /// 初始化所有组件
        protected virtual void InitializeComponents()
        {
            visualComponent = GetComponent<CardVisualComponent>();
            dragComponent = GetComponent<CardDragComponent>();
            hoverComponent = GetComponent<CardHoverComponent>();
            placementComponent = GetComponent<CardPlacementComponent>();
            canvas = GetComponentInParent<Canvas>();

            // 初始化所有组件
            visualComponent?.Initialize(this);
            dragComponent?.Initialize(this);
            hoverComponent?.Initialize(this);
            placementComponent?.Initialize(this);
        }


        /// 检查放置是否有效
        public virtual bool IsValidPlacement(PointerEventData eventData)
        {
            return placementComponent != null && placementComponent.IsValidPlacement(eventData);
        }


        /// 执行卡牌放置
        public virtual void OnCardPlaced(PointerEventData eventData)
        {
            placementComponent?.OnPlacement(eventData);
        }


        /// 获取画布引用
        public Canvas Canvas => canvas;
    }
} 