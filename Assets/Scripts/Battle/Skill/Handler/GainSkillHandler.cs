using System.Collections.Generic;

public class GainSkillHandler : SkillHandlerBase
{
    public GainSkillHandler()
    {
        CommonSelector();
    }
    public override string SkillHandlerName(){ return "GainSkillHandler"; }
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }

}