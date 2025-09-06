using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff
{
    public bool isRemove;
    public BattleEventDefine battleEvent;   // 触发器类型
    public List<int> parameter;              // 被动技能参数 技能ID,参数1,参数2,.....
    
    public BaseBuff(List<int> parameter)
    {
        Debug.Log("BaseBuff()");
        isRemove = true;
        CardPassiveClass cfg = CardPassiveConfig.GetCardPassiveClassByKey((int)parameter[0]);
        if (cfg != null)
        {
            this.parameter = parameter;
            AddTrigger((BattleEventDefine)cfg.passive_trigger);
        }
    }

    public void AddTrigger(BattleEventDefine battleEvent)
    {
        this.battleEvent = battleEvent;
        isRemove = false;
        BattleEventCenter.AddListener(battleEvent, OnTrigger);
    }

    ~BaseBuff()
    {
        Debug.LogError("~BaseBuff()");
        if (!isRemove)
        {
            BattleEventCenter.RemoveListener(battleEvent,OnTrigger);
            isRemove = true;
        }
        if (!isRemove)
        {
            Debug.LogError("----buff 未被溢出，请检查----");
        }
    }

    public abstract void OnTrigger();
}