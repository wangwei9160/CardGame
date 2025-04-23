using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using Unity.VisualScripting.FullSerializer;

public enum SkillType
{
    DAMAGE = 1,
    HEAL = 2,
    DRAW = 3,
    GAIN = 4,
}

public enum SkillSelectorType
{
    NONE = 0,
    ONE = 1,
}

public class SkillManager : ManagerBase<SkillManager>
{
    // 主动效果 or 被动效果
    private Dictionary<SkillType, SkillHandlerBase> skillHandlers;
    private Dictionary<SkillSelectorType, SkillSelectorBase> skillSelectors;

    private void Start()
    {
        skillHandlers = new Dictionary<SkillType, SkillHandlerBase>()
        {
            {SkillType.DAMAGE , new DamageSkillHandler() },
            {SkillType.HEAL , new HeadlSkillHandler() },
            {SkillType.DRAW , new DrawCardSkillHandler() },
            {SkillType.GAIN , new GainSkillHandler() },
        };
        skillSelectors = new Dictionary<SkillSelectorType, SkillSelectorBase>
        {
            {SkillSelectorType.ONE , new OneSelector() },
            {SkillSelectorType.NONE , new NoneSelector() },
        };
    }

    public void PreExecuteSelecte(SkillSelectorType tp)
    {
        if (skillSelectors.TryGetValue(tp, out var handler))
        {
            handler.CreateSelector();
            return;
        }
        else
        {
            Debug.Log($"当前不存在 Type={tp} 的技能选择器");
        }
    }

    public void PreExecuteSelecteClose(SkillSelectorType tp)
    {
        if (skillSelectors.TryGetValue(tp, out var handler))
        {
            handler.CloseSelector();
            return;
        }
        else
        {
            Debug.Log($"当前不存在 Type={tp} 的技能选择器");
        }
    }

    public bool checkTypeAndSelect(SkillType tp , SkillSelectorType stp)
    {
        if(stp == SkillSelectorType.ONE) 
        {
            if (skillSelectors.TryGetValue(stp, out var handler))
            {
                return handler.GetUnits() != null;
            }
            else
            {
                Debug.Log($"当前不存在 Type={tp} 的技能选择器");
                return false;
            }
        }
        return true;
    }

    public void ExecuteEffect(SkillType tp , SkillSelectorType stp , string config)
    {
        if (skillHandlers.TryGetValue(tp , out var handler))
        {
            if(skillSelectors.TryGetValue(stp , out var selector))
            handler.Execute(selector);
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