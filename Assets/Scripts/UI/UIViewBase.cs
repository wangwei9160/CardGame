using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIViewType
{
    Unknown = 0,
    Singleton,
    Multiple,
    
}

public class UIViewBase : MonoBehaviour
{
    public string _name = "";
    public virtual string Name => _name;

    public virtual UIViewType Type => UIViewType.Singleton;
    public int index = 0;
    public void ResetIndex(int idx)
    {
        this.index = idx;
    }
    public virtual int Index => index;

    public virtual void OnAddlistening() { }
    public virtual void OnRemovelistening() { }

    protected virtual void Start()
    {
        OnAddlistening();
    }

    protected virtual void OnDestroy()
    {
        OnRemovelistening();
    }

    public virtual void Init() { }
    public virtual void Init(string str) { _name = str; }
    public virtual void Init(GameObject obj) { }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        Debug.Log($"{Name} is Show");
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log($"{Name} is Hide");
    }

    public virtual void Close()
    {
        Destroy(gameObject);
        Debug.Log($"{Name} is Close");
    }
}

