using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

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
            {SkillSelectorType.RANDOM , new RandomSelector() },
            {SkillSelectorType.SELF , new RandomSelector() },
            {SkillSelectorType.PLAYER , new TroopSelector(1,1) },
        };
    }

    public SkillSelectorType OpenSelector(List<int> _list)
    {
        SkillSelectorType stp = SkillSelectorType.NONE;
        if (skillHandlers.TryGetValue((SkillType)_list[0], out var handler))
        {
            var ntp = handler.NeedOpenSelector(_list);
            if(stp.CompareTo(ntp) < 0)
            {
                stp = ntp;
            }
        }
        else
        {
            Debug.LogWarning($"当前不存在 Type={_list[0]} 的技能选择器");
        }
        return stp;
    }

    public SkillSelectorType OpenSelector(List<List<int>> _list)
    {
        SkillSelectorType stp = SkillSelectorType.NONE;
        for(int i = 0 ; i < _list.Count ; i++)
        {
            var active_id = _list[i];
            if (skillHandlers.TryGetValue((SkillType)active_id[0], out var handler))
            {
                var ntp = handler.NeedOpenSelector(active_id);
                if(stp.CompareTo(ntp) < 0)
                {
                    stp = ntp;
                }
            }
            else
            {
                Debug.Log($"当前不存在 Type={active_id[0]} 的技能选择器");
            }
        }
        return stp;
    }

    public SkillSelectorType OpenSelector(int cardID)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(cardID);
        return OpenSelector(cfg.active_id);
    }

    public void PreExecuteSelecte(int id)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        PreExecuteSelecte(cfg);
    }

    public void PreExecuteSelecte(CardClass cfg)
    {
        SkillSelectorType tp = OpenSelector(cfg.active_id);
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

    public void PreExecuteSelecteClose(int id)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        PreExecuteSelecteClose(cfg);
    }

    public void PreExecuteSelecteClose(CardClass cfg)
    {
        SkillSelectorType tp = OpenSelector(cfg.active_id);
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

    #region CHECK

    public bool CheckNeedHideCard(int cardID)
    {
        var stp = OpenSelector(cardID);
        if(stp == SkillSelectorType.ONE) {
            return true;
        }
        return false;
    }

    public bool CheckTypeAndSelect(int cardID)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(cardID);
        return checkTypeAndSelect(cfg);
    }

    public bool checkTypeAndSelect(CardClass cfg)
    {
        var active_ids = cfg.active_id;
        bool ok = true;
        for(int i = 0 ; i < active_ids.Count && ok ; i++)
        {
            var _list = active_ids[i];
            if(skillHandlers.TryGetValue((SkillType)_list[0] , out var handler))
            {
                 ok = ok & (handler.CheckCanUse(_list));
            }
        }
        return ok;
    }
    #endregion

    public void ExecuteEffect(int id)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        ExecuteEffect(cfg);
    }

    public void ExecuteEffect(CardClass cfg)
    {
        var active_ids = cfg.active_id;
        for(int i = 0 ; i < active_ids.Count ; i++)
        {
            var _list = active_ids[i];
            SkillType tp = (SkillType)_list[0];
            if (skillHandlers.TryGetValue(tp , out var handler))
            {
                SkillSelectorType stp = OpenSelector(_list);
                if(skillSelectors.TryGetValue(stp , out var selector))
                {
                    handler.Execute(_list);
                }
            }else
            {
                Debug.Log($"当前不存在 Type={tp} 的技能类型处理器");
            }
        }
    }

    public string GetSkillDescription(int id)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        return GetSkillDescription(cfg);
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

    #region Get Map

    public SkillType GetSkillType(List<int> resource)
    {
        if (id2SkillType.TryGetValue(resource[0], out var type))
        {
            return type;
        }
        else
        {
            Debug.Log($"当前不存在 Type={resource[0]} 的技能类型");
        }
        return SkillType.ATTACK;
    }

    public SkillSelectorBase GetSkillSelectorBase(SkillSelectorType stp)
    {
        if (skillSelectors.TryGetValue(stp, out var handler))
        {
            return handler;
        }
        else
        {
            Debug.Log($"当前不存在 Type={stp} 的技能选择器");
        }
        return null;
    }

    #endregion

}