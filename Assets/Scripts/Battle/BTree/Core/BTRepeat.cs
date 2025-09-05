using System.Collections.Generic;
using UnityEngine;

// BTRepeat 重复执行
public class BTRepeat : BTNode
{
    public BTRepeat() : base() { }

    public override BTNodeState OnUpdate()
    {
        while(child.OnUpdate() != BTNodeState.FAILURE)
        {
            Debug.Log("BTRepeat ==> child.OnUpdate");
        }
        
        return BTNodeState.SUCCESS;
    }
}