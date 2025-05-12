using System.Collections.Generic;

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

    public abstract void CreateSelector(bool isForce = false);
    public abstract void CloseSelector();
    public abstract BaseCharacter GetUnit();
    public abstract List<BaseCharacter> GetUnits();
}
