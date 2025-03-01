using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    public override CharacterType Type => CharacterType.Enemy;
    private bool isEnemyTurn;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.OnEnemyTurn, OnEnemyTurn);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnEnemyTurn, OnEnemyTurn);
    }

    protected override void Start()
    {
        UIManager.Instance.Show("CardHpUI", gameObject, ref HpUIIndex);
        EventCenter.Broadcast<int,int>(EventDefine.OnHpChangeByName, hp , HpUIIndex );
    }

    private void OnEnemyTurn()
    {
        var rd = Random.Range(5, 20);
        hp -= rd;
        if(hp < 0)
        {
            hp = 0;
            Destroy(gameObject , 0.5f); // 延迟死亡 可以用于创建协程触发死亡动画
            EventCenter.Broadcast(EventDefine.OnEnemyDeath);
        }
        EventCenter.Broadcast<int, int>(EventDefine.OnHpChangeByName, hp, HpUIIndex);
    }

}
