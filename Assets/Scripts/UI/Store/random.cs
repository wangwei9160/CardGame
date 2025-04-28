using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{
    private List<TreasureClass> rareTreasures = new List<TreasureClass>();
    private List<TreasureClass> commonTreasures = new List<TreasureClass>();

    private void Awake()
    {
        InitializeTreasureLists();
    }

    public void InitializeTreasureLists()
    {
        var allTreasures = TreasureConfig.GetAll();
        foreach (var treasure in allTreasures)
        {
            if (treasure.Type == 1)
                rareTreasures.Add(treasure);
            else if (treasure.Type == 2)
                commonTreasures.Add(treasure);
        }

        Debug.Log($"初始化宝物列表完成 - 稀有宝物: {rareTreasures.Count}, 普通宝物: {commonTreasures.Count}");
    }

    // 方法一：50%概率稀有，50%概率普通
    public TreasureClass GetRandomTreasure()
    {
        // 确保列表已初始化
        if (rareTreasures.Count == 0 && commonTreasures.Count == 0)
        {
            InitializeTreasureLists();
        }

        // 50%概率选择稀有宝物，50%概率选择普通宝物
        if (Random.value < 0.5f)
        {
            if (rareTreasures.Count > 0)
                return rareTreasures[Random.Range(0, rareTreasures.Count)];
        }
        else
        {
            if (commonTreasures.Count > 0)
                return commonTreasures[Random.Range(0, commonTreasures.Count)];
        }
        
        Debug.LogError("没有可用的宝物");
        return null;
    }

    // 方法二：只随机普通宝物
    public TreasureClass GetRandomCommonTreasure()
    {
        // 确保列表已初始化
        if (commonTreasures.Count == 0)
        {
            InitializeTreasureLists();
        }

        if (commonTreasures.Count > 0)
            return commonTreasures[Random.Range(0, commonTreasures.Count)];
        
        Debug.LogError("没有可用的普通宝物");
        return null;
    }

    // 方法三： 只随机稀有宝物
    public TreasureClass GetRandomRareTreasure()
    {
        if(rareTreasures.Count == 0)
        {
            InitializeTreasureLists();
        }

        if(rareTreasures.Count > 0)
            return rareTreasures[Random.Range(0, rareTreasures.Count)];
        
        Debug.LogError("没有可用的稀有宝物");
        return null;
    }   
}
