using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : BaseBuff
{
    public AttackBuff(BattleLogicUnit owner , List<int> parameter) : base(owner, parameter) { }

    public override void OnTrigger()
    {
        owner.Attack();
        Debug.Log("AttackBuff OnTrigger");
    }
}