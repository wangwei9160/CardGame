using System.Collections.Generic;

public class AttackSkillHandler : SkillHandlerBase
{
    // 1 
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }
    public override void Execute(SkillSelectorBase selector)
    {
        List<BaseCharacter> select = selector.GetUnits();
        for(int i = 0 ; i < select.Count ; i++)
        {
            select[i].OnHurt(10);
        }
    }

    public override void Execute(List<int> resource)
    {
        List<BaseEnemy> enemies = BattleManager.Instance.getAllEnemy();
        foreach(BaseEnemy enemy in  enemies)
        {
            enemy.OnHurt(10);
        }
    }
    
}
