using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public enum SkillType
{
    DAMAGE = 1,
    HEAL = 2,
    DRAW = 3,
    GAIN = 4,
}

public class SkillManager : ManagerBase<SkillManager>
{
    // 主动效果 or 被动效果
    private Dictionary<SkillType, SkillHandlerBase> skillHandlers;

    private void Start()
    {
        skillHandlers = new Dictionary<SkillType, SkillHandlerBase>()
        {
            {SkillType.DAMAGE , new DamageSkillHandler() },
            {SkillType.HEAL , new HeadlSkillHandler() },
            {SkillType.DRAW , new DrawCardSkillHandler() },
            {SkillType.GAIN , new GainSkillHandler() },
        };
    }

    public void ExecuteEffect(SkillType tp , string config)
    {
        if (skillHandlers.TryGetValue(tp , out var handler))
        {
            handler.Execute(config);
            return ;
        }else
        {
            Debug.Log($"当前不存在 Type={tp} 的技能类型处理器");
        }
    }

    public string GetSkillDescription(SkillType tp)
    {
        if (skillHandlers.TryGetValue(tp , out var handler))
        {
            return handler.Description();
        }else
        {
            Debug.LogWarning($"当前不存在 Type={tp} 的技能类型处理器");
        }
        return "";
    }

}

// 卡牌效果显示,找到效果表,显示效果
// 卡牌使用（对敌人造成伤害）,找效果表,skillManager内效果找到触发器,传参,触发事件,事件下发 or 直接触发