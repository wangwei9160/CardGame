using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSelector : SkillSelectorBase
{
    public BattlePerformUnit _unit;

    public CharacterType troop;
    public int condition; 

    public TroopSelector(){}
    public TroopSelector(int _troop , int _condition)
    {
        troop = (CharacterType)_troop;
        condition = _condition;
    }

    public override void CloseSelector(){}

    public override void CreateSelector(bool isForce = false)
    {
        // 巫真
        if(troop == CharacterType.Player && condition == 1)
        {
            _unit = BattleManager.Instance.getPlayer();
            return ;
        }
    }

    public override void UpdateSelector(BattlePerformUnit u)
    {
        // 巫真
        if(troop == CharacterType.Player && condition == 1)
        {
            _unit = BattleManager.Instance.getPlayer();
            return ;
        }
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
