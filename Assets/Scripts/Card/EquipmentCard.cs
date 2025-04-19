using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 装备卡牌基类
public class EquipmentCard : Card
{
    // 装备特有属性
    public int durability;         // 耐久度
    public bool isEquipped = false;  // 是否已装备
    
    // 装备位置
    protected Vector3 equipmentPosition;  // 装备在屏幕上的位置
    
    // 初始化装备卡牌
    public override void Initialize(string name, string desc, int cost, Sprite image)
    {
        base.Initialize(name, desc, cost, image);
        durability = 3; // 默认耐久度为3
        equipmentPosition = new Vector3(-7f, -3f, 0f);  // 默认位置在屏幕左下角
    }
    
    // 装备
    public virtual void Equip()
    {
        if (!isEquipped)
        {
            isEquipped = true;
            OnEquip();
        }
    }
    
    // 卸下装备
    public virtual void Unequip()
    {
        if (isEquipped)
        {
            isEquipped = false;
            OnUnequip();
        }
    }
    
    // 装备时触发
    protected virtual void OnEquip()
    {
        // 装备时触发被动效果
        OnPassiveEffect();
    }
    
    // 卸下装备时触发
    protected virtual void OnUnequip()
    {
        // 卸下装备时移除被动效果
        foreach (CardEffect effect in passiveEffects)
        {
            Effectmanager.Instance.RemoveEffect(effect);
        }
    }
    
    // 使用装备卡牌
    public override bool OnPlay()
    {
        if (base.OnPlay())
        {
            // 装备
            Equip();
            return true;
        }
        return false;
    }
    
    // 减少耐久度
    public virtual void ReduceDurability(int amount = 1)
    {
        durability -= amount;
        
        // 耐久度为0时卸下装备
        if (durability <= 0)
        {
            Unequip();
            Destroy(gameObject);
        }
    }
    
    // 设置装备位置
    public void SetPosition(Vector3 position)
    {
        equipmentPosition = position;
        transform.position = position;
    }
    
    // 获取装备位置
    public Vector3 GetPosition()
    {
        return equipmentPosition;
    }
    
    // 重置回合状态
    public virtual void ResetTurnState()
    {
        // 重置装备卡牌的回合状态
        OnTurnStart();
    }
}

// 武器卡牌
public class WeaponCard : EquipmentCard
{
    // 武器特有属性
    public int attackValue;        // 攻击力
    
    // 初始化武器卡牌
    public void InitializeWeapon(string name, string desc, int cost, Sprite image, int attack, int durability)
    {
        Initialize(name, desc, cost, image);
        this.attackValue = attack;
        this.durability = durability;
    }
    
    // 装备时触发
    protected override void OnEquip()
    {
        base.OnEquip();
        
        // 增加玩家攻击力

    }
    
    // 卸下装备时触发
    protected override void OnUnequip()
    {
        // 减少玩家攻击力

        
        base.OnUnequip();
    }
    
    // 攻击时减少耐久度
    public void OnAttack()
    {
        ReduceDurability();
    }
}

// 饰品卡牌
public class AccessoryCard : EquipmentCard
{
    // 饰品特有属性
    public int bonusValue;         // 加成值
    public int BonusClass;        // 加成类型
    
    // 初始化饰品卡牌
    public void InitializeAccessory(string name, string desc, int cost, Sprite image, int bonus, int durability)
    {
        Initialize(name, desc, cost, image);
        this.bonusValue = bonus;
        this.durability = durability;
    }
    
    // 装备时触发
    protected override void OnEquip()
    {
        base.OnEquip();
        
        // 增加玩家属性

    }
    
    // 卸下装备时触发
    protected override void OnUnequip()
    {
        // 减少玩家属性

        
        base.OnUnequip();
    }
} 