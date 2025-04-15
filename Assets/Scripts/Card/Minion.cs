using UnityEngine;
using System.Collections.Generic;

// 随从类
public class Minion : MonoBehaviour
{
    // 随从状态
    public bool canAttack = false;  // 是否可以攻击
    public bool hasAttacked = false;  // 是否已经攻击
    
    // 效果列表
    private List<CardEffect> turnStartEffects = new List<CardEffect>();
    private List<CardEffect> turnEndEffects = new List<CardEffect>();
    private List<CardEffect> passiveEffects = new List<CardEffect>();
    
    // 属性系统
    private CardAttribute attributes;
    
    void Awake()
    {
        // 获取或添加属性组件
        attributes = GetComponent<CardAttribute>();
        if (attributes == null)
        {
            attributes = gameObject.AddComponent<CardAttribute>();
        }
    }
    
    // 初始化随从
    public void Initialize(int attack, int health)
    {
        // 设置基础属性
        attributes.AddAttributeValue(AttributeType.Vitality, health, true);
        
        // 设置攻击力（这里可以根据需要添加攻击力属性）
        // TODO: 添加攻击力属性类型
        
        canAttack = false;
        hasAttacked = false;
        
        // 更新UI
        UpdateUI();
    }
    
    // 受到伤害
    public void TakeDamage(int damage)
    {
        attributes.TakeDamage(damage, true, false);
        UpdateUI();
    }
    
    // 受到魔法伤害
    public void TakeMagicDamage(int damage)
    {
        attributes.TakeDamage(damage, false, true);
        UpdateUI();
    }
    
    // 治疗
    public void Heal(int amount)
    {
        attributes.AddAttributeValue(AttributeType.Vitality, amount, false, true);
        UpdateUI();
    }
    
    // 增加攻击力
    public void AddAttack(int amount)
    {
        // TODO: 实现攻击力属性的增加
        UpdateUI();
    }
    
    // 增加生命值
    public void AddHealth(int amount)
    {
        attributes.AddAttributeValue(AttributeType.Vitality, amount, false, true);
        UpdateUI();
    }
    
    // 添加护盾
    public void AddShield(int amount)
    {
        attributes.AddAttributeValue(AttributeType.Shield, amount, false, true);
        UpdateUI();
    }
    
    // 死亡
    private void OnDeath()
    {
        // 从战场移除
        CardBattleManager.Instance.RemoveMinionFromBattlefield(gameObject);
        
        // 销毁随从
        Destroy(gameObject);
        
        Debug.Log("随从死亡");
    }
    
    // 攻击目标
    public void Attack(GameObject target)
    {
        if (!canAttack || hasAttacked)
        {
            Debug.Log("随从无法攻击");
            return;
        }
        
        // 获取目标随从组件
        Minion targetMinion = target.GetComponent<Minion>();
        if (targetMinion != null)
        {
            // TODO: 使用攻击力属性值
            int attackValue = 1; // 临时使用固定值
            
            // 对目标造成伤害
            targetMinion.TakeDamage(attackValue);
            
            // 受到目标的反击伤害
            TakeDamage(targetMinion.GetAttackValue());
            
            // 标记已攻击
            hasAttacked = true;
            
            Debug.Log("随从攻击目标");
        }
    }
    
    // 获取攻击力
    public int GetAttackValue()
    {
        // TODO: 返回攻击力属性值
        return 1; // 临时返回固定值
    }
    
    // 获取当前生命值
    public int GetHealth()
    {
        return attributes.GetAttributeTotal(AttributeType.Vitality);
    }
    
    // 获取护盾值
    public int GetShield()
    {
        return attributes.GetAttributeTotal(AttributeType.Shield);
    }
    
    // 回合开始时重置状态
    public void ResetTurnState()
    {
        canAttack = true;
        hasAttacked = false;
        
        // 触发回合开始效果
        TriggerTurnStartEffects();
        
        // 更新UI
        UpdateUI();
    }
    
    // 回合结束时的处理
    public void OnTurnEnd()
    {
        // 触发回合结束效果
        TriggerTurnEndEffects();
        
        // 清理临时属性
        attributes.ClearTemporaryAttributes();
        
        // 更新UI
        UpdateUI();
    }
    
    // 添加回合开始效果
    public void AddTurnStartEffect(CardEffect effect)
    {
        turnStartEffects.Add(effect);
    }
    
    // 添加回合结束效果
    public void AddTurnEndEffect(CardEffect effect)
    {
        turnEndEffects.Add(effect);
    }
    
    // 添加被动效果
    public void AddPassiveEffect(CardEffect effect)
    {
        passiveEffects.Add(effect);
    }
    
    // 触发回合开始效果
    public void TriggerTurnStartEffects()
    {
        foreach (CardEffect effect in turnStartEffects)
        {
            effect.Trigger(gameObject);
        }
    }
    
    // 触发回合结束效果
    public void TriggerTurnEndEffects()
    {
        foreach (CardEffect effect in turnEndEffects)
        {
            effect.Trigger(gameObject);
        }
    }
    
    // 触发被动效果
    public void TriggerPassiveEffects(GameObject target = null)
    {
        foreach (CardEffect effect in passiveEffects)
        {
            effect.Trigger(gameObject, target);
        }
    }
    
    // 更新UI
    private void UpdateUI()
    {
        // 这里可以添加更新UI的代码
        // 例如更新生命值、攻击力等显示
    }
} 