using System;
using System.Collections.Generic;
using UnityEngine;
// 技能效果触发器基类
public abstract class SkillHandlerBase
{
    public abstract string SkillHandlerName();
    public delegate string DescriptionMethod(List<int> resource);
    public Dictionary<int , DescriptionMethod> typeHandler;
    public Dictionary<SkillSelectorType , List<int>> needOneSelector = new();
    public List<List<int>> _list = new();

    public virtual SkillSelectorType NeedOpenSelector(List<int> active_id)
    {
        foreach (var item in needOneSelector)
        {
            SkillSelectorType key = item.Key;
            List<int> _list = item.Value;
            for(int i = 0 ; i < _list.Count ; i++)
            {
                if(_list[i] == active_id[1]) 
                {
                    return key;
                }
            }
        }
        return SkillSelectorType.NONE;
    }

    public virtual bool CheckCanUse(List<int> active_id)
    {
        foreach (var item in needOneSelector)
        {
            SkillSelectorType key = item.Key;
            List<int> _list = item.Value;
            for(int i = 0 ; i < _list.Count ; i++)
            {
                if(_list[i] == active_id[1]) 
                {
                    var _select = SkillManager.Instance.GetSkillSelectorBase(key);
                    var _unit_list = _select.GetUnits();
                    return _unit_list.Count >= 1;
                }
            }
        }
        return true;
    }

    public void CommonSelector()
    {
        needOneSelector[SkillSelectorType.ONE] = new List<int>{1};
        needOneSelector[SkillSelectorType.PLAYER] = new List<int>{4};
    }

    public virtual void Execute(SkillSelectorBase selector){}
    
    public virtual void Execute(List<int> resource){}

    public virtual void Execute(SkillSelectorBase selector , List<int> resource)
    {
        Debug.Log(SkillHandlerName());
    }

    public abstract string Description(List<int> resource);

    public virtual string Description(int id)
    {
        CardActiveClass cfg = CardActiveConfig.GetCardActiveClassByKey(id);
        return cfg.active_content;
    }

    public virtual string DescriptionByAllInt(List<int> resource)
    {
        if (resource.Count < 1)
        {
            Debug.LogWarning("传递类型错误或资源列表为空");
            return "";
        }
        CardActiveClass cfg = CardActiveConfig.GetCardActiveClassByKey(resource[0]);
        object[] formatArgs = new object[resource.Count - 1];
        for (int i = 1; i < resource.Count; i++)
        {
            formatArgs[i - 1] = resource[i];
        }
        try
        {
            string ans = string.Format(cfg.active_content, formatArgs);
            return ans;
        }
        catch (FormatException)
        {
            Debug.LogWarning($"格式化字符串失败: 参数数量不匹配 (格式需要 {cfg.active_content.Split('{').Length - 1} 个参数，但提供了 {formatArgs.Length} 个)");
            return "";
        }
    }

    public virtual string DescriptionCommon(List<int> resource)
    {
        if (resource.Count < 1)
        {
            Debug.LogWarning("传递类型错误或资源列表为空");
            return "";
        }
        CardActiveClass cfg = CardActiveConfig.GetCardActiveClassByKey(resource[0]);
        object[] formatArgs = new object[resource.Count - 1];
        for (int i = 1; i < resource.Count; i++)
        {
            if (i == 1)
            {
                formatArgs[i - 1] = SkillHelper.GetNameByType((COMMONTYPE)resource[i]);
            }
            else
            {
                if(resource[i] == 1) formatArgs[i - 1] = "";
                else formatArgs[i - 1] = string.Format("{0}{1}",resource[i],"次");
            }
        }
        try
        {
            string ans = string.Format(cfg.active_content, formatArgs);
            return ans;
        }
        catch (FormatException)
        {
            Debug.LogWarning($"格式化字符串失败: 参数数量不匹配 (格式需要 {cfg.active_content.Split('{').Length - 1} 个参数，但提供了 {formatArgs.Length} 个)");
            return "";
        }
    }
}