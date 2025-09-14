using UnityEngine;

public class BaseEnemy : BattlePerformUnit
{
    public int Index { get; set; }

    private void Awake()
    {
        ResetCharacterType(CharacterType.Enemy);
    }

    private void OnDestroy()
    {
        if(HpUIIndex != -1)
        {
            EventCenter.Broadcast(EventDefine.OnEnemyDeath , Index , Type);
        }
    }

    protected override void Start()
    {
        UIManager.Instance.Show("CardHpUI", gameObject, ref HpUIIndex);
        EventCenter.Broadcast<int,int>(EventDefine.OnHpChangeByName, hp , HpUIIndex );
    }

    public override void OnHurt(int damage)
    {
        if(hp <= 0) return ;
        hp -= damage;
        OnDeadCheck();
        EventCenter.Broadcast<int, int>(EventDefine.OnHpChangeByName, hp, HpUIIndex);
    }

    public override void OnHeal(int val)
    {
        base.OnHeal(val);
        hp += val;
        EventCenter.Broadcast<int, int>(EventDefine.OnHpChangeByName, hp, HpUIIndex);
    }

    public override void OnDeadCheck()
    {
        if(hp <= 0)
        {
            hp = 0;
            EventCenter.Broadcast(EventDefine.OnHpChangeByName, hp, HpUIIndex);
            EventCenter.Broadcast(EventDefine.OnEnemyDeath , Index , Type);
            HpUIIndex = -1;
            Destroy(gameObject , 0.3f); // 延迟死亡 可以用于创建协程触发死亡动画
        }
    }

    public override void ReSetPosition()
    {
        EventCenter.Broadcast(EventDefine.OnFollowerHpReSetPostion , HpUIIndex);
    }

}