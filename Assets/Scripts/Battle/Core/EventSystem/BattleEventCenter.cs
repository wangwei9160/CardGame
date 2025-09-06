using System;
using System.Collections.Generic;

// 事件中心
public static class BattleEventCenter
{
    private static Dictionary<BattleEventDefine, Delegate> m_Event = new Dictionary<BattleEventDefine, Delegate>();

    public static void Clear()
    {
        m_Event.Clear();
    }

    private static void OnListenerAdding(BattleEventDefine eventType, Delegate del)
    {
        if (!m_Event.ContainsKey(eventType))
        {
            m_Event.Add(eventType, null);
        }
        Delegate d = m_Event[eventType];
        if (d != null && d.GetType() != del.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}[添加]不同类型的委托,{1}和{2}", eventType, d.GetType(), del.GetType()));
        }
    }

    private static void OnListenerRemoving(BattleEventDefine eventType, Delegate del)
    {
        if (m_Event.ContainsKey(eventType))
        {
            Delegate d = m_Event[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("事件{0}没有正在监听的委托", eventType));
            }
            else if (d.GetType() != del.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}[移除]不同类型的委托,{1}和{2}", eventType, d.GetType(), del.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("不存在事件{0}", eventType));
        }
    }

    private static void OnEventRemove(BattleEventDefine eventType)
    {
        if (m_Event[eventType] == null)
        {
            m_Event.Remove(eventType);
        }
    }


    public static void AddListener(BattleEventDefine eventType, BattleCallBack callback)
    {
        OnListenerAdding(eventType, callback);
        m_Event[eventType] = (BattleCallBack)m_Event[eventType] + callback;
    }

    public static void AddListener<T>(BattleEventDefine eventType, BattleCallBack<T> callback)
    {
        OnListenerAdding(eventType, callback);
        m_Event[eventType] = (BattleCallBack<T>)m_Event[eventType] + callback;
    }

    public static void AddListener<T, U>(BattleEventDefine eventType, BattleCallBack<T, U> callback)
    {
        OnListenerAdding(eventType, callback);
        m_Event[eventType] = (BattleCallBack<T, U>)m_Event[eventType] + callback;
    }
    public static void AddListener<T, U, V>(BattleEventDefine eventType, BattleCallBack<T, U, V> callback)
    {
        OnListenerAdding(eventType, callback);
        m_Event[eventType] = (BattleCallBack<T, U, V>)m_Event[eventType] + callback;
    }

    public static void RemoveListener(BattleEventDefine eventType, BattleCallBack callback)
    {
        OnListenerRemoving(eventType, callback);
        m_Event[eventType] = (BattleCallBack)m_Event[eventType] - callback;
        OnEventRemove(eventType);
    }

    public static void RemoveListener<T>(BattleEventDefine eventType, BattleCallBack<T> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_Event[eventType] = (BattleCallBack<T>)m_Event[eventType] - callback;
        OnEventRemove(eventType);
    }

    public static void RemoveListener<T, U>(BattleEventDefine eventType, BattleCallBack<T, U> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_Event[eventType] = (BattleCallBack<T, U>)m_Event[eventType] - callback;
        OnEventRemove(eventType);
    }
    public static void RemoveListener<T, U, V>(BattleEventDefine eventType, BattleCallBack<T, U,V> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_Event[eventType] = (BattleCallBack<T, U, V>)m_Event[eventType] - callback;
        OnEventRemove(eventType);
    }

    public static void Broadcast(BattleEventDefine eventType)
    {
        Delegate d;
        if (m_Event.TryGetValue(eventType, out d))
        {
            BattleCallBack callBack = (BattleCallBack)d;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("广播事件{0}错误", eventType));
            }
        }
    }

    public static void Broadcast<T>(BattleEventDefine eventType, T arg)
    {
        Delegate d;
        if (m_Event.TryGetValue(eventType, out d))
        {
            BattleCallBack<T> callBack = (BattleCallBack<T>)d;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件{0}错误", eventType));
            }
        }
    }

    public static void Broadcast<T, U>(BattleEventDefine eventType, T arg1, U arg2)
    {
        Delegate d;
        if (m_Event.TryGetValue(eventType, out d))
        {
            BattleCallBack<T, U> callBack = (BattleCallBack<T, U>)d;
            if (callBack != null)
            {
                callBack(arg1, arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件{0}错误", eventType));
            }
        }
    }

    public static void Broadcast<T, U , V>(BattleEventDefine eventType, T arg1, U arg2 , V arg3)
    {
        Delegate d;
        if (m_Event.TryGetValue(eventType, out d))
        {
            BattleCallBack<T, U, V> callBack = (BattleCallBack<T, U,V>)d;
            if (callBack != null)
            {
                callBack(arg1, arg2 , arg3);
            }
            else
            {
                throw new Exception(string.Format("广播事件{0}错误", eventType));
            }
        }
    }

}