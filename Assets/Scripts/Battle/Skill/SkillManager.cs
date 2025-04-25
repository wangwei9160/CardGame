using UnityEngine;
using System.Collections.Generic;

public class SkillManager : ManagerBase<SkillManager>
{
    // 主动效果 or 被动效果
    private Dictionary<int , SkillType> id2SkillType;
    private Dictionary<SkillType, SkillHandlerBase> skillHandlers;
    private Dictionary<SkillSelectorType, SkillSelectorBase> skillSelectors;

    private void Start()
    {
        id2SkillType = new Dictionary<int, SkillType>()
        {
            {1,SkillType.ATTACK},
            {2,SkillType.HEAL},
            {3,SkillType.GAIN},
            {4,SkillType.REDUCE},
            {5,SkillType.DRAW},
            {6,SkillType.GET},
            {7,SkillType.GET},
            {8,SkillType.CONDITION_DRAW},
            {9,SkillType.CONDITION_ATTACK},
            {10,SkillType.SUMMON},
            {11,SkillType.BUFF},
            {12,SkillType.BACK},
            {13,SkillType.SUMMON},
            {14,SkillType.SUMMON},
        };
        skillHandlers = new Dictionary<SkillType, SkillHandlerBase>()
        {
            {SkillType.ATTACK , new AttackSkillHandler() },
            {SkillType.HEAL , new HealSkillHandler() },
            {SkillType.GAIN , new GainSkillHandler() },
            {SkillType.REDUCE , new ReduceSkillHandler() },
            {SkillType.DRAW , new DrawCardSkillHandler() },
            {SkillType.MAGIC , new MagicSkillHandler() },
            {SkillType.GET , new GetSkillHandler() },
            {SkillType.CONDITION_DRAW , new ConditionDrawCardSkillHandler() },
            {SkillType.CONDITION_ATTACK , new ConditionAttackSkillHandler() },
            {SkillType.SUMMON , new SummonSkillHandler() },
            {SkillType.BUFF , new BuffSkillHandler() },
            {SkillType.BACK , new BackSkillHandler() },
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

    public string GetSkillDescription(int id)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        List<string> ret = new List<string>();
        var _list = cfg.active_id;
        for(int i = 0 ; i < _list.Count ; i++)
        {
            ret.Add(GetSkillDescriptionByType(_list[i]));
        }
        string ans = string.Join("\n", ret);
        return ans;
    }

    public string GetSkillDescription(CardClass cfg)
    {
        List<string> ret = new List<string>();
        var _list = cfg.active_id;
        for(int i = 0 ; i < _list.Count ; i++)
        {
            ret.Add(GetSkillDescriptionByType(_list[i]));
        }
        string ans = string.Join("\n", ret);
        return ans;
    }

    public string GetSkillDescriptionByType(List<int> args)
    {
        SkillType tp = id2SkillType[args[0]];
        if (skillHandlers.TryGetValue(tp, out var handler))
        {
            return handler.Description(args);
        }else
        {
            Debug.LogWarning($"当前不存在 Type={tp} 的技能类型处理器");
        }
        return "";
    }

}