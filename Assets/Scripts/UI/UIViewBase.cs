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

    // 用于添加监听事件
    public virtual void OnAddlistening() { EventCenter.AddListener(EventDefine.AdjustPosition, AdjustPosition); }
    // 用于销毁时移除监听事件
    public virtual void OnRemovelistening() { EventCenter.RemoveListener(EventDefine.AdjustPosition, AdjustPosition); }
    
    // 应该在实例化后就监听
    protected virtual void Awake()
    {
        OnAddlistening();
    }

    protected virtual void Start() { }

    // 销毁后移除
    protected virtual void OnDestroy()
    {
        OnRemovelistening();
    }

    // 创建时的一些Info更新
    public virtual void Init() { }
    // ui的名称
    public virtual void Init(string str) { _name = str; }
    // 所有权归属，owner
    public virtual void Init(string str ,GameObject obj) { Init(str); }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        //Debug.Log($"{Name} is Show");
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        //Debug.Log($"{Name} is Hide");
    }

    public virtual void Close()
    {
        Destroy(gameObject);
        //Debug.Log($"{Name} is Close");
    }

    public virtual void AdjustPosition() { }
}

