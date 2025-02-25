public interface IInteract 
{
    
}

public interface IFocusable : IInteract
{
    public bool enableFocus { get; }
    public void OnFocus() { }
    public void EndFocus() { }
}

public interface ISelectable : IFocusable
{
    public bool enableSelect { get; }

    public void OnSelect() { }
    public void EndSelect() { }
}

public interface IDragable : ISelectable
{
    public bool enableDrag { get; }

    public void StartDrag() { }
    public void OnDrag() { }
    public void EndDrag() { }

}