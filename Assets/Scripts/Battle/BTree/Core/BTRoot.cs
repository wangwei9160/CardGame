using System.Collections.Generic;
using System;

public class BTBlackboard
{
    private readonly Dictionary<string, object> _data = new Dictionary<string, object>();
    private readonly Dictionary<string, Type> _typeInfo = new Dictionary<string, Type>();

    public void Set<T>(string key, T value)
    {
        _data[key] = value;
        _typeInfo[key] = typeof(T);
    }

    public T Get<T>(string key, T defaultValue = default)
    {
        if (_data.TryGetValue(key, out var data) && data is T typedValue)
        {
            return typedValue;
        }
        return defaultValue;
    }

    public bool Remove(string key)
    {
        bool removedData = _data.Remove(key);
        bool removedType = _typeInfo.Remove(key);
        return removedData || removedType;
    }

    public void Clear()
    {
        _data.Clear();
        _typeInfo.Clear();
    }

    public IEnumerable<string> Keys => _data.Keys;

    public int Count => _data.Count;
}

public class BTRoot
{
    private BTNode _root = null;
    protected BTBlackboard Blackboard { get; private set; }

    public BTRoot(BTNode root)
    {
        _root = root;
    }

    public BTRoot(BTNode root, BTBlackboard blackboard)
    {
        _root = root;
        _root.parent = this;
        Blackboard = blackboard;
    }

    public void InitBlackboard(BTBlackboard blackboard)
    {
        Blackboard = blackboard;
    }

    public void SetParameter(string key, object value)
    {
        Blackboard.Set(key, value);
    }

    public T GetParameter<T>(string key, T value)
    {
        return Blackboard.Get<T>(key, value);
    }

    public void ClearParameter()
    {
        Blackboard?.Clear();
    }

    protected void Start()
    {
        _root = BTreeHelper.SetUpBTree();
    }

    public void OnUpdate()
    {
        while(true)
        {
            var state = _root?.OnUpdate();
            if (state != null)
            {
                if(state == BTNodeState.SUCCESS || state == BTNodeState.FAILURE)
                {
                    return;
                }
            }
        }
    }
}