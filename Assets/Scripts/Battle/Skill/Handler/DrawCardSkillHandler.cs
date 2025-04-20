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
}
