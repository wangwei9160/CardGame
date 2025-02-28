using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    public override CharacterType Type => CharacterType.Enemy;

    protected override void Start()
    {
        UIManager.Instance.Show("CardHpUI", gameObject, ref HpUIIndex);
        EventCenter.Broadcast(EventDefine.OnHpChangeByName, hp , HpUIIndex );
    }

}
