using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSelectorBase
{

    public static bool FilterTroop()
    {
        return true;
    }

    private bool FilterTroopType(BaseCharacter u)
    {
        return u.Type == CharacterType.Player;
    }
    public abstract void UpdateSelector(BaseCharacter u);

    public abstract void CreateSelector();
    public abstract void CloseSelector();
    public abstract BaseCharacter GetUnit();
    public abstract List<BaseCharacter> GetUnits();
}
