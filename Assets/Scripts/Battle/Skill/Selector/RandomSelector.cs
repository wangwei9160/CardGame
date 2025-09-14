using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelector : SkillSelectorBase
{
    public BattlePerformUnit _unit;

    public override void CloseSelector(){}

    public override void CreateSelector(bool isForce = false){}

    public override void UpdateSelector(BattlePerformUnit u)
    {
        List<BattlePerformUnit> _list = BattleManager.Instance.getAllEnemy();
        _unit = RandomUtil.GetRandomValueInList(_list);
    }

    public override BattlePerformUnit GetUnit()
    {
        UpdateSelector(null);
        return _unit;
    }

    public override List<BattlePerformUnit> GetUnits()
    {
        BattlePerformUnit unit = GetUnit();
        if(unit == null) return new List<BattlePerformUnit>();
        List<BattlePerformUnit> _list = new List<BattlePerformUnit>{_unit};
        return _list;
    }

    
}
