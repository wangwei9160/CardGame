using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIViewBase : MonoBehaviour
{
    public virtual string Name => "";

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

