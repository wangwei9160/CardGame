using System.Collections.Generic;
public class HeadlSkillHandler : SkillHandlerBase
{
    public override string Description()
    {
        return "";
    }

    public override void Execute(string cfg)
    {
        var playerTeam = BattleManager.Instance.getAllPlayerTeam();
        foreach (var obj in playerTeam)
        {
            obj.OnHeal(10);
        }
    }

    public override void Execute(SkillSelectorBase selector)
    {
        var playerTeam = BattleManager.Instance.getAllPlayerTeam();
        foreach (var obj in playerTeam)
        {
            obj.OnHeal(10);
        }
    }
}
