using System.Collections.Generic;
public class HealSkillHandler : SkillHandlerBase
{
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }

    public override void Execute(List<int> resource)
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
