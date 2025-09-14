using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneSelector : SkillSelectorBase
{
    public override void CreateSelector(bool isForce = false){}

    public override void CloseSelector(){}

    public override void UpdateSelector(BattlePerformUnit u){}

    public override BattlePerformUnit GetUnit(){return null;}

    public override List<BattlePerformUnit> GetUnits() 
    {
        return BattleManager.Instance.getAllEnemy();
    }

}
