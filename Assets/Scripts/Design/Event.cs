using System;
using System.Collections.Generic;

[Serializable]
public class EventClass
{
    public int id;
    public string name;
    public int type_ids;
    public string event_type;
}

public class EventManager
{
    public static Dictionary<int, EventClass> m_Dic;

    static EventManager()
    {
        m_Dic = new Dictionary<int, EventClass>()
        {
            {1, new EventClass() { id = 1, name = "被侵蚀的铸造师", type_ids = 101, event_type = "event" } },
            {2, new EventClass() { id = 2, name = "沼泽奇遇（一）", type_ids = 101, event_type = "event" } },
            {3, new EventClass() { id = 3, name = "舞牛", type_ids = 101, event_type = "event" } },
            {4, new EventClass() { id = 4, name = "柳暗花明", type_ids = 102, event_type = "rest" } },
            {5, new EventClass() { id = 5, name = "林中小屋（一）", type_ids = 101, event_type = "event" } },
            {6, new EventClass() { id = 6, name = "狐威", type_ids = 100, event_type = "combat" } },
        };
    }
    public static EventClass GetEventClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
