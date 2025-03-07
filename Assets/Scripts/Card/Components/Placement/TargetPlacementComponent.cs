using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{

    /// 目标放置组件
    /// 处理需要选择目标的卡牌放置逻辑
    public class TargetPlacementComponent : CardPlacementComponent
    {

        /// 检查是否有有效的目标对象
        public override bool IsValidPlacement(PointerEventData eventData)
        {
            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                var target = result.gameObject.GetComponent<ICardTarget>();
                if (target != null && target.CanBeTargeted(card))
                {
                    return true;
                }
            }
            return false;
        }

        /// 执行目标放置操作
        public override void OnPlacement(PointerEventData eventData)
        {
            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            
            foreach (var result in results)
            {
                var target = result.gameObject.GetComponent<ICardTarget>();
                if (target != null && target.CanBeTargeted(card))
                {
                    target.OnCardTargeted(card);
                    break;
                }
            }
        }
    }
} 