using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 效果类型枚举
public enum EffectType
{
    Active,         // 主动效果
    Passive,        // 被动效果
    TurnStart,      // 回合开始时效果
    TurnEnd         // 回合结束时效果
}

// 基本行为枚举
public enum BasicAction
{
    Attack,         // 攻击
    Heal,           // 治疗
    Buff,           // 增益
    Debuff          // 减益
}

// 卡牌效果基类
public abstract class CardEffect
{
    // 效果基本信息
    public string effectName;          // 效果名称
    public string effectDescription;   // 效果描述
    public EffectType effectType;      // 效果类型
    public BasicAction action;         // 基本行为
    public int value;                  // 效果数值
    
    // 目标要求
    public bool requiresTarget = false;  // 是否需要目标
    public bool canTargetSelf = false;   // 是否可以以自身为目标
    public bool canTargetEnemy = false;  // 是否可以以敌人为目标
    public bool canTargetAlly = false;   // 是否可以以友方为目标
    
    // 效果触发
    public abstract bool Trigger(GameObject source, GameObject target = null);
    
    // 执行效果
    protected bool ExecuteEffect(GameObject source, GameObject target)
    {
        switch (action)
        {
            case BasicAction.Attack:
                return ExecuteAttack(source, target);
            case BasicAction.Heal:
                return ExecuteHeal(source, target);
            case BasicAction.Buff:
                return ExecuteBuff(source, target);
            case BasicAction.Debuff:
                return ExecuteDebuff(source, target);
            default:
                return false;
        }
    }
    
    // 执行攻击效果
    protected virtual bool ExecuteAttack(GameObject source, GameObject target)
    {
        if (target == null) return false;
        
        // 获取目标组件
        Minion targetMinion = target.GetComponent<Minion>();
        if (targetMinion != null)
        {
            // 对目标造成伤害
            targetMinion.TakeDamage(value);
            Debug.Log(source.name + " 对 " + target.name + " 造成 " + value + " 点伤害");
            return true;
        }
        
        return false;
    }
    
    // 执行治疗效果
    protected virtual bool ExecuteHeal(GameObject source, GameObject target)
    {
        if (target == null) return false;
        
        // 获取目标组件
        Minion targetMinion = target.GetComponent<Minion>();
        if (targetMinion != null)
        {
            // 治疗目标
            targetMinion.Heal(value);
            Debug.Log(source.name + " 治疗 " + target.name + " " + value + " 点生命值");
            return true;
        }
        
        return false;
    }
    
    // 执行增益效果
    protected virtual bool ExecuteBuff(GameObject source, GameObject target)
    {
        if (target == null) return false;
        
        // 获取目标组件
        Minion targetMinion = target.GetComponent<Minion>();
        if (targetMinion != null)
        {
            // 增加目标攻击力
            targetMinion.AddAttack(value);
            Debug.Log(source.name + " 使 " + target.name + " 攻击力增加 " + value + " 点");
            return true;
        }
        
        return false;
    }
    
    // 执行减益效果
    protected virtual bool ExecuteDebuff(GameObject source, GameObject target)
    {
        if (target == null) return false;
        
        // 获取目标组件
        Minion targetMinion = target.GetComponent<Minion>();
        if (targetMinion != null)
        {
            // 减少目标攻击力
            targetMinion.AddAttack(-value);
            Debug.Log(source.name + " 使 " + target.name + " 攻击力减少 " + value + " 点");
            return true;
        }
        
        return false;
    }
}

// 主动效果类
public class ActiveEffect : CardEffect
{
    public ActiveEffect(string name, string description, BasicAction action, int value)
    {
        this.effectName = name;
        this.effectDescription = description;
        this.effectType = EffectType.Active;
        this.action = action;
        this.value = value;
    }
    
    public override bool Trigger(GameObject source, GameObject target = null)
    {
        // 检查是否需要目标
        if (requiresTarget && target == null)
        {
            Debug.Log("该效果需要目标");
            return false;
        }
        
        // 执行效果
        return ExecuteEffect(source, target);
    }
}

// 被动效果类
public class PassiveEffect : CardEffect
{
    public PassiveEffect(string name, string description, BasicAction action, int value)
    {
        this.effectName = name;
        this.effectDescription = description;
        this.effectType = EffectType.Passive;
        this.action = action;
        this.value = value;
    }
    
    public override bool Trigger(GameObject source, GameObject target = null)
    {
        // 执行效果
        return ExecuteEffect(source, target);
    }
}

// 回合开始时效果类
public class TurnStartEffect : CardEffect
{
    public TurnStartEffect(string name, string description, BasicAction action, int value)
    {
        this.effectName = name;
        this.effectDescription = description;
        this.effectType = EffectType.TurnStart;
        this.action = action;
        this.value = value;
    }
    
    public override bool Trigger(GameObject source, GameObject target = null)
    {
        // 执行效果
        return ExecuteEffect(source, target);
    }
}

// 回合结束时效果类
public class TurnEndEffect : CardEffect
{
    public TurnEndEffect(string name, string description, BasicAction action, int value)
    {
        this.effectName = name;
        this.effectDescription = description;
        this.effectType = EffectType.TurnEnd;
        this.action = action;
        this.value = value;
    }
    
    public override bool Trigger(GameObject source, GameObject target = null)
    {
        // 执行效果
        return ExecuteEffect(source, target);
    }
}

// 效果管理器
public class Effectmanager : MonoBehaviour
{
    // 单例模式
    public static Effectmanager Instance { get; private set; }
    
    // 效果列表
    private List<CardEffect> activeEffects = new List<CardEffect>();      // 主动效果
    private List<CardEffect> passiveEffects = new List<CardEffect>();     // 被动效果
    private List<CardEffect> turnStartEffects = new List<CardEffect>();   // 回合开始时效果
    private List<CardEffect> turnEndEffects = new List<CardEffect>();     // 回合结束时效果
    
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
        
        Debug.Log("添加效果: " + effect.effectName);
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
        
        Debug.Log("移除效果: " + effect.effectName);
    }
    
    // 触发回合开始时效果
    public void TriggerTurnStartEffects(GameObject source)
    {
        foreach (CardEffect effect in turnStartEffects)
        {
            effect.Trigger(source);
        }
    }
    
    // 触发回合结束时效果
    public void TriggerTurnEndEffects(GameObject source)
    {
        foreach (CardEffect effect in turnEndEffects)
        {
            effect.Trigger(source);
        }
    }
    
    // 触发被动效果
    public void TriggerPassiveEffects(GameObject source, GameObject target = null)
    {
        foreach (CardEffect effect in passiveEffects)
        {
            effect.Trigger(source, target);
        }
    }
    
    // 触发主动效果
    public bool TriggerActiveEffect(GameObject source, GameObject target = null)
    {
        bool success = false;
        
        foreach (CardEffect effect in activeEffects)
        {
            if (effect.Trigger(source, target))
            {
                success = true;
            }
        }
        
        return success;
    }
} 