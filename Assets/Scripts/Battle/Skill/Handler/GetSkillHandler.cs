using System.Collections.Generic;
using UnityEngine;

public class GetSkillHandler : SkillHandlerBase
{
    public GetSkillHandler()
    {
        _list = new List<List<int>>(){
            new List<int>{6 , 7},
            new List<int>{7},
        };
        typeHandler = new Dictionary<int, DescriptionMethod>{
            {0 , DescriptionByAllInt},
        };
    }

    public override string Description(List<int> resource)
    {
        int resourceType = resource[0];
        for(int i = 0 ; i < _list.Count ; i++)
        {
            for(int j = 0 ; j < _list[i].Count ; j++)
            {
                if(j == resource[0])
                {
                    if (typeHandler.TryGetValue(resourceType, out DescriptionMethod handler))
                    {
                        return handler(resource);
                    }
                    else
                    {
                        Debug.LogWarning($" {resourceType} 指定的获取描述方法不存在");
                        return "";
                    }
                }
            }
        }
        return DescriptionByAllInt(resource);
    }

}