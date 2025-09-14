using System.Collections.Generic;
using UnityEngine;

public class SummonBuff : BaseBuff
{
    /*
     parameter：id1,id2,id3
     */
    public SummonBuff(BattleLogicUnit owner, List<int> parameter) : base(owner, parameter) { }

    public override void OnTrigger()
    {
        Debug.Log("触发召唤");
        for(int i = 1; i < parameter.Count; i++)
        {
            BattleManager.Instance.AddUnitByIdAndTeam(parameter[i], owner.UnitTeam);
        }
    }
}