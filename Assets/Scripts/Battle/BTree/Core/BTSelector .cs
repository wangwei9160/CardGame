using System.Collections.Generic;

// BTSequence 按顺序执行子节点，直到一个失败
public class BTSelector : BTNode
{
    public BTSelector() : base() { }
    public BTSelector(List<BTNode> children) : base(children) { }

    public override BTNodeState OnUpdate()
    {
        foreach(BTNode child in childrens)
        {
            var state = child.OnUpdate();
            if(state != BTNodeState.FAILURE)
            {
                return state;
            }
        }

        return BTNodeState.FAILURE;
    }
}