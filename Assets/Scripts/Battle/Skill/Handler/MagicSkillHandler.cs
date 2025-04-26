using System.Collections.Generic;

public class MagicSkillHandler : SkillHandlerBase
{
    public override string SkillHandlerName(){ return "MagicSkillHandler"; }
    public override string Description(List<int> resource)
    {
        return DescriptionByAllInt(resource);
    }

}