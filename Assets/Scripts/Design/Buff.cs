using System;
using System.Collections.Generic;

[Serializable]
public class BuffClass
{
    public int buff_id;
    public string buff_name;
    public string buff_effect;
}

public class BuffConfig
{
    public static Dictionary<int, BuffClass> m_Dic;

    static BuffConfig()
    {
        m_Dic = new Dictionary<int, BuffClass>()
        {
            {500001, new BuffClass() { buff_id = 500001, buff_name = "被附身", buff_effect = "最大生命值-70%,但是你的每回合获得法力值翻倍。同时后续触发相关事件" } },
            {500002, new BuffClass() { buff_id = 500002, buff_name = "普通buff001", buff_effect = "下场战斗胜利后额外获得三张卡牌,但是有可能（25%）获得一个负面遗物" } },
            {500003, new BuffClass() { buff_id = 500003, buff_name = "", buff_effect = "" } },
            {500004, new BuffClass() { buff_id = 500004, buff_name = "", buff_effect = "" } },
            {500005, new BuffClass() { buff_id = 500005, buff_name = "", buff_effect = "" } },
            {500006, new BuffClass() { buff_id = 500006, buff_name = "", buff_effect = "" } },
            {500007, new BuffClass() { buff_id = 500007, buff_name = "", buff_effect = "" } },
            {500008, new BuffClass() { buff_id = 500008, buff_name = "", buff_effect = "" } },
            {500009, new BuffClass() { buff_id = 500009, buff_name = "", buff_effect = "" } },
            {500010, new BuffClass() { buff_id = 500010, buff_name = "", buff_effect = "" } },
        };
    }
    public static BuffClass GetBuffClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<BuffClass> GetAll()
    {
        List<BuffClass> ret = new List<BuffClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
