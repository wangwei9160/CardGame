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
    public List<int> active_id;
    public List<int> passive_id;
}

public class CardConfig
{
    public static Dictionary<int, CardClass> m_Dic;

    static CardConfig()
    {
        m_Dic = new Dictionary<int, CardClass>()
        {
            {100001, new CardClass() { id = 100001, name = "铜剑", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 2, magic = 0, defence = 0, glass = 0, vatality = 1, active_id = new List<int>() { 1 }, passive_id = new List<int>() {  } } },
            {100002, new CardClass() { id = 100002, name = "修元", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 3, active_id = new List<int>() { 1,8,18 }, passive_id = new List<int>() {  } } },
            {100003, new CardClass() { id = 100003, name = "东风", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 2, blade = 3, magic = 3, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 2 }, passive_id = new List<int>() {  } } },
            {100004, new CardClass() { id = 100004, name = "希声", icon = "", type = 1, cost = 2, leftAttribute = 1, rightAttribute = 3, blade = 8, magic = 0, defence = 5, glass = 0, vatality = 0, active_id = new List<int>() { 3 }, passive_id = new List<int>() {  } } },
            {200001, new CardClass() { id = 200001, name = "银戒指", icon = "", type = 2, cost = 1, leftAttribute = 3, rightAttribute = 5, blade = 0, magic = 0, defence = 5, glass = 0, vatality = 5, active_id = new List<int>() { 8,20 }, passive_id = new List<int>() {  } } },
            {200002, new CardClass() { id = 200002, name = "红水晶项链", icon = "", type = 2, cost = 1, leftAttribute = 1, rightAttribute = 2, blade = 5, magic = 0, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 4,19,21 }, passive_id = new List<int>() {  } } },
            {200003, new CardClass() { id = 200003, name = "发簪", icon = "", type = 2, cost = 1, leftAttribute = 1, rightAttribute = 2, blade = 5, magic = 5, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 1,20 }, passive_id = new List<int>() {  } } },
            {200004, new CardClass() { id = 200004, name = "香囊", icon = "", type = 2, cost = 1, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 4, defence = 0, glass = 0, vatality = 4, active_id = new List<int>() { 25,20 }, passive_id = new List<int>() {  } } },
            {300001, new CardClass() { id = 300001, name = "香气", icon = "", type = 3, cost = 0, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 0, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() { 1 } } },
            {200005, new CardClass() { id = 200005, name = "绳结", icon = "", type = 2, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 1, active_id = new List<int>() { 26,20 }, passive_id = new List<int>() {  } } },
            {200006, new CardClass() { id = 200006, name = "护符", icon = "", type = 2, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 0, magic = 0, defence = 0, glass = 0, vatality = 2, active_id = new List<int>() { 27,22 }, passive_id = new List<int>() {  } } },
            {300002, new CardClass() { id = 300002, name = "狄", icon = "", type = 3, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 10, active_id = new List<int>() {  }, passive_id = new List<int>() { 1 } } },
            {300003, new CardClass() { id = 300003, name = "飞剑", icon = "", type = 3, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 2, active_id = new List<int>() {  }, passive_id = new List<int>() { 1 } } },
            {300004, new CardClass() { id = 300004, name = "水晶护盾", icon = "", type = 3, cost = 1, leftAttribute = 4, rightAttribute = 5, blade = 0, magic = 0, defence = 0, glass = 1, vatality = 5, active_id = new List<int>() {  }, passive_id = new List<int>() {  } } },
            {300005, new CardClass() { id = 300005, name = "漂浮碎石", icon = "", type = 3, cost = 2, leftAttribute = 1, rightAttribute = 4, blade = 3, magic = 0, defence = 0, glass = 1, vatality = 0, active_id = new List<int>() { 3 }, passive_id = new List<int>() { 2 } } },
            {300006, new CardClass() { id = 300006, name = "滚动巨石", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 4, magic = 0, defence = 0, glass = 0, vatality = 8, active_id = new List<int>() { 3 }, passive_id = new List<int>() { 2,3 } } },
            {300007, new CardClass() { id = 300007, name = "森林狼", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 6, active_id = new List<int>() { 28 }, passive_id = new List<int>() { 2,5 } } },
            {300008, new CardClass() { id = 300008, name = "幼狼", icon = "", type = 3, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 1, magic = 0, defence = 0, glass = 0, vatality = 2, active_id = new List<int>() {  }, passive_id = new List<int>() { 1,7 } } },
            {300009, new CardClass() { id = 300009, name = "壮硕狗熊", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 4, magic = 0, defence = 0, glass = 0, vatality = 10, active_id = new List<int>() {  }, passive_id = new List<int>() { 1,6 } } },
            {300010, new CardClass() { id = 300010, name = "石头人", icon = "", type = 3, cost = 5, leftAttribute = 1, rightAttribute = 4, blade = 10, magic = 0, defence = 0, glass = 3, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() { 2,4 } } },
            {300011, new CardClass() { id = 300011, name = "花妖", icon = "", type = 3, cost = 1, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 1, defence = 0, glass = 0, vatality = 3, active_id = new List<int>() {  }, passive_id = new List<int>() { 8,9 } } },
            {300012, new CardClass() { id = 300012, name = "蔓灵", icon = "", type = 3, cost = 2, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 1, defence = 0, glass = 0, vatality = 3, active_id = new List<int>() { 17 }, passive_id = new List<int>() { 10,9 } } },
            {300013, new CardClass() { id = 300013, name = "行走树苗", icon = "", type = 3, cost = 1, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 20, defence = 0, glass = 0, vatality = 3, active_id = new List<int>() {  }, passive_id = new List<int>() { 11 } } },
            {300014, new CardClass() { id = 300014, name = "荆棘木灵", icon = "", type = 3, cost = 4, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 30, active_id = new List<int>() {  }, passive_id = new List<int>() { 12 } } },
            {300015, new CardClass() { id = 300015, name = "剑阵", icon = "", type = 3, cost = 5, leftAttribute = 1, rightAttribute = 4, blade = 5, magic = 0, defence = 0, glass = 3, vatality = 0, active_id = new List<int>() { 6 }, passive_id = new List<int>() { 13,14 } } },
            {300016, new CardClass() { id = 300016, name = "固定盔甲", icon = "", type = 3, cost = 3, leftAttribute = 3, rightAttribute = 4, blade = 0, magic = 0, defence = 5, glass = 5, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() {  } } },
            {300017, new CardClass() { id = 300017, name = "魔纹虎", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 10, magic = 0, defence = 0, glass = 0, vatality = 10, active_id = new List<int>() { 29 }, passive_id = new List<int>() { 1 } } },
            {300018, new CardClass() { id = 300018, name = "魔纹狮", icon = "", type = 3, cost = 5, leftAttribute = 1, rightAttribute = 5, blade = 10, magic = 0, defence = 0, glass = 0, vatality = 10, active_id = new List<int>() { 37,15 }, passive_id = new List<int>() { 1 } } },
            {300019, new CardClass() { id = 300019, name = "巨大宝箱", icon = "", type = 3, cost = 3, leftAttribute = 3, rightAttribute = 5, blade = 0, magic = 0, defence = 1, glass = 0, vatality = 3, active_id = new List<int>() {  }, passive_id = new List<int>() { 15,16 } } },
            {300020, new CardClass() { id = 300020, name = "魔法阵", icon = "", type = 3, cost = 2, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 5, defence = 0, glass = 0, vatality = 1, active_id = new List<int>() {  }, passive_id = new List<int>() { 17,18 } } },
            {300021, new CardClass() { id = 300021, name = "战旗", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 5, magic = 0, defence = 0, glass = 0, vatality = 5, active_id = new List<int>() { 37,9 }, passive_id = new List<int>() { 19,20 } } },
            {300022, new CardClass() { id = 300022, name = "熔炉", icon = "", type = 3, cost = 3, leftAttribute = 3, rightAttribute = 4, blade = 0, magic = 0, defence = 3, glass = 3, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() { 21 } } },
            {300023, new CardClass() { id = 300023, name = "战鼓", icon = "", type = 3, cost = 3, leftAttribute = 1, rightAttribute = 5, blade = 5, magic = 0, defence = 0, glass = 0, vatality = 5, active_id = new List<int>() { 15 }, passive_id = new List<int>() { 22 } } },
            {300024, new CardClass() { id = 300024, name = "灵气培育装置", icon = "", type = 3, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 1, magic = 0, defence = 0, glass = 0, vatality = 1, active_id = new List<int>() {  }, passive_id = new List<int>() { 27,23 } } },
            {300025, new CardClass() { id = 300025, name = "灵气防御炮台", icon = "", type = 3, cost = 3, leftAttribute = 2, rightAttribute = 4, blade = 0, magic = 8, defence = 0, glass = 3, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() { 24 } } },
            {300026, new CardClass() { id = 300026, name = "灵气自走战甲", icon = "", type = 3, cost = 5, leftAttribute = 1, rightAttribute = 4, blade = 5, magic = 0, defence = 0, glass = 5, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() { 25 } } },
            {300027, new CardClass() { id = 300027, name = "灵气吸取魔像", icon = "", type = 3, cost = 2, leftAttribute = 2, rightAttribute = 4, blade = 0, magic = 5, defence = 0, glass = 1, vatality = 0, active_id = new List<int>() {  }, passive_id = new List<int>() { 1,26 } } },
            {400001, new CardClass() { id = 400001, name = "直击", icon = "", type = 4, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 3, magic = 0, defence = 0, glass = 0, vatality = 1, active_id = new List<int>() { 1 }, passive_id = new List<int>() {  } } },
            {400002, new CardClass() { id = 400002, name = "附魔打击", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 2, blade = 3, magic = 3, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 1 }, passive_id = new List<int>() {  } } },
            {400003, new CardClass() { id = 400003, name = "转移刺", icon = "", type = 4, cost = 2, leftAttribute = 2, rightAttribute = 3, blade = 0, magic = 3, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 1,17 }, passive_id = new List<int>() {  } } },
            {400004, new CardClass() { id = 400004, name = "横劈", icon = "", type = 4, cost = 3, leftAttribute = 1, rightAttribute = 3, blade = 10, magic = 0, defence = 2, glass = 0, vatality = 0, active_id = new List<int>() { 3 }, passive_id = new List<int>() {  } } },
            {400005, new CardClass() { id = 400005, name = "恢复", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 4, blade = 5, magic = 0, defence = 0, glass = 1, vatality = 0, active_id = new List<int>() { 7 }, passive_id = new List<int>() {  } } },
            {400006, new CardClass() { id = 400006, name = "基本功", icon = "", type = 4, cost = 4, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 10, defence = 0, glass = 0, vatality = 8, active_id = new List<int>() { 1,8,16 }, passive_id = new List<int>() {  } } },
            {400007, new CardClass() { id = 400007, name = "拨挡", icon = "", type = 4, cost = 2, leftAttribute = 3, rightAttribute = 4, blade = 0, magic = 0, defence = 5, glass = 1, vatality = 0, active_id = new List<int>() { 8,23 }, passive_id = new List<int>() {  } } },
            {400008, new CardClass() { id = 400008, name = "高位横斩", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 3, blade = 8, magic = 0, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 1,24 }, passive_id = new List<int>() {  } } },
            {400009, new CardClass() { id = 400009, name = "柄击", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 3, blade = 3, magic = 0, defence = 5, glass = 0, vatality = 0, active_id = new List<int>() { 1 }, passive_id = new List<int>() {  } } },
            {400010, new CardClass() { id = 400010, name = "怒击", icon = "", type = 4, cost = 5, leftAttribute = 1, rightAttribute = 1, blade = 40, magic = 0, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 1 }, passive_id = new List<int>() {  } } },
            {400011, new CardClass() { id = 400011, name = "不稳定飞弹", icon = "", type = 4, cost = 3, leftAttribute = 2, rightAttribute = 5, blade = 0, magic = 30, defence = 0, glass = 0, vatality = 1, active_id = new List<int>() { 5 }, passive_id = new List<int>() {  } } },
            {400012, new CardClass() { id = 400012, name = "激怒", icon = "", type = 4, cost = 1, leftAttribute = 1, rightAttribute = 5, blade = 10, magic = 0, defence = 0, glass = 0, vatality = 5, active_id = new List<int>() { 1,11 }, passive_id = new List<int>() {  } } },
            {400013, new CardClass() { id = 400013, name = "水中倒影", icon = "", type = 4, cost = 1, leftAttribute = 2, rightAttribute = 4, blade = 0, magic = 5, defence = 0, glass = 5, vatality = 0, active_id = new List<int>() { 38 }, passive_id = new List<int>() {  } } },
            {400014, new CardClass() { id = 400014, name = "燃烧", icon = "", type = 4, cost = 2, leftAttribute = 2, rightAttribute = 3, blade = 0, magic = 5, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 39 }, passive_id = new List<int>() {  } } },
            {400015, new CardClass() { id = 400015, name = "恐吓", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 3, blade = 8, magic = 0, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 1,4 }, passive_id = new List<int>() {  } } },
            {400016, new CardClass() { id = 400016, name = "牺牲", icon = "", type = 4, cost = 3, leftAttribute = 2, rightAttribute = 3, blade = 0, magic = 50, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 30 }, passive_id = new List<int>() {  } } },
            {400017, new CardClass() { id = 400017, name = "群体牺牲", icon = "", type = 4, cost = 5, leftAttribute = 2, rightAttribute = 3, blade = 0, magic = 50, defence = 1, glass = 0, vatality = 0, active_id = new List<int>() { 31 }, passive_id = new List<int>() {  } } },
            {400018, new CardClass() { id = 400018, name = "邪恶仪式", icon = "", type = 4, cost = 8, leftAttribute = 1, rightAttribute = 2, blade = 1, magic = 1, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 32 }, passive_id = new List<int>() {  } } },
            {400019, new CardClass() { id = 400019, name = "等价交换", icon = "", type = 4, cost = 5, leftAttribute = 1, rightAttribute = 2, blade = 1, magic = 1, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 33 }, passive_id = new List<int>() {  } } },
            {400020, new CardClass() { id = 400020, name = "人剑合一", icon = "", type = 4, cost = 5, leftAttribute = 1, rightAttribute = 2, blade = 1, magic = 1, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 35 }, passive_id = new List<int>() {  } } },
            {400021, new CardClass() { id = 400021, name = "御剑飞行", icon = "", type = 4, cost = 3, leftAttribute = 1, rightAttribute = 2, blade = 1, magic = 1, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 36 }, passive_id = new List<int>() {  } } },
            {400022, new CardClass() { id = 400022, name = "农夫三剑", icon = "", type = 4, cost = 5, leftAttribute = 1, rightAttribute = 2, blade = 1, magic = 1, defence = 0, glass = 0, vatality = 0, active_id = new List<int>() { 34 }, passive_id = new List<int>() {  } } },
            {400023, new CardClass() { id = 400023, name = "进攻号令", icon = "", type = 4, cost = 2, leftAttribute = 1, rightAttribute = 5, blade = 5, magic = 0, defence = 0, glass = 0, vatality = 5, active_id = new List<int>() { 12 }, passive_id = new List<int>() {  } } },
            {400024, new CardClass() { id = 400024, name = "防守号令", icon = "", type = 4, cost = 5, leftAttribute = 3, rightAttribute = 4, blade = 0, magic = 0, defence = 2, glass = 2, vatality = 0, active_id = new List<int>() { 13 }, passive_id = new List<int>() {  } } },
            {400025, new CardClass() { id = 400025, name = "绝对防御", icon = "", type = 4, cost = 5, leftAttribute = 3, rightAttribute = 4, blade = 0, magic = 0, defence = 5, glass = 1, vatality = 0, active_id = new List<int>() { 11,7 }, passive_id = new List<int>() {  } } },
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
