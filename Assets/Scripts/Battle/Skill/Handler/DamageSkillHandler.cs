using System.Collections.Generic;

public class DamageSkillHandler : SkillHandlerBase
{
    public enum Type{
        ALL = 0,
        ONE = 1,
    }

    public override void Execute(SkillSelectorBase selector)
    {
        List<BaseCharacter> select = selector.GetUnits();
        for(int i = 0 ; i < select.Count ; i++)
        {
            select[i].OnHurt(10);
        }
    }

    public override void Execute(string cfg)
    {
        List<BaseEnemy> enemies = BattleManager.Instance.getAllEnemy();
        foreach(BaseEnemy enemy in  enemies)
        {
            enemy.OnHurt(10);
        }
    }
    public override string Description()
    {
        return "";
    }

    
}
