using System.Collections.Generic;

public class DamageSkillHandler : SkillHandlerBase
{
    public override void Execute(string cfg)
    {
        //base.Execute(cfg);
        List<BaseEnemy> enemies = BattleManager.Instance.getAllEnemy();
        foreach(BaseEnemy enemy in  enemies)
        {
            enemy.OnHurt(10);
        }
    }
}
