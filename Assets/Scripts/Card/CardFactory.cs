using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class CardFactory
{
    private static Dictionary<int, Type> _typeMap;
    private static Dictionary<int, Card> _instanceMap;
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
                    _instanceMap = new Dictionary<int, Card>();
                    AutoRegisterCards();
                    _isInitialized = true;
                }
            }
        }
    }

    private static void AutoRegisterCards()
    {
        Type itemType = typeof(Card);
        Assembly assembly = Assembly.GetAssembly(itemType);
        Type[] allTypes = assembly.GetTypes();

        foreach (Type type in allTypes)
        {
            if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(itemType))
            {
                Card instance = (Card)Activator.CreateInstance(type);
                RegisterCard(instance, type);
            }
        }
    }

    private static void RegisterCard(Card instance, Type type)
    {
        int id = instance.ID;
        if (!_typeMap.ContainsKey(id))
        {
            _typeMap[id] = type;
        }
        else
        {
            Debug.LogError($"卡牌ID = {id} 重复, {_typeMap[id]} 和 {type}");
        }

        if (!_instanceMap.ContainsKey(id))
        {
            _instanceMap[id] = instance;
            Debug.Log($"注册卡牌 {id}");
        }
    }

    public static Card GetCard(int id)
    {
        InitializeIfNeeded();
        if (_instanceMap.TryGetValue(id, out Card card))
        {
            return card;
        }
        Debug.LogError($"找不到ID为 {id} 的卡牌");
        return null;
    }

    public static Type GetCardType(int id)
    {
        InitializeIfNeeded();
        if (_typeMap.TryGetValue(id, out Type type))
        {
            return type;
        }
        Debug.LogError($"找不到ID为 {id} 的卡牌类型");
        return null;
    }
}