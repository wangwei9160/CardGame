using System;
using System.Collections.Generic;

[Serializable]
public class CombatClass
{
    public int combat_ids;
    public string combat_type;
    public List<List<int>> enemy_id;
}

public class CombatManager
{
    public static Dictionary<int, CombatClass> m_Dic;

    static CombatManager()
    {
        m_Dic = new Dictionary<int, CombatClass>()
        {
            {60001, new CombatClass() { combat_ids = 60001, combat_type = "normal_1", enemy_id = new List<List<int>>() { new List<int>() { 10001, 10001 }, new List<int>() { 0, 10001 }, new List<int>() { 0, 0 } } } },
            {60002, new CombatClass() { combat_ids = 60002, combat_type = "normal_1", enemy_id = new List<List<int>>() { new List<int>() { 10008, 0 }, new List<int>() { 0, 10002 }, new List<int>() { 0, 0 } } } },
            {60003, new CombatClass() { combat_ids = 60003, combat_type = "normal_2", enemy_id = new List<List<int>>() { new List<int>() { 10012, 0 }, new List<int>() { 0, 10011 }, new List<int>() { 0, 0 } } } },
            {60004, new CombatClass() { combat_ids = 60004, combat_type = "normal_2", enemy_id = new List<List<int>>() { new List<int>() { 10004, 10005 }, new List<int>() { 0, 10005 }, new List<int>() { 0, 0 } } } },
            {60005, new CombatClass() { combat_ids = 60005, combat_type = "normal_2", enemy_id = new List<List<int>>() { new List<int>() { 10006, 0 }, new List<int>() { 0, 10006 }, new List<int>() { 0, 0 } } } },
            {60006, new CombatClass() { combat_ids = 60006, combat_type = "hard_1", enemy_id = new List<List<int>>() { new List<int>() { 10003, 0 }, new List<int>() { 0, 10001 }, new List<int>() { 0, 0 } } } },
            {60007, new CombatClass() { combat_ids = 60007, combat_type = "hard_2", enemy_id = new List<List<int>>() { new List<int>() { 10011, 0 }, new List<int>() { 0, 10001 }, new List<int>() { 0, 10006 } } } },
            {60008, new CombatClass() { combat_ids = 60008, combat_type = "hard_2", enemy_id = new List<List<int>>() { new List<int>() { 10012, 0 }, new List<int>() { 0, 10001 }, new List<int>() { 0, 10002 } } } },
        };
    }
    public static CombatClass GetCombatClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
