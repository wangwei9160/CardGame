using System;
using System.Collections.Generic;

public class CardData
{
    public int id;                 // 卡牌ID
    public string cardName;        // 卡牌名称
    public string description;     // 卡牌描述
    public int manaCost;          // 法力值消耗
    public string cardType;        // 卡牌类型（Weapon/Accessory/Minion/Spell）
    public int attackValue;        // 攻击力（武器和随从）
    public int healthValue;        // 生命值（随从）
    public int durability;         // 耐久度（装备）
    public int bonusValue;         // 加成值（饰品）
    public string leftAttribute;   // 左属性
    public string rightAttribute;  // 右属性
    public string effects;         // 效果列表（用分号分隔）
}

public class CardDataManager
{
    public static Dictionary<int, CardData> m_Dic;

    static CardDataManager()
    {
        m_Dic = new Dictionary<int, CardData>();
    }

    public static CardData GetCardDataById(int id)
    {
        if(m_Dic.ContainsKey(id)) return m_Dic[id];
        return null;
    }

    public static void AddCardData(CardData data)
    {
    if(!m_Dic.ContainsKey(data.id))
        {
            m_Dic.Add(data.id, data);
        }
    }
}
