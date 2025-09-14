using System;
using System.Collections.Generic;

[Serializable]
public class CardPassiveClass
{
    public int passive_id;
    public string passive_content;
    public int passive_trigger;
}

public class CardPassiveConfig
{
    public static Dictionary<int, CardPassiveClass> m_Dic;

    static CardPassiveConfig()
    {
        m_Dic = new Dictionary<int, CardPassiveClass>()
        {
            {1, new CardPassiveClass() { passive_id = 1, passive_content = "攻击最前方的敌人。", passive_trigger = 2 } },
            {2, new CardPassiveClass() { passive_id = 2, passive_content = "攻击所有敌人。", passive_trigger = 2 } },
            {3, new CardPassiveClass() { passive_id = 3, passive_content = "召唤一个漂浮碎石。", passive_trigger = 4 } },
            {4, new CardPassiveClass() { passive_id = 4, passive_content = "召唤一个滚动巨石。", passive_trigger = 4 } },
            {5, new CardPassiveClass() { passive_id = 5, passive_content = "场上的其他森林狼与幼狼攻击相同目标一次。", passive_trigger = 5 } },
            {6, new CardPassiveClass() { passive_id = 6, passive_content = "自身获得3活力治疗。", passive_trigger = 2 } },
            {7, new CardPassiveClass() { passive_id = 7, passive_content = "自身获得{0}/{1}。", passive_trigger = 2 } },
            {8, new CardPassiveClass() { passive_id = 8, passive_content = "召唤{0}", passive_trigger = 2 } },
        };
    }
    public static CardPassiveClass GetCardPassiveClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<CardPassiveClass> GetAll()
    {
        List<CardPassiveClass> ret = new List<CardPassiveClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
