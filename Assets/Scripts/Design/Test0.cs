using System;
using System.Collections.Generic;

[Serializable]
public class Test0Class
{
    public int ID;
    public string Name;
    public string Icon;
    public string Type;
    public string Description;
    public int cost;
}

public class Test0Config
{
    public static Dictionary<int, Test0Class> m_Dic;

    static Test0Config()
    {
        m_Dic = new Dictionary<int, Test0Class>()
        {
            {400001, new Test0Class() { ID = 400001, Name = "直击", Icon = "400001直击", Type = "法术", Description = "主动效果：攻击目标。", cost = 1 } },
            {400002, new Test0Class() { ID = 400002, Name = "附魔打击", Icon = "400002附魔打击", Type = "法术", Description = "主动效果：攻击目标。", cost = 2 } },
            {400003, new Test0Class() { ID = 400003, Name = "转移刺", Icon = "400003转移刺", Type = "法术", Description = "主动效果：攻击目标。", cost = 2 } },
            {400004, new Test0Class() { ID = 400004, Name = "横劈", Icon = "400004横劈", Type = "法术", Description = "主动效果：攻击所有敌人。", cost = 3 } },
            {400005, new Test0Class() { ID = 400005, Name = "恢复", Icon = "400005恢复", Type = "法术", Description = "主动效果：治疗目标。", cost = 2 } },
            {400006, new Test0Class() { ID = 400006, Name = "基本功", Icon = "400006基本功", Type = "法术", Description = "主动效果：攻击目标。", cost = 4 } },
        };
    }
    public static Test0Class GetTest0ClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<Test0Class> GetAll()
    {
        List<Test0Class> ret = new List<Test0Class>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
