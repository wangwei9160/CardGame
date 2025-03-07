using UnityEngine;

namespace CardSystem
{

    /// 卡牌基础组件类
    /// 所有卡牌相关组件的基类，提供基本的卡牌引用功能
    public class CardComponent : MonoBehaviour
    {
        protected Card card;  // 卡牌引用
        

        /// 初始化组件，设置卡牌引用
        /// <param name="card">关联的卡牌对象</param>
        public virtual void Initialize(Card card)
        {
            this.card = card;
        }
    }
} 