using UnityEngine;

public class TreasureBase
{
    public virtual int ID { get; protected set; } = 1;
    public TreasureClass treasureCfg => TreasureConfig.GetTreasureClassByKey(ID);

    public void print() { Debug.Log(treasureCfg.Description); }

    public virtual string Description(bool isOnlyShow)
    {
        return treasureCfg.Description;
    }

    // 获得遗物
    public virtual void OnGet() { }

    // 丢弃遗物
    public virtual void OnDelete() { }

    // 进入战斗
    public virtual void OnBattleStart() { }

    // 回合开始
    public virtual void OnTurnStart() { }

    // 回合结束
    public virtual void OnTurnEnd() { }

    // 造成伤害前结算
    public virtual void OnBeforeDamage() { }

    // 造成伤害后结算
    public virtual void OnAfterDamage() { }

    // 受伤前
    public virtual void OnBeforeHurt() { }

    // 受伤后
    public virtual void OnAfterHurt() { }

    // 敌方死亡效果
    public virtual void OnEnemyDeath() { }

    // 我方单位死亡
    public virtual void OnPlayerDeath() { }

}
