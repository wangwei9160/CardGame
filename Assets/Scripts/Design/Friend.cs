using System;
using System.Collections.Generic;

[Serializable]
public class FriendClass
{
    public int friend_id;
    public string friend_name;
    public int friend_hp;
    public int friend_blade;
    public int friend_magic;
    public int friend_def;
    public int friend_glass;
    public string friend_passive;
    public string friend_active;
    public int friend_mana;
}

public class FriendConfig
{
    public static Dictionary<int, FriendClass> m_Dic;

    static FriendConfig()
    {
        m_Dic = new Dictionary<int, FriendClass>()
        {
            {300001, new FriendClass() { friend_id = 300001, friend_name = "铸造师", friend_hp = 0, friend_blade = 0, friend_magic = 0, friend_def = 0, friend_glass = 0, friend_passive = "", friend_active = "", friend_mana = 0 } },
            {300002, new FriendClass() { friend_id = 300002, friend_name = "舞牛痴", friend_hp = 0, friend_blade = 0, friend_magic = 0, friend_def = 0, friend_glass = 0, friend_passive = "", friend_active = "", friend_mana = 0 } },
            {300003, new FriendClass() { friend_id = 300003, friend_name = "", friend_hp = 0, friend_blade = 0, friend_magic = 0, friend_def = 0, friend_glass = 0, friend_passive = "", friend_active = "", friend_mana = 0 } },
            {300004, new FriendClass() { friend_id = 300004, friend_name = "", friend_hp = 0, friend_blade = 0, friend_magic = 0, friend_def = 0, friend_glass = 0, friend_passive = "", friend_active = "", friend_mana = 0 } },
        };
    }
    public static FriendClass GetFriendClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<FriendClass> GetAll()
    {
        List<FriendClass> ret = new List<FriendClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
