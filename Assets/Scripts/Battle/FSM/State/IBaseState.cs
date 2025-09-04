public class IBaseState
{
    public BaseBattlePlayer Parent { get; set; }

    public IBaseState(BaseBattlePlayer parent)
    {
        Parent = parent;
    }   

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }

}