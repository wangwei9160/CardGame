using System.Collections.Generic;
public enum BTNodeState
{
    RUNNING,
    SUCCESS,
    FAILURE,
}

public class BTNode
{
    public BTRoot parent;
    protected BTNode child;
    protected List<BTNode> childrens = new();

    public BTNode()
    {
        parent = null;
    }

    public virtual T GetParameter<T>(string key, T value)
    {
        return parent.GetParameter(key, value);
    }

    public virtual void AddParameter<T>(string key, T value)
    {
        parent.SetParameter(key, value);
    }

    public BTNode(List<BTNode> children)
    {
        foreach (var child in children)
        {
            AddChildrens(child);
        }
    }

    public void AddChild(BTNode node)
    {
        node.parent = this.parent;
        child = node;
    }

    public void AddChildrens(BTNode node)
    {
        node.parent = this.parent;
        childrens.Add(node);
    }

    public virtual BTNodeState OnUpdate()
    {
        return BTNodeState.SUCCESS;
    }
}