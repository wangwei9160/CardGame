using System.Collections.Generic;

public class BTNode
{
    public BTNode parent;
    protected List<BTNode> childrens = new List<BTNode>();

    public BTNode()
    {
        parent = null;
    }

    public BTNode(List<BTNode> children)
    {
        foreach (var child in children)
        {
            AddChildren(child);
        }
    }

    public void AddChildren(BTNode node)
    {
        node.parent = this;
        childrens.Add(node);
    }
}