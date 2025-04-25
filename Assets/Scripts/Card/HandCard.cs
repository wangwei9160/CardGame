using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 手牌基类
public class HandCard : Card
{
    // 手牌特有属性
    public bool isTargetRequired = false;  // 是否需要目标
    public List<GameObject> validTargets = new List<GameObject>();  // 有效目标列表
    
    // 初始化手牌
    public override void Initialize(string name, string desc, int cost, Sprite image)
    {
        base.Initialize(name, desc, cost, image);
    }
    
    // 设置是否需要目标
    public void SetTargetRequired(bool required)
    {
        isTargetRequired = required;
    }
    
    // 添加有效目标
    public void AddValidTarget(GameObject target)
    {
        if (!validTargets.Contains(target))
        {
            validTargets.Add(target);
        }
    }
    
    // 移除有效目标
    public void RemoveValidTarget(GameObject target)
    {
        if (validTargets.Contains(target))
        {
            validTargets.Remove(target);
        }
    }
    
    // 清空有效目标列表
    public void ClearValidTargets()
    {
        validTargets.Clear();
    }
    
    // 检查目标是否有效
    public bool IsValidTarget(GameObject target)
    {
        return validTargets.Contains(target);
    }
    
    // 手牌使用效果（带目标）
    public virtual bool OnPlayWithTarget(GameObject target)
    {
        // 检查是否需要目标
        if (isTargetRequired && target == null)
        {
            Debug.Log("该卡牌需要目标");
            return false;
        }
        
        // 检查目标是否有效
        if (isTargetRequired && !IsValidTarget(target))
        {
            Debug.Log("无效的目标");
            return false;
        }
        
        // 检查是否有足够的法力值

        
        // 扣除法力值

        
        // 触发主动效果
        bool success = false;
        foreach (CardEffect effect in activeEffects)
        {
            if (effect.Trigger(gameObject, target))
            {
                success = true;
            }
        }
        
        return success;
    }
    
    // 手牌使用效果（无目标）
    public virtual bool OnPlay()
    {
        // 检查是否需要目标
        if (isTargetRequired)
        {
            Debug.Log("该卡牌需要目标");
            return false;
        }
        
        // 检查是否有足够的法力值

        
        // 扣除法力值

        
        // 触发主动效果
        bool success = false;
        foreach (CardEffect effect in activeEffects)
        {
            if (effect.Trigger(gameObject))
            {
                success = true;
            }
        }
        
        return success;
    }
}

// 随从牌类
public class MinionCard : HandCard
{
    // 随从特有属性
    public int attackValue;        // 攻击力
    public int healthValue;        // 生命值
    public GameObject minionPrefab;  // 随从预制体
    
    // 初始化随从牌
    public void InitializeMinion(string name, string desc, int cost, Sprite image, int attack, int health, GameObject prefab)
    {
        Initialize(name, desc, cost, image);
        attackValue = attack;
        healthValue = health;
        minionPrefab = prefab;
        isTargetRequired = true;  // 随从牌需要目标位置
    }
    
    // 随从牌使用效果
    public override bool OnPlayWithTarget(GameObject target)
    {
        if (base.OnPlayWithTarget(target))
        {
            // 检查战场空间
            if (!CardBattleManager.Instance.HasAvailableSpace())
            {
                Debug.Log("战场空间不足,无法放置随从");
                return false;
            }
            
            // 在目标位置生成随从
            Vector3 spawnPosition = target.transform.position;
            GameObject minion = Instantiate(minionPrefab, spawnPosition, Quaternion.identity);
            
            // 设置随从属性
            Minion minionComponent = minion.GetComponent<Minion>();
            if (minionComponent != null)
            {
                minionComponent.Initialize(attackValue, healthValue);
                
                // 将随从的效果添加到随从组件
                foreach (CardEffect effect in turnStartEffects)
                {
                    minionComponent.AddTurnStartEffect(effect);
                }
                
                foreach (CardEffect effect in turnEndEffects)
                {
                    minionComponent.AddTurnEndEffect(effect);
                }
                
                foreach (CardEffect effect in passiveEffects)
                {
                    minionComponent.AddPassiveEffect(effect);
                }
            }
            
            // 将随从添加到战场
            CardBattleManager.Instance.AddMinionToBattlefield(minion);
            
            Debug.Log("放置随从: " + cardName);
            return true;
        }
        return false;
    }
}

// 法术牌类
public class SpellCard : HandCard
{
    // 法术特有属性
    public string spellEffect;     // 法术效果描述
    public bool isAreaEffect = false;  // 是否是范围效果
    
    // 初始化法术牌
    public void InitializeSpell(string name, string desc, int cost, Sprite image, string effect, bool isArea = false)
    {
        Initialize(name, desc, cost, image);
        spellEffect = effect;
        isAreaEffect = isArea;
        isTargetRequired = !isArea;  // 范围效果不需要目标
    }
    
    // 法术牌使用效果
    public override bool OnPlayWithTarget(GameObject target)
    {
        if (base.OnPlayWithTarget(target))
        {
            // 法术效果处理
            Debug.Log("释放法术: " + cardName);
            
            if (isAreaEffect)
            {
                // 范围效果处理
                Debug.Log("范围效果: " + spellEffect);
                // 可以在这里添加范围效果的具体实现
            }
            else
            {
                // 单体效果处理
                Debug.Log("对目标 " + target.name + " 释放效果: " + spellEffect);
                // 可以在这里添加单体效果的具体实现
            }
            
            return true;
        }
        return false;
    }
    
    // 法术牌使用效果（无目标）
    public override bool OnPlay()
    {
        if (isAreaEffect)
        {
            return OnPlayWithTarget(null);
        }
        else
        {
            Debug.Log("该法术需要目标");
            return false;
        }
    }
} 