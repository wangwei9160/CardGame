using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    public override CharacterType Type => CharacterType.Card;
    
    public int Index { get; set; }

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.OnFinishPlayerTurn, OnEnemyTurn);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnFinishPlayerTurn, OnEnemyTurn);
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
        if(hp <= 0)
        {
            hp = 0;
            Destroy(gameObject , 0.3f); // 延迟死亡 可以用于创建协程触发死亡动画
            EventCenter.Broadcast(EventDefine.OnEnemyDeath , Index);
        }
        EventCenter.Broadcast<int, int>(EventDefine.OnHpChangeByName, hp, HpUIIndex);
    }

}
