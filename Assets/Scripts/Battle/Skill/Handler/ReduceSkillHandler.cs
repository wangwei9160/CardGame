using UnityEngine;
using System.Collections.Generic;
public class ReduceSkillHandler : SkillHandlerBase
{
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }
}