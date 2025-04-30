using System;
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

    private void OnDestroy()
    {
        if(UIManager.Instance != null)
        {
            UIManager.Instance.Close("HpUI" , HpUIIndex);
        }
    }

    public override void OnHeal(int val)
    {
        hp = Math.Min(hp + val , maxHp);
        EventCenter.Broadcast(EventDefine.OnPlayerAttributeChange, hp , maxHp, HpUIIndex);
    }

    public override void OnHurt(int val)
    {
        hp = Math.Max(hp - val , 0);
        EventCenter.Broadcast(EventDefine.OnPlayerAttributeChange, hp , maxHp, HpUIIndex);
    }

}
