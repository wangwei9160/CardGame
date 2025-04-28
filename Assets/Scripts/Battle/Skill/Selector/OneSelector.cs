using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSelector : SkillSelectorBase
{
    public BaseCharacter _unit;

    public bool isUse;

    public override void CreateSelector()
    {
        isUse = true;
        _unit = null;
        UIManager.Instance.Show("OneSelecteUI", this);
    }

    public override void CloseSelector()
    {
        if (!isUse) return;
        UIManager.Instance.Close("OneSelecteUI");
        _unit = null;
        isUse = false;
    }

    public override void UpdateSelector(BaseCharacter u)
    {
        if (_unit == u) return;
        _unit = u;
    }

    public override BaseCharacter GetUnit()
    {
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
