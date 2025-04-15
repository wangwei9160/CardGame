using UnityEngine;
using System.Collections.Generic;

public class AccessoryManager : MonoBehaviour
{
    private static AccessoryManager instance;
    public static AccessoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AccessoryManager");
                instance = go.AddComponent<AccessoryManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    // 所有可用配件列表
    private List<CardAccessory> availableAccessories = new List<CardAccessory>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 创建新配件
    public CardAccessory CreateAccessory(string name, string description, int manaCost)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);
        CardAccessory accessory = go.AddComponent<CardAccessory>();
        accessory.Initialize(name, description, manaCost);
        availableAccessories.Add(accessory);
        return accessory;
    }

    // 获取所有可用配件
    public List<CardAccessory> GetAllAccessories()
    {
        return new List<CardAccessory>(availableAccessories);
    }

    // 根据名称查找配件
    public CardAccessory FindAccessoryByName(string name)
    {
        return availableAccessories.Find(a => a.accessoryName == name);
    }

    // 移除配件
    public void RemoveAccessory(CardAccessory accessory)
    {
        if (availableAccessories.Contains(accessory))
        {
            availableAccessories.Remove(accessory);
            Destroy(accessory.gameObject);
        }
    }

    // 清空所有配件
    public void ClearAllAccessories()
    {
        foreach (CardAccessory accessory in availableAccessories)
        {
            Destroy(accessory.gameObject);
        }
        availableAccessories.Clear();
    }
} 