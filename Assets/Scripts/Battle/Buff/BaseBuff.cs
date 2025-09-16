using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff
{
    public BattleLogicUnit owner;           // Buff持有者
    public bool isRemove;                   // 移除检测，防止移除中且又触发了
    public BattleEventDefine battleEvent;   // 触发器类型
    public List<int> parameter;              // 被动技能参数 技能ID,参数1,参数2,.....

    // 一些Buff可能需要的参数
    public int fireTimes = 0;

    public BaseBuff(BattleLogicUnit owner,List<int> parameter)
    {
        Debug.Log("BaseBuff()");
        isRemove = true;
        CardPassiveClass cfg = CardPassiveConfig.GetCardPassiveClassByKey((int)parameter[0]);
        if (cfg != null)
        {
            this.parameter = parameter;
            this.owner = owner;
            AddTrigger((BattleEventDefine)cfg.passive_trigger);
        }
    }

    public void AddTrigger(BattleEventDefine battleEvent)
    {
        this.battleEvent = battleEvent;
        isRemove = false;
        BattleManager.Instance.BaseBattlePlayer.EventCenter.AddListener(battleEvent, OnTrigger);
    }

    ~BaseBuff()
    {
        Debug.LogError("~BaseBuff()");
        if (!isRemove)
        {
            BattleManager.Instance.BaseBattlePlayer.EventCenter.RemoveListener(battleEvent,OnTrigger);
            isRemove = true;
        }
        if (!isRemove)
        {
            Debug.LogError("----buff 未被溢出，请检查----");
        }
    }

    public abstract void OnTrigger();
}