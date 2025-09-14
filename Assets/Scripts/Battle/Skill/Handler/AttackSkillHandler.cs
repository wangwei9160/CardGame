using System;
using System.Collections.Generic;

public class AttackSkillHandler : SkillHandlerBase
{
    public override string SkillHandlerName(){ return "AttackSkillHandler"; }
    // 1 

    public AttackSkillHandler()
    {
        CommonSelector();
    }

    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }
    public override void Execute(SkillSelectorBase selector)
    {
        List<BattlePerformUnit> select = selector.GetUnits();
        for(int i = 0 ; i < select.Count ; i++)
        {
            select[i].OnHurt(10);
        }
    }

    public override void Execute(List<int> resource)
    {
        SkillSelectorType stp =  SkillManager.Instance.OpenSelector(resource);
        SkillSelectorBase _selector = SkillManager.Instance.GetSkillSelectorBase(stp);
        List<BattlePerformUnit> enemies = _selector.GetUnits();
        foreach(BattlePerformUnit enemy in  enemies)
        {
            // Attack Count
            for(int i = 0 ; i < resource[2] ; i++)
            {
                enemy.OnHurt(10);
            }
        }
    }

    public override void Execute(SkillSelectorBase selector , List<int> resource)
    {
        List<BattlePerformUnit> enemies = BattleManager.Instance.getAllEnemy();
        foreach(BaseEnemy enemy in  enemies)
        {
            enemy.OnHurt(10);
        }
    }
}
