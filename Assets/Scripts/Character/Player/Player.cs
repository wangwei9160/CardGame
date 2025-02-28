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
        EventCenter.Broadcast(EventDefine.OnPlayerAttributeChange, 1.0f * hp / maxHp , HpUIIndex);
    }

    private void Update()
    {
        f += Time.deltaTime;
        if(f > 1f)
        {
            EventCenter.Broadcast<float , int>(EventDefine.OnPlayerAttributeChange, 1.0f * hp / maxHp, HpUIIndex);
            f = 0f;
            hp = (hp + 10) % maxHp; 
        }
    }

}
