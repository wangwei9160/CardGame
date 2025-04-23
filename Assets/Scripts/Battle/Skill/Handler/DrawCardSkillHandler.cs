using System.Collections.Generic;
public class DrawCardSkillHandler : SkillHandlerBase
{
    public override string Description()
    {
        return "";
    }

    public override void Execute(string cfg)
    {
        BattleManager.Instance.GetHandCard();
    }

    public override void Execute(SkillSelectorBase selector)
    {
        throw new System.NotImplementedException();
    }
}
