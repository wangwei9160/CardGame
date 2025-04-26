using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneSelector : SkillSelectorBase
{
    public override void CreateSelector(){}

    public override void CloseSelector(){}

    public override void UpdateSelector(BaseCharacter u){}

    public override BaseCharacter GetUnit(){return null;}

    public override List<BaseCharacter> GetUnits() 
    {
        return BattleManager.Instance.getAllEnemy();
    }

}
