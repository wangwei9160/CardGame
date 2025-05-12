using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelector : SkillSelectorBase
{
    public BaseCharacter _unit;

    public override void CloseSelector(){}

    public override void CreateSelector(bool isForce = false){}

    public override void UpdateSelector(BaseCharacter u)
    {
        List<BaseCharacter> _list = BattleManager.Instance.getAllEnemy();
        _unit = RandomUtil.GetRandomValueInList(_list);
    }

    public override BaseCharacter GetUnit()
    {
        UpdateSelector(null);
        return _unit;
    }

    public override List<BaseCharacter> GetUnits()
    {
        BaseCharacter unit = GetUnit();
        if(unit == null) return new List<BaseCharacter>();
        List<BaseCharacter> _list = new List<BaseCharacter>{_unit};
        return _list;
    }

    
}
