namespace CardSystem
{

    /// 可交互目标接口
    /// 定义可以被卡牌选中的目标对象接口
    public interface ICardTarget
    {
 
        /// 检查是否可以被指定卡牌选中
        bool CanBeTargeted(Card card);


        /// 被卡牌选中时的处理
        void OnCardTargeted(Card card);
    }
} 