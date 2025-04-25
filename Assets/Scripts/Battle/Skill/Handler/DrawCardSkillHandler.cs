using System.Collections.Generic;
public class DrawCardSkillHandler : SkillHandlerBase
{
    public override string Description(List<int> resource)
    {
        return DescriptionByAllInt(resource);
    }

    public override void Execute(List<int> resource)
    {
        BattleManager.Instance.GetHandCard();
    }

    public override void Execute(SkillSelectorBase selector)
    {
        throw new System.NotImplementedException();
    }
}
