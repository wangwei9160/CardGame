using System.Collections.Generic;
public class DrawCardSkillHandler : SkillHandlerBase
{
    public override string SkillHandlerName(){ return "DrawCardSkillHandler"; }
    public override string Description(List<int> resource)
    {
        return DescriptionByAllInt(resource);
    }

    public override void Execute(List<int> resource)
    {
        for(int i = 0 ; i < resource[1] ; i++)
        {
            BattleManager.Instance.GetHandCard();
        }
    }

    public override void Execute(SkillSelectorBase selector)
    {
        throw new System.NotImplementedException();
    }
}
