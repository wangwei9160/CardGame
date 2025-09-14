using System.Collections.Generic;
public class HealSkillHandler : SkillHandlerBase
{
    public HealSkillHandler()
    {
        CommonSelector();
    }
    public override string SkillHandlerName(){ return "HealSkillHandler"; }
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }

    public override void Execute(List<int> resource)
    {
        SkillSelectorType stp =  SkillManager.Instance.OpenSelector(resource);
        SkillSelectorBase _selector = SkillManager.Instance.GetSkillSelectorBase(stp);
        List<BattlePerformUnit> _list = _selector.GetUnits();
        foreach(BattlePerformUnit obj in  _list)
        {
            // Count
            for(int i = 0 ; i < resource[2] ; i++)
            {
                obj.OnHeal(10);
            }
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
