using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFightState
{
    protected virtual string name { get; private set; }
    public string ID => name;
    public virtual void OnEnter()
    {
        //Debug.Log("OnEnter() " + ID);
        EventCenter.Broadcast(EventDefine.ChangeState, this);
    }

    public virtual void OnUpdate()
    {
        //Debug.Log("OnUpdate() " + ID);
    }

    public virtual void OnExit()
    {
        //Debug.Log("OnExit() " + ID);
    }

    public virtual bool TryChange(IFightState other)
    {
        if (ID == other.ID) return false;
        return true;
    }

}
