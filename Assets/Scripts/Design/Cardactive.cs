using System;
using System.Collections.Generic;

[Serializable]
public class CardActiveClass
{
    public int active_id;
    public string active_content;
}

public class CardActiveConfig
{
    public static Dictionary<int, CardActiveClass> m_Dic;

    static CardActiveConfig()
    {
        m_Dic = new Dictionary<int, CardActiveClass>()
        {
            {1, new CardActiveClass() { active_id = 1, active_content = "攻击{0}{1}。" } },
            {2, new CardActiveClass() { active_id = 2, active_content = "治疗{0}{1}。" } },
            {3, new CardActiveClass() { active_id = 3, active_content = "增益{0}{1}。" } },
            {4, new CardActiveClass() { active_id = 4, active_content = "减益{0}{1}。" } },
            {5, new CardActiveClass() { active_id = 5, active_content = "抽{0}张牌。" } },
            {6, new CardActiveClass() { active_id = 6, active_content = "获得{0}点法力值。" } },
            {7, new CardActiveClass() { active_id = 7, active_content = "使自身{0}/{1}。" } },
            {8, new CardActiveClass() { active_id = 8, active_content = "自身每有1剑，抽1张牌。" } },
            {9, new CardActiveClass() { active_id = 9, active_content = "选择一个友方随从，使其进入墓地，然后自身攻击所有敌人。" } },
            {10, new CardActiveClass() { active_id = 10, active_content = "在战场最前方召唤一个与自身属性相同的“香气”。" } },
            {11, new CardActiveClass() { active_id = 11, active_content = "使下一张法术牌的消耗减少1。" } },
            {12, new CardActiveClass() { active_id = 12, active_content = "将自身放回手牌中。" } },
            {13, new CardActiveClass() { active_id = 13, active_content = "如果自身的活力大于0，则从墓地中选择一张随从牌，将其复活至场上指定位置。" } },
            {14, new CardActiveClass() { active_id = 14, active_content = "在自身前方召唤另一只森林狼。" } },
        };
    }
    public static CardActiveClass GetCardActiveClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<CardActiveClass> GetAll()
    {
        List<CardActiveClass> ret = new List<CardActiveClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
