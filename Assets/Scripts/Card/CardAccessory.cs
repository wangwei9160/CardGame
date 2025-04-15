using UnityEngine;
using System.Collections.Generic;

public class CardAccessory : MonoBehaviour
{
    // 配件基本信息
    public string accessoryName;        // 配件名称
    public string description;          // 配件描述
    public int manaCost;               // 法力消耗
    public int attackBonus;            // 攻击力加成
    public int healthBonus;            // 生命值加成
    public int shieldBonus;            // 护盾加成
    public int energyBonus;            // 能量加成
    public int buffBonus;              // 增益加成
    public int debuffBonus;            // 减益加成
    public Sprite icon;

    // 配件效果
    protected List<CardEffect> effects = new List<CardEffect>();

    // 配件状态
    private bool isAttached = false;   // 是否已装配
    private Card attachedCard = null;  // 装配的卡牌

    // 初始化配件
    public virtual void Initialize(string name, string description, int manaCost)
    {
        this.accessoryName = name;
        this.description = description;
        this.manaCost = manaCost;
    }

    // 装配到卡牌上
    public bool AttachToCard(Card card)
    {
        if (isAttached || card == null)
            return false;

        // 检查卡牌是否已有三个配件
        if (card.GetAccessoryCount() >= 3)
            return false;

        // 装配配件
        isAttached = true;
        attachedCard = card;
        card.AddAccessory(this);

        // 应用配件效果
        ApplyEffects(card);

        return true;
    }

    // 从卡牌上移除
    public bool DetachFromCard()
    {
        if (!isAttached || attachedCard == null)
            return false;

        // 移除配件效果
        RemoveEffects(attachedCard);

        // 从卡牌上移除配件
        attachedCard.RemoveAccessory(this);
        isAttached = false;
        attachedCard = null;

        return true;
    }

    // 应用配件效果
    private void ApplyEffects(Card card)
    {
        // 增加法力消耗
        card.manaCost += manaCost;

        // 增加属性值
        card.attackValue += attackBonus;
        card.healthValue += healthBonus;
        card.shieldValue += shieldBonus;
        card.energyValue += energyBonus;
        card.buffValue += buffBonus;
        card.debuffValue += debuffBonus;

        // 添加特殊效果
        foreach (CardEffect effect in effects)
        {
            card.AddEffect(effect);
        }
    }

    // 移除配件效果
    private void RemoveEffects(Card card)
    {
        // 减少法力消耗
        card.manaCost -= manaCost;

        // 减少属性值
        card.attackValue -= attackBonus;
        card.healthValue -= healthBonus;
        card.shieldValue -= shieldBonus;
        card.energyValue -= energyBonus;
        card.buffValue -= buffBonus;
        card.debuffValue -= debuffBonus;

        // 移除特殊效果
        foreach (CardEffect effect in effects)
        {
            card.RemoveEffect(effect);
        }
    }

    // 获取配件状态
    public bool IsAttached()
    {
        return isAttached;
    }

    // 获取装配的卡牌
    public Card GetAttachedCard()
    {
        return attachedCard;
    }

    // 添加效果
    public void AddEffect(string effectName, string effectDescription, EffectType type, BasicAction action, int value)
    {
        CardEffect effect = null;
        switch (type)
        {
            case EffectType.Active:
                effect = new ActiveEffect(effectName, effectDescription, action, value);
                break;
            case EffectType.Passive:
                effect = new PassiveEffect(effectName, effectDescription, action, value);
                break;
            case EffectType.TurnStart:
                effect = new TurnStartEffect(effectName, effectDescription, action, value);
                break;
            case EffectType.TurnEnd:
                effect = new TurnEndEffect(effectName, effectDescription, action, value);
                break;
        }
        if (effect != null)
        {
            effects.Add(effect);
        }
    }

    // 移除效果
    public void RemoveEffect(string effectName)
    {
        effects.RemoveAll(e => e.effectName == effectName);
    }

    // 获取所有效果
    public List<CardEffect> GetEffects()
    {
        return new List<CardEffect>(effects);
    }

    // 触发所有效果
    public virtual void TriggerEffects(GameObject target)
    {
        if (target != null)
        {
            foreach (CardEffect effect in effects)
            {
                effect.Trigger(gameObject, target);
            }
        }
    }

    // 当配件被装备时触发
    public virtual void OnEquip(GameObject target)
    {
        if (target != null)
        {
            Debug.Log($"配件 {accessoryName} 被装备到卡牌 {target.name} 上");
            TriggerEffects(target);
        }
    }

    // 当配件被卸下时触发
    public virtual void OnUnequip(GameObject target)
    {
        if (target != null)
        {
            Debug.Log($"配件 {accessoryName} 从卡牌 {target.name} 上卸下");
        }
    }
} 