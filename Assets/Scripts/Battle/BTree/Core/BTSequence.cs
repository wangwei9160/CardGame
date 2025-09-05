using System.Collections.Generic;

// BTSequence 按顺序执行子节点，直到一个失败
public class BTSequence : BTNode
{
    public BTSequence() : base() { }
    public BTSequence(List<BTNode> children) : base(children) { }

    public override BTNodeState OnUpdate()
    {
        foreach(BTNode child in childrens)
        {
            var state = child.OnUpdate();
            if(state != BTNodeState.SUCCESS)
            {
                return state;
            }
        }
        
        return BTNodeState.SUCCESS;
    }
}