using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡牌基类
public class Card : MonoBehaviour
{
    // 卡牌基本信息
    public string cardName;            // 卡牌名称
    public string description;         // 卡牌描述
    public int manaCost;              // 法力消耗
    public Sprite cardImage;           // 卡牌图像
    public AttributeType leftAttribute;  // 左侧属性类型
    public AttributeType rightAttribute; // 右侧属性类型

    // 卡牌属性值
    public int attackValue;           // 攻击力
    public int healthValue;           // 生命值
    public int shieldValue;           // 护盾值
    public int energyValue;           // 能量值
    public int drawValue;             // 抽牌值
    public int discardValue;          // 弃牌值
    public int damageValue;           // 伤害值
    public int healingValue;          // 治疗值
    public int buffValue;             // 增益值
    public int debuffValue;           // 减益值

    // 配件系统
    private List<CardAccessory> accessories = new List<CardAccessory>();  // 已装配的配件列表
    private bool isInBattle = false;  // 是否在战斗中

    // 卡牌状态
    protected bool isPlayable = true;  // 是否可以使用
    
    // 卡牌效果
    public List<CardEffect> activeEffects = new List<CardEffect>();      // 主动效果
    public List<CardEffect> passiveEffects = new List<CardEffect>();     // 被动效果
    public List<CardEffect> turnStartEffects = new List<CardEffect>();   // 回合开始时效果
    public List<CardEffect> turnEndEffects = new List<CardEffect>();     // 回合结束时效果
    
    // 属性系统
    protected CardAttribute attributes;
    public virtual int ID { get; protected set; } = 1;
    public Test0Class cardCfg => Test0Manager.GetTest0ClassByKey(ID);
    
    void Awake()
    {
        // 获取或添加属性组件
        attributes = GetComponent<CardAttribute>();
        if (attributes == null)
        {
            attributes = gameObject.AddComponent<CardAttribute>();
        }
    }
    
    // 初始化卡牌
    public virtual void Initialize(string name, string desc, int mana, Sprite image)
    {
        cardName = name;
        description = desc;
        manaCost = mana;
        cardImage = image;
    }
    
    // 设置基础属性
    public void SetBaseAttributes(AttributeType left, AttributeType right)
    {
        leftAttribute = left;
        rightAttribute = right;
    }
    
    // 添加配件
    public void AddAccessory(CardAccessory accessory)
    {
        if (isInBattle)
            return;

        if (accessories.Count < 3)
        {
            accessories.Add(accessory);
            Debug.Log($"配件 {accessory.accessoryName} 已装配到卡牌 {cardName} 上");
        }
    }
    
    // 移除配件
    public void RemoveAccessory(CardAccessory accessory)
    {
        if (isInBattle)
            return;

        if (accessories.Contains(accessory))
        {
            accessories.Remove(accessory);
            Debug.Log($"配件 {accessory.accessoryName} 已从卡牌 {cardName} 上移除");
        }
    }
    
    // 获取配件数量
    public int GetAccessoryCount()
    {
        return accessories.Count;
    }
    
    // 获取配件列表
    public List<CardAccessory> GetAccessories()
    {
        return new List<CardAccessory>(accessories);
    }
    
    // 进入战斗
    public void EnterBattle()
    {
        isInBattle = true;
    }
    
    // 离开战斗
    public void LeaveBattle()
    {
        isInBattle = false;
    }
    
    // 检查是否在战斗中
    public bool IsInBattle()
    {
        return isInBattle;
    }
    
    // 添加属性值
    public virtual void AddAttributeValue(AttributeType type, int value, bool isInherent = false, bool isTemporary = true)
    {
        attributes.AddAttributeValue(type, value, isInherent, isTemporary);
    }
    
    // 获取属性值
    public virtual int GetAttributeValue(AttributeType type)
    {
        return attributes.GetAttributeTotal(type);
    }
    
    // 受到伤害
    public virtual void TakeDamage(int damage, bool isSwordDamage, bool isMagicDamage)
    {
        attributes.TakeDamage(damage, isSwordDamage, isMagicDamage);
    }
    
    // 添加效果
    public void AddEffect(CardEffect effect)
    {
        switch (effect.effectType)
        {
            case EffectType.Active:
                activeEffects.Add(effect);
                break;
            case EffectType.Passive:
                passiveEffects.Add(effect);
                break;
            case EffectType.TurnStart:
                turnStartEffects.Add(effect);
                break;
            case EffectType.TurnEnd:
                turnEndEffects.Add(effect);
                break;
        }
        Debug.Log($"卡牌 {cardName} 添加效果: {effect.effectName}");
    }
    
    // 移除效果
    public void RemoveEffect(CardEffect effect)
    {
        switch (effect.effectType)
        {
            case EffectType.Active:
                activeEffects.Remove(effect);
                break;
            case EffectType.Passive:
                passiveEffects.Remove(effect);
                break;
            case EffectType.TurnStart:
                turnStartEffects.Remove(effect);
                break;
            case EffectType.TurnEnd:
                turnEndEffects.Remove(effect);
                break;
        }
        Debug.Log($"卡牌 {cardName} 移除效果: {effect.effectName}");
    }
    
    // 使用卡牌
    public virtual bool OnPlay()
    {
        // 检查法力值是否足够

        
        // 扣除法力值

        
        // 触发卡牌效果
        TriggerEffects();
        
        return true;
    }
    
    // 触发卡牌效果
    protected virtual void TriggerEffects()
    {
        // 这里可以添加效果触发逻辑
        Debug.Log($"触发卡牌 {cardName} 的效果");
    }
    
    // 回合开始时触发效果
    public virtual void OnTurnStart()
    {
        foreach (CardEffect effect in turnStartEffects)
        {
            effect.Trigger(gameObject);
        }
    }
    
    // 回合结束时触发效果
    public virtual void OnTurnEnd()
    {
        foreach (CardEffect effect in turnEndEffects)
        {
            effect.Trigger(gameObject);
        }
        
        // 清理临时属性
        attributes.ClearTemporaryAttributes();
    }
    
    // 触发被动效果
    public virtual void OnPassiveEffect(GameObject target = null)
    {
        foreach (CardEffect effect in passiveEffects)
        {
            effect.Trigger(gameObject, target);
        }
    }
    
    // 设置卡牌是否可用
    public void SetPlayable(bool playable)
    {
        isPlayable = playable;
    }
    
    // 获取卡牌是否可用
    public bool IsPlayable()
    {
        return isPlayable;
    }
    
    // 死亡处理
    protected virtual void OnDeath()
    {
        Debug.Log($"卡牌 {cardName} 已死亡，进入墓地");
        // TODO: 实现进入墓地的具体逻辑
    }
} 