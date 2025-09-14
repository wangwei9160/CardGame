using System.Collections.Generic;

public abstract class SkillSelectorBase
{

    public static bool FilterTroop()
    {
        return true;
    }

    private bool FilterTroopType(BattlePerformUnit u)
    {
        return u.Type == CharacterType.Player;
    }
    public abstract void UpdateSelector(BattlePerformUnit u);

    public abstract void CreateSelector(bool isForce = false);
    public abstract void CloseSelector();
    public abstract BattlePerformUnit GetUnit();
    public abstract List<BattlePerformUnit> GetUnits();
}
