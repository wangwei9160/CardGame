using UnityEngine;
using System.Collections.Generic;
public class ReduceSkillHandler : SkillHandlerBase
{
    public ReduceSkillHandler()
    {
        CommonSelector();
    }
    public override string SkillHandlerName(){ return "ReduceSkillHandler"; }
    public override string Description(List<int> resource)
    {
        return DescriptionCommon(resource);
    }
}