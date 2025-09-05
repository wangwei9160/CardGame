public abstract class BTAction : BTNode
{
    public override BTNodeState OnUpdate()
    {
        return PerformAction() ? BTNodeState.SUCCESS : BTNodeState.RUNNING;
    }

    public abstract bool PerformAction();
}