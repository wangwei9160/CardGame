using System;
using System.Collections.Generic;

[Serializable]
public class CardClass
{
    public int id;
    public string name;
    public string icon;
    public int type;
    public int cost;
    public int leftAttribute;
    public int rightAttribute;
    public int blade;
    public int magic;
    public int defence;
    public int glass;
    public int vatality;
    public List<List<int>> active_id;
    public List<List<int>> passive_id;
}

public class CardConfig
{
    public static Dictionary<int, CardClass> m_Dic;

    static CardConfig()
    {
        m_Dic = new Dictionary<int, CardClass>()
        {
            {100001, new CardClass() { id = 100001, name = "铜剑", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 2, magic = 0, defence = 0, glass = 0, vatality = 1, active_id = new List<List<int>>() { new List<int>() { 1,1,1 } }, passive_id = new List<List<int>>() {  } } },
            {100002, new CardClass() { id = 100002, name = "修元", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 3, active_id = new List<List<int>>() { new List<int>() { 1,1,1 },new List<int>() { 2,4,1 },new List<int>() { 6,1 } }, passive_id = new List<List<int>>() {  } } },
            {100003, new CardClass() { id = 100003, name = "东风", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 2, blade = 3, magic = 3, defence = 0, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,1,2 } }, passive_id = new List<List<int>>() {  } } },
            {100004, new CardClass() { id = 100004, name = "希声", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 3, blade = 8, magic = 0, defence = 5, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,0,1 } }, passive_id = new List<List<int>>() {  } } },
            {200001, new CardClass() { id = 200001, name = "银戒指", icon = "", type = 2, cost = 1, leftAttribute = 3, rightAttribute = 5, blade = 0, magic = 0, defence = 5, glass = 0, vatality = 5, active_id = new List<List<int>>() { new List<int>() { 2,1,1 },new List<int>() { 7,-2,-2 } }, passive_id = new List<List<int>>() {  } } },
            {200002, new CardClass() { id = 200002, name = "红水晶项链", icon = "", type = 2, cost = 1, leftAttribute = 1, rightAttribute = 2, blade = 5, magic = 0, defence = 1, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,4,1 },new List<int>() { 6,3 },new List<int>() { 7,2,2 } }, passive_id = new List<List<int>>() {  } } },
            {200003, new CardClass() { id = 200003, name = "发簪", icon = "", type = 2, cost = 1, leftAttribute = 1, rightAttribute = 2, blade = 5, magic = 5, defence = 0, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,1,1 },new List<int>() { 7,-2,-2 } }, passive_id = new List<List<int>>() {  } } },
            {300002, new CardClass() { id = 300002, name = "狄", icon = "", type = 3, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 10, active_id = new List<List<int>>() {  }, passive_id = new List<List<int>>() { new List<int>() { 1 } } } },
            {300003, new CardClass() { id = 300003, name = "飞剑", icon = "", type = 3, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 2, active_id = new List<List<int>>() {  }, passive_id = new List<List<int>>() { new List<int>() { 1 } } } },
            {300004, new CardClass() { id = 300004, name = "水晶护盾", icon = "", type = 3, cost = 1, leftAttribute = 4, rightAttribute = 5, blade = 0, magic = 0, defence = 0, glass = 1, vatality = 5, active_id = new List<List<int>>() {  }, passive_id = new List<List<int>>() {  } } },
            {300005, new CardClass() { id = 300005, name = "漂浮碎石", icon = "", type = 3, cost = 2, leftAttribute = 1, rightAttribute = 4, blade = 3, magic = 0, defence = 0, glass = 1, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,0,1 } }, passive_id = new List<List<int>>() { new List<int>() { 2 } } } },
            {300006, new CardClass() { id = 300006, name = "滚动巨石", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 4, magic = 0, defence = 0, glass = 0, vatality = 8, active_id = new List<List<int>>() { new List<int>() { 1,0,1 } }, passive_id = new List<List<int>>() { new List<int>() { 2,3 } } } },
            {300007, new CardClass() { id = 300007, name = "森林狼", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 6, active_id = new List<List<int>>() { new List<int>() { 14 } }, passive_id = new List<List<int>>() { new List<int>() { 1,5 } } } },
            {300008, new CardClass() { id = 300008, name = "幼狼", icon = "", type = 3, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 1, magic = 0, defence = 0, glass = 0, vatality = 2, active_id = new List<List<int>>() {  }, passive_id = new List<List<int>>() { new List<int>() { 1 },new List<int>() { 7,3,3 } } } },
            {400001, new CardClass() { id = 400001, name = "直击", icon = "", type = 4, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 1, active_id = new List<List<int>>() { new List<int>() { 1,1,1 } }, passive_id = new List<List<int>>() {  } } },
            {400002, new CardClass() { id = 400002, name = "附魔打击", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 2, blade = 3, magic = 3, defence = 0, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 11,1 } }, passive_id = new List<List<int>>() {  } } },
            {400003, new CardClass() { id = 400003, name = "转移刺", icon = "", type = 4, cost = 2, leftAttribute = 2, rightAttribute = 3, blade = 0, magic = 3, defence = 1, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,1,1 },new List<int>() { 5,2 } }, passive_id = new List<List<int>>() {  } } },
            {400004, new CardClass() { id = 400004, name = "横劈", icon = "", type = 4, cost = 3, leftAttribute = 1, rightAttribute = 3, blade = 10, magic = 0, defence = 2, glass = 0, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 1,0,1 } }, passive_id = new List<List<int>>() {  } } },
            {400005, new CardClass() { id = 400005, name = "恢复", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 4, blade = 5, magic = 0, defence = 0, glass = 1, vatality = 0, active_id = new List<List<int>>() { new List<int>() { 2,1,1 } }, passive_id = new List<List<int>>() {  } } },
            {400006, new CardClass() { id = 400006, name = "基本功", icon = "", type = 4, cost = 4, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 10, defence = 0, glass = 0, vatality = 8, active_id = new List<List<int>>() { new List<int>() { 1,1,1 },new List<int>() { 2,4,1 },new List<int>() { 5,1 } }, passive_id = new List<List<int>>() {  } } },
        };
    }
    public static CardClass GetCardClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<CardClass> GetAll()
    {
        List<CardClass> ret = new List<CardClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
