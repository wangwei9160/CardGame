using System;
using System.Collections.Generic;

[Serializable]
public class EchoEventClass
{
    public int id;
    public string name;
    public string icon;
    public string description;
    public string ps;
    public string type;
}

public class EchoEventManager
{
    public static Dictionary<int, EchoEventClass> m_Dic;

    static EchoEventManager()
    {
        m_Dic = new Dictionary<int, EchoEventClass>()
        {
            {1, new EchoEventClass() { id = 1, name = "斗争", icon = "斗争", description = "散兵游勇，不足为惧", ps = "基础类型", type = "combat" } },
            {4, new EchoEventClass() { id = 4, name = "机遇", icon = "机遇", description = "冥冥之中，自有天意", ps = "基础类型", type = "event" } },
            {16, new EchoEventClass() { id = 16, name = "财运", icon = "财运", description = "金银财宝，收入囊中", ps = "基础类型", type = "event" } },
            {64, new EchoEventClass() { id = 64, name = "宁静", icon = "宁静", description = "养精蓄锐，劳逸结合", ps = "基础类型", type = "rest" } },
            {2, new EchoEventClass() { id = 2, name = "斗争+斗争", icon = "斗争+斗争", description = "兵来将挡，水来土掩", ps = "合成类型", type = "combat" } },
            {5, new EchoEventClass() { id = 5, name = "斗争+机遇", icon = "斗争+机遇", description = "玄机暗藏，险中求之", ps = "合成类型", type = "event" } },
            {17, new EchoEventClass() { id = 17, name = "斗争+财运", icon = "斗争+财运", description = "君子爱财，取之有道", ps = "合成类型", type = "event" } },
            {65, new EchoEventClass() { id = 65, name = "斗争+宁静", icon = "宁静+斗争", description = "置于死地，然后得生", ps = "合成类型", type = "event" } },
            {8, new EchoEventClass() { id = 8, name = "机遇+机遇", icon = "机遇+机遇", description = "千载难逢，天赐良机", ps = "合成类型", type = "event" } },
            {20, new EchoEventClass() { id = 20, name = "机遇+财运", icon = "财运+机遇", description = "小赌怡情，不怡也行", ps = "合成类型", type = "event" } },
            {68, new EchoEventClass() { id = 68, name = "机遇+宁静", icon = "宁静+机遇", description = "步步为营，徐徐图之", ps = "合成类型", type = "event" } },
            {12, new EchoEventClass() { id = 12, name = "财运+财运", icon = "财运+财运", description = "堆金积玉，盆满钵满", ps = "合成类型", type = "event" } },
            {80, new EchoEventClass() { id = 80, name = "财运+宁静", icon = "宁静+财运", description = "或做买卖，或存仁义", ps = "合成类型", type = "store" } },
            {128, new EchoEventClass() { id = 128, name = "宁静+宁静", icon = "宁静+宁静", description = "屏息凝神，超凡入圣", ps = "合成类型", type = "rest" } },
            {3, new EchoEventClass() { id = 3, name = "boss", icon = "", description = "纵使路有千条……", ps = "", type = "combat" } },
            {0, new EchoEventClass() { id = 0, name = "空", icon = "", description = "最少一个，最多两个", ps = "", type = "" } },
        };
    }
    public static EchoEventClass GetEchoEventClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
