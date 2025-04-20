using System.Collections.Generic;

public class DamageSkillHandler : SkillHandlerBase
{
    public enum Type{
        ALL = 0,
        ONE = 1,
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
