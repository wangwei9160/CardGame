using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 战场管理器
public class CardBattleManager : MonoBehaviour
{
    // 单例模式
    public static CardBattleManager Instance { get; private set; }
    
    // 战场设置
    public int maxBattlefieldSize = 5;  // 战场最大空间
    public float minionSpacing = 1.5f;   // 随从之间的间距
    
    // 战场上的随从
    private List<GameObject> playerMinions = new List<GameObject>();  // 玩家随从
    private List<GameObject> enemyMinions = new List<GameObject>();   // 敌人随从
    
    // 主角
    public GameObject playerCharacter;  // 玩家主角
    
    // 战场位置
    private Vector3[] battlefieldPositions;  // 战场位置数组
    
    private void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // 初始化战场位置
        InitializeBattlefieldPositions();
    }
    
    // 初始化战场位置
    private void InitializeBattlefieldPositions()
    {
        battlefieldPositions = new Vector3[maxBattlefieldSize];
        
        // 设置战场位置
        for (int i = 0; i < maxBattlefieldSize; i++)
        {
            // 从右到左排列，主角在最左边
            float xPos = 5f - (i * minionSpacing);
            battlefieldPositions[i] = new Vector3(xPos, 0f, 0f);
        }
    }
    
    // 添加随从到战场
    public void AddMinionToBattlefield(GameObject minion)
    {
        // 检查战场空间
        if (playerMinions.Count >= maxBattlefieldSize)
        {
            Debug.Log("战场空间不足，无法放置随从");
            return;
        }
        
        // 添加到玩家随从列表
        playerMinions.Add(minion);
        
        // 重新排列随从位置
        RearrangeMinions();
    }
    
    // 从战场移除随从
    public void RemoveMinionFromBattlefield(GameObject minion)
    {
        if (playerMinions.Contains(minion))
        {
            playerMinions.Remove(minion);
            
            // 重新排列随从位置
            RearrangeMinions();
            
            Debug.Log("随从从战场移除");
        }
    }
    
    // 重新排列随从位置
    private void RearrangeMinions()
    {
        // 主角位置固定在最左边
        if (playerCharacter != null)
        {
            playerCharacter.transform.position = new Vector3(-5f, 0f, 0f);
        }
        
        // 重新排列随从位置
        for (int i = 0; i < playerMinions.Count; i++)
        {
            if (playerMinions[i] != null)
            {
                // 从右到左排列，主角在最左边
                float xPos = 5f - (i * minionSpacing);
                playerMinions[i].transform.position = new Vector3(xPos, 0f, 0f);
            }
        }
    }
    
    // 在指定位置插入随从
    public bool InsertMinionAtPosition(GameObject minion, int position)
    {
        // 检查战场空间
        if (playerMinions.Count >= maxBattlefieldSize)
        {
            Debug.Log("战场空间不足，无法放置随从");
            return false;
        }
        
        // 检查位置是否有效
        if (position < 0 || position > playerMinions.Count)
        {
            Debug.Log("无效的位置");
            return false;
        }
        
        // 在指定位置插入随从
        playerMinions.Insert(position, minion);
        
        // 重新排列随从位置
        RearrangeMinions();
        
        Debug.Log("在位置 " + position + " 插入随从");
        return true;
    }
    
    // 检查战场是否有可用空间
    public bool HasAvailableSpace()
    {
        return playerMinions.Count < maxBattlefieldSize;
    }
    
    // 获取战场上的随从数量
    public int GetMinionCount()
    {
        return playerMinions.Count;
    }
    
    // 获取战场上的随从列表
    public List<GameObject> GetPlayerMinions()
    {
        return playerMinions;
    }
    
    // 获取战场上的敌人随从列表
    public List<GameObject> GetEnemyMinions()
    {
        return enemyMinions;
    }
    
    // 添加敌人随从到战场
    public void AddEnemyMinion(GameObject enemyMinion)
    {
        enemyMinions.Add(enemyMinion);
        
        // 设置敌人随从位置
        float xPos = 5f + (enemyMinions.Count * minionSpacing);
        enemyMinion.transform.position = new Vector3(xPos, 0f, 0f);
    }
    
    // 从战场移除敌人随从
    public void RemoveEnemyMinion(GameObject enemyMinion)
    {
        if (enemyMinions.Contains(enemyMinion))
        {
            enemyMinions.Remove(enemyMinion);
            
            // 重新排列敌人随从位置
            for (int i = 0; i < enemyMinions.Count; i++)
            {
                if (enemyMinions[i] != null)
                {
                    float xPos = 5f + (i * minionSpacing);
                    enemyMinions[i].transform.position = new Vector3(xPos, 0f, 0f);
                }
            }
            
            Debug.Log("敌人随从从战场移除");
        }
    }
    
    // 清空战场
    public void ClearBattlefield()
    {
        // 清空玩家随从
        foreach (GameObject minion in playerMinions)
        {
            if (minion != null)
            {
                Destroy(minion);
            }
        }
        playerMinions.Clear();
        
        // 清空敌人随从
        foreach (GameObject enemy in enemyMinions)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        enemyMinions.Clear();
        
        Debug.Log("战场已清空");
    }
} 