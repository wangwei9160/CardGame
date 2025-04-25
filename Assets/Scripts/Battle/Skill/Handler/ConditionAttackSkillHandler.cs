using System.Collections.Generic;
public class ConditionAttackSkillHandler : SkillHandlerBase
{
    public override string Description(List<int> resource)
    {
        return Description(resource[0]);
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
