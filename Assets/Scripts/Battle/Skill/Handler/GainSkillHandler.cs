using System.Collections.Generic;

public class GainSkillHandler : SkillHandlerBase
{
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }

}