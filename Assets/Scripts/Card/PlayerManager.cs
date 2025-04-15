using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家管理器
public class PlayerManager : MonoBehaviour
{
    // 单例模式
    public static PlayerManager Instance { get; private set; }
    
    // 玩家属性
    public int maxHealth = 30;         // 最大生命值
    public int currentHealth;           // 当前生命值
    public int maxMana = 10;            // 最大法力值
    public int currentMana;             // 当前法力值
    public int maxManaPerTurn = 10;     // 每回合最大法力值 
    public int attackValue = 0;         // 玩家攻击力
    public int attackClass = 1;
    public int healthClass = 2;
    public int manaClass = 3;
    // 玩家状态
    public bool isPlayerTurn = false;   // 是否是玩家回合
    
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
        
        // 初始化玩家属性
        InitializePlayer();
    }
    
    // 初始化玩家
    private void InitializePlayer()
    {
        currentHealth = maxHealth;
        currentMana = 0;
        
        // 初始化玩家卡牌
        CardManager.Instance.InitializePlayerCards();
    }
    
    // 回合开始时
    public void StartTurn()
    {
        isPlayerTurn = true;
        
        // 增加法力值
        AddMana(1);
        
        // 抽牌
        CardManager.Instance.DrawCardsForTurn();
        
        // 重置固有牌状态
        CardManager.Instance.ResetEquipmentCards();
        
        // 重置随从状态
        ResetMinions();
        
        Debug.Log("玩家回合开始");
    }
    
    // 回合结束时
    public void EndTurn()
    {
        isPlayerTurn = false;
        
        // 清空法力值
        currentMana = 0;
        
        Debug.Log("玩家回合结束");
    }
    
    // 受到伤害
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Die();
        }
        
        // 更新UI
        UpdateUI();
    }
    
    // 治疗
    public void Heal(int amount)
    {
        currentHealth += amount;
        
        // 确保不超过最大生命值
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        // 更新UI
        UpdateUI();
    }
    
    // 增加法力值
    public void AddMana(int amount)
    {
        currentMana += amount;
        
        // 更新UI
        UpdateUI();
    }
    
    // 消耗法力值
    public void SpendMana(int amount)
    {
        currentMana -= amount;
        
        // 确保不小于0
        if (currentMana < 0)
        {
            currentMana = 0;
        }
        
        // 更新UI
        UpdateUI();
    }

    // 增加攻击力
    public void AddAttack(int amount)
    {
        // 增加玩家攻击力
        attackValue += amount;

        // 更新UI
        UpdateUI(); 
    }
 
    public void AddBonus(int amount, int bonusClass)
    {
        // 增加玩家属性
        if (bonusClass == attackClass)
        {
            attackValue += amount;
        }
        else if (bonusClass == healthClass)
        {
            currentHealth += amount;
        }
        else if (bonusClass == manaClass){
            currentMana += amount;
        }
        // 更新UI
    }   
    
    // 死亡
    private void Die()
    {
        Debug.Log("玩家死亡");
        // 可以在这里添加游戏结束的逻辑
    }
    
    // 重置随从状态
    private void ResetMinions()
    {
        List<GameObject> minions = CardBattleManager.Instance.GetPlayerMinions();
        
        foreach (GameObject minion in minions)
        {
            Minion minionComponent = minion.GetComponent<Minion>();
            if (minionComponent != null)
            {
                minionComponent.ResetTurnState();
            }
        }
    }
    
    // 更新UI
    private void UpdateUI()
    {
        // 这里可以添加更新UI的代码
        // 例如更新生命值、法力值等显示
    }
} 