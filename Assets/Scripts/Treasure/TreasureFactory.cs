using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class TreasureFactory
{
    private static Dictionary<int, Type> _typeMap;
    private static Dictionary<int, TreasureBase> _instanceMap;
    private static bool _isInitialized = false;

    private static readonly object _lock = new object();

    private static void InitializeIfNeeded()
    {
        if (!_isInitialized)
        {
            lock (_lock)
            {
                if (!_isInitialized) // double check
                {
                    _typeMap = new Dictionary<int, Type>();
                    _instanceMap = new Dictionary<int, TreasureBase>();
                    AutoRegisterTreasures();
                    _isInitialized = true;
                }
            }
        }
    }

    private static void AutoRegisterTreasures()
    {
        Type itemType = typeof(TreasureBase);
        Assembly assembly = Assembly.GetAssembly(itemType);
        Type[] allTypes = assembly.GetTypes();

        foreach (Type type in allTypes)
        {
            if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(itemType))
            {
                TreasureBase instance = (TreasureBase)Activator.CreateInstance(type);
                RegisterTreasure(instance, type);
            }
        }
    }

    private static void RegisterTreasure(TreasureBase instance, Type type)
    {
        int id = instance.ID;
        if (!_typeMap.ContainsKey(id))
        {
            _typeMap[id] = type;
        }
        else
        {
            Debug.LogError($"遗物Treasure = {id} 重复, {_typeMap[id]} 和 {type}");
        }

        if (!_instanceMap.ContainsKey(id))
        {
            _instanceMap[id] = instance;
            Debug.Log($"注册Treasure {id}");
        }
    }

    public static TreasureBase GetTreasure(int id)
    {
        InitializeIfNeeded();
        if (_instanceMap.TryGetValue(id, out TreasureBase treasure))
        {
            return treasure;
        }
        Debug.LogError($"找不到ID为 {id} 的Treasure");
        return null;
    }

    public static Type GetTreasureType(int id)
    {
        InitializeIfNeeded();
        if (_typeMap.TryGetValue(id, out Type type))
        {
            return type;
        }
        Debug.LogError($"找不到ID为 {id} 的Treasure类型");
        return null;
    }
}