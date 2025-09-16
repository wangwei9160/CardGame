using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnit
{
    public int UnitId { get; set; }
    public int UnitTeam { get; set; }
    public int[] Attributes = new int[(int)UnitAttribute.COUNT];
    public List<BaseBuff> buffs = new List<BaseBuff>();

    public BattlePerformUnit PerformUnit { get; set; }

    public CardClass Cfg { get; set; }

    public int leftAttributeType;   // 左属性
    public int rightAttributeType;  // 右属性
    public BattleLogicUnit()
    {
        for (int i = 0; i < (int)UnitAttribute.COUNT; i++)
        {
            Attributes[i] = 0;
        }
    }

    public BattleLogicUnit(int id,int teamId):this() 
    {
        UnitId = id;
        UnitTeam = teamId;
        Cfg = CardConfig.GetCardClassByKey(id);
        if (Cfg != null)
        {
            leftAttributeType = Cfg.leftAttribute;
            rightAttributeType = Cfg.rightAttribute;
            for (int i = 0; i < Cfg.passive_id.Count; i++)
            {
                // 被动技能 挂Buff到身上
                // 传递一个引用给Buff保存，不直接使用
                var buff = AddBuffFactory.CreateBuff(this, Cfg.passive_id[i]);
                buffs.Add(buff);
            }
        }
    }

    public bool IsAlive()
    {
        return true;
    }

    public bool IsOwnerTeam()
    {
        return (UnitTeamType)UnitTeam == UnitTeamType.OWNER;
    }

    // 技能释放相关
    public CardActiveClass useActiveSkill;
    public BattleLogicUnit skillTarget;

    public bool CardCostCheck()
    {
        if(Cfg != null)
        {
            if(Cfg.cost >= 0) 
                return true;
        }
        return false;
    }

    // 获取当前技能的参数列表（技能id,参数1,参数2...)
    public List<int> GetCurrentSkillConfigWithParameter()
    {
        if (Cfg != null && Cfg.active_id.Count > 0)
        {
            useActiveSkill = CardActiveConfig.GetCardActiveClassByKey(Cfg.active_id[0][0]);
        }
        if (useActiveSkill != null)
        {
            for (int i = 0; i < Cfg.active_id.Count; i++)
            {
                if (Cfg.active_id[i][0] == useActiveSkill.active_id)
                {
                    return Cfg.active_id[i];
                }
            }
        }
        return null;
    }

    public void SetSkillTarget()
    {
        if (IsOwnerTeam())
        {

        }else
        {
            skillTarget = BattleManager.Instance.BaseBattlePlayer.battleTeams[(int)UnitTeamType.OWNER].BattleUnits[0];
            Debug.Log("选择左边阵营id" + skillTarget.UnitId);
        }
    }

    // 获取技能目标
    public BattleLogicUnit GetSkillTarget()
    {
        if(useActiveSkill != null)
        {

        }
        return null;
    }

    public void Attack()
    {
        if(PerformUnit != null)
        {
            BattleManager.Instance.BaseBattlePlayer.PerformModule.AddPerformAction(new PerformAction
            {
                actionType = PerformActionType.UnitAction,
                onwer = this,
                name = "isRunning",
                OnAnimationEnd = () => { Debug.Log("动画播放完毕"); }
            });
        }
    }
}