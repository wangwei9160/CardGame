using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ATTACK = 1,
    HEAL = 2,
    GAIN = 3,
    REDUCE = 4,
    DRAW = 5,
    MAGIC = 6,
    GET = 7,
    CONDITION_DRAW = 8,
    CONDITION_ATTACK = 9,
    SUMMON = 10,
    BUFF = 11,
    BACK = 12,
    
}

public enum SkillSelectorType
{
    NONE = 0,
    ONE = 1,
}

// 0所有敌人，1目标，2随机敌人，3自身，4巫真。
public enum COMMONTYPE
{
    ALL = 0,
    ONE = 1,
    RANDOM = 2,
    SELF = 3,
    HERO = 4
}

public static class SkillHelper
{
    public static Dictionary<COMMONTYPE , string> SkillTypeName;
    static SkillHelper()
    {
        SkillTypeName = new Dictionary<COMMONTYPE, string>(){
            {COMMONTYPE.ALL , "所有敌方单位"},
            {COMMONTYPE.ONE , "一个指定敌方单位"},
            {COMMONTYPE.RANDOM , "一个随机敌人"},
            {COMMONTYPE.SELF , "自身"},
            {COMMONTYPE.HERO , "巫真"},
        };
    }

    public static string GetNameByType(COMMONTYPE tp)
    {
        if(SkillTypeName.TryGetValue(tp , out var name))
        {
            return name;
        }else {
            Debug.LogWarning($"SkillHelper.SkillTypeName 不存在类型 {tp},请联系相关负责人");
            return "";
        }
    }
}