using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : BaseBuff
{
    public AttackBuff(BattleLogicUnit owner , List<int> parameter) : base(owner, parameter) { }

    public override void OnTrigger()
    {
        Debug.Log("AttackBuff OnTrigger");
    }
}