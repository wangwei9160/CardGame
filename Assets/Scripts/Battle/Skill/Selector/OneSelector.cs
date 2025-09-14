using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSelector : SkillSelectorBase
{
    public BattlePerformUnit _unit;

    public bool isUse;

    public override void CreateSelector(bool isForce = false)
    {
        isUse = true;
        _unit = null;
        object[] objs = {this,isForce};
        UIManager.Instance.Show("OneSelecteUI", objs);
    }

    public override void CloseSelector()
    {
        if (!isUse) return;
        UIManager.Instance.Close("OneSelecteUI");
        _unit = null;
        isUse = false;
    }

    public override void UpdateSelector(BattlePerformUnit u)
    {
        if (_unit == u) return;
        _unit = u;
    }

    public override BattlePerformUnit GetUnit()
    {
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
