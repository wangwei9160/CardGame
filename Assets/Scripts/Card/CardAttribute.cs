using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 属性类型枚举
public enum AttributeType
{
    None,       // 无属性
    Vitality,   // 活力♥
    Glass,      // 玻璃
    Defense,    // 防御
    Shield      // 护盾
}

// 属性值类
[System.Serializable]
public class AttributeValue
{
    public AttributeType type;     // 属性类型
    public int value;              // 属性值
    public bool isInherent;        // 是否为固有属性（基底赋予）
    public bool isTemporary;       // 是否为临时属性（当前战斗有效）

    public AttributeValue(AttributeType type, int value, bool isInherent = true, bool isTemporary = false)
    {
        this.type = type;
        this.value = value;
        this.isInherent = isInherent;
        this.isTemporary = isTemporary;
    }
}

// 卡牌属性管理器
public class CardAttribute : MonoBehaviour
{
    [SerializeField]
    private AttributeType leftAttribute = AttributeType.None;   // 左属性
    [SerializeField]
    private AttributeType rightAttribute = AttributeType.None;  // 右属性
    
    // 属性值字典
    private Dictionary<AttributeType, List<AttributeValue>> attributes = new Dictionary<AttributeType, List<AttributeValue>>();
    
    void Awake()
    {
        InitializeAttributesDictionary();
    }
    
    // 初始化属性字典
    private void InitializeAttributesDictionary()
    {
        attributes.Clear();
        foreach (AttributeType type in System.Enum.GetValues(typeof(AttributeType)))
        {
            attributes[type] = new List<AttributeValue>();
        }
    }
    
    // 设置基底属性
    public void SetBaseAttributes(AttributeType left, AttributeType right)
    {
        leftAttribute = left;
        rightAttribute = right;
    }
    
    // 获取基底属性
    public (AttributeType left, AttributeType right) GetBaseAttributes()
    {
        return (leftAttribute, rightAttribute);
    }
    
    // 添加属性值
    public void AddAttributeValue(AttributeType type, int value, bool isInherent = false, bool isTemporary = true)
    {
        if (!attributes.ContainsKey(type))
        {
            attributes[type] = new List<AttributeValue>();
        }
        attributes[type].Add(new AttributeValue(type, value, isInherent, isTemporary));
    }
    
    // 获取属性总值
    public int GetAttributeTotal(AttributeType type)
    {
        if (!attributes.ContainsKey(type)) return 0;
        
        int total = 0;
        foreach (var attr in attributes[type])
        {
            total += attr.value;
        }
        return total;
    }
    
    // 处理伤害
    public void TakeDamage(int damage, bool isSwordDamage, bool isMagicDamage)
    {
        // 如果有防御属性,先计算防御减伤
        if (isSwordDamage)
        {
            int defense = GetAttributeTotal(AttributeType.Defense);
            if (defense >= damage)
            {
                return; // 伤害完全被防御抵消
            }
            damage -= defense;
        }
        
        // 优先扣除护盾值
        int remainingDamage = ProcessShieldDamage(damage);
        if (remainingDamage <= 0) return;
        
        // 处理玻璃和活力
        if (isSwordDamage)
        {
            // 玻璃只受到1点伤害
            ProcessGlassDamage();
        }
        
        if (remainingDamage > 0 && (isSwordDamage || isMagicDamage))
        {
            // 处理活力伤害
            ProcessVitalityDamage(remainingDamage);
        }
        
        // 检查是否死亡
        CheckDeath();
    }
    
    // 处理护盾伤害
    private int ProcessShieldDamage(int damage)
    {
        int shield = GetAttributeTotal(AttributeType.Shield);
        if (shield <= 0) return damage;
        
        if (shield >= damage)
        {
            RemoveAttributeValue(AttributeType.Shield, damage, false);  // 优先移除临时护盾
            return 0;
        }
        
        RemoveAttributeValue(AttributeType.Shield, shield, false);  // 优先移除临时护盾
        return damage - shield;
    }
    
    // 处理玻璃伤害（剑属性攻击时固定减少1）
    private void ProcessGlassDamage()
    {
        // 优先移除临时玻璃属性
        RemoveAttributeValue(AttributeType.Glass, 1, false);
        
        // 如果没有临时属性,则移除固有属性
        if (GetAttributeTotal(AttributeType.Glass) > 0)
        {
            RemoveAttributeValue(AttributeType.Glass, 1, true);
        }
    }
    
    // 处理活力伤害
    private void ProcessVitalityDamage(int damage)
    {
        // 优先移除临时活力属性
        int remainingDamage = RemoveAttributeValue(AttributeType.Vitality, damage, false);
        
        // 如果还有剩余伤害,则移除固有属性
        if (remainingDamage > 0)
        {
            RemoveAttributeValue(AttributeType.Vitality, remainingDamage, true);
        }
    }
    
    // 移除指定数量的属性值,返回未能移除的数量
    private int RemoveAttributeValue(AttributeType type, int amount, bool removeInherent)
    {
        if (!attributes.ContainsKey(type)) return amount;
        
        int remaining = amount;
        var attributesToRemove = new List<AttributeValue>();
        
        foreach (var attr in attributes[type])
        {
            if (attr.isInherent == removeInherent)
            {
                if (attr.value >= remaining)
                {
                    attr.value -= remaining;
                    if (attr.value <= 0)
                    {
                        attributesToRemove.Add(attr);
                    }
                    remaining = 0;
                    break;
                }
                else
                {
                    remaining -= attr.value;
                    attributesToRemove.Add(attr);
                }
            }
        }
        
        foreach (var attr in attributesToRemove)
        {
            attributes[type].Remove(attr);
        }
        
        return remaining;
    }
    
    // 检查是否死亡
    private void CheckDeath()
    {
        bool hasVitality = GetAttributeTotal(AttributeType.Vitality) > 0;
        bool hasGlass = GetAttributeTotal(AttributeType.Glass) > 0;
        
        if (!hasVitality && !hasGlass)
        {
            // 通知卡牌死亡
            SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
        }
    }
    
    // 清理临时属性（战斗结束时调用）
    public void ClearTemporaryAttributes()
    {
        foreach (var type in attributes.Keys)
        {
            attributes[type].RemoveAll(attr => attr.isTemporary);
        }
    }
} 