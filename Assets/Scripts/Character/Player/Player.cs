using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public override CharacterType Type => CharacterType.Player;
    public float f = 0f;

    protected override void Start()
    {
        UIManager.Instance.Show("HpUI", gameObject, ref HpUIIndex);
        hp = GameManager.Instance.Data.hp;
        maxHp = GameManager.Instance.Data.maxHp;
        EventCenter.Broadcast(EventDefine.OnPlayerAttributeChange, hp , maxHp , HpUIIndex);
    }

    private void Update()
    {
        f += Time.deltaTime;
        if(f > 1f)
        {
            EventCenter.Broadcast(EventDefine.OnPlayerAttributeChange, hp , maxHp, HpUIIndex);
            f = 0f;
            hp = (hp + 10) % maxHp; 
        }
    }

}
