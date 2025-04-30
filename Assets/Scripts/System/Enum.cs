using System.Collections.Generic;
using UnityEngine;

public enum CARD_TYPE
{
    WEAPON = 1,
    JEWELRY = 2,
    FOLLOWER = 3,
    MAGIC = 4
}

public enum CARD_ATTRIBUTE_TYPE
{
    BLADE = 1,
    MAGICE,
    DEFENCE,
    GLASS,
    VATALITY
}

public static class EnumHelper
{
    private static readonly Dictionary<CARD_TYPE , string> TypeMap = new Dictionary<CARD_TYPE, string>()
    {
        {CARD_TYPE.WEAPON , "武器"},
        {CARD_TYPE.JEWELRY , "饰品"},
        {CARD_TYPE.FOLLOWER , "随从"},
        {CARD_TYPE.MAGIC , "法术"},
    };

    private static readonly Dictionary<CARD_ATTRIBUTE_TYPE, string> AttributeMap = new Dictionary<CARD_ATTRIBUTE_TYPE, string>
    {
        { CARD_ATTRIBUTE_TYPE.BLADE, "blade" },
        { CARD_ATTRIBUTE_TYPE.MAGICE, "magic" },
        { CARD_ATTRIBUTE_TYPE.DEFENCE, "defence" },
        { CARD_ATTRIBUTE_TYPE.GLASS, "glass" },
        { CARD_ATTRIBUTE_TYPE.VATALITY, "vatality" }
    };

    public static string GetTypeName(this CARD_TYPE type)
    {
        if(TypeMap.TryGetValue(type , out var value))
        {
            return value;
        }else {
            Debug.LogWarning($"不存在类型{type}对应的名称");
        }
        return "";
    }

    public static string GetAttributeName(this CARD_ATTRIBUTE_TYPE type)
    {
        if(AttributeMap.TryGetValue(type , out var value))
        {
            return value;
        }else {
            Debug.LogWarning($"不存在类型{type}对应的名称");
        }
        return "";
    }
}