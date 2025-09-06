using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : BaseBuff
{
    public AttackBuff(List<int> parameters) : base(parameters) { }

    public override void OnTrigger()
    {
        Debug.Log("AttackBuff OnTrigger");
    }
}