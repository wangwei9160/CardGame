using System.Xml.Serialization;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    public int Index { get; set; }

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.OnFinishPlayerTurn, OnEnemyTurn);
        ResetCharacterType(CharacterType.Enemy);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnFinishPlayerTurn, OnEnemyTurn);
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

    private void OnEnemyTurn()
    {
        // 临时用来扣血的
        var rd = Random.Range(10, 30);
        hp -= rd;
        OnDeadCheck();
        EventCenter.Broadcast<int, int>(EventDefine.OnHpChangeByName, hp, HpUIIndex);
    }

    public override void OnHurt(int damage)
    {
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