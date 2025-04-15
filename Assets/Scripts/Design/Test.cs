using System;
using System.Collections.Generic;

[Serializable]
public class TestClass
{
    public int id;
    public string cardName;
    public string description;
    public int manaCost;
    public string cardType;
    public int attackValue;
    public int healthValue;
    public int durability;
    public int bonusValue;
    public string leftAttribute;
    public string rightAttribute;
    public string effects;
}

public class TestManager
{
    public static Dictionary<int, TestClass> m_Dic;

    static TestManager()
    {
        m_Dic = new Dictionary<int, TestClass>()
        {
            {1, new TestClass() { id = 1, cardName = "铁剑", description = "基础武器", manaCost = 2, cardType = "weapon", attackValue = 3, healthValue = 0, durability = 5, bonusValue = 0, leftAttribute = "None", rightAttribute = "None", effects = "无" } },
            {2, new TestClass() { id = 2, cardName = "护盾", description = "提供防御", manaCost = 1, cardType = "Accessory", attackValue = 0, healthValue = 0, durability = 3, bonusValue = 2, leftAttribute = "Defense", rightAttribute = "None", effects = "提供2点防御" } },
            {3, new TestClass() { id = 3, cardName = "战士", description = "基础随从", manaCost = 3, cardType = "Minion", attackValue = 2, healthValue = 3, durability = 0, bonusValue = 0, leftAttribute = "Vitality", rightAttribute = "None", effects = "无" } },
            {4, new TestClass() { id = 4, cardName = "火球术", description = "造成伤害", manaCost = 2, cardType = "Spell", attackValue = 0, healthValue = 0, durability = 0, bonusValue = 0, leftAttribute = "None", rightAttribute = "None", effects = "造成3点伤害" } },
        };
    }
    public static TestClass GetTestClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
