using System.Collections.Generic;

public class BattleTeam
{
    public int Id { get; set; }
    public List<BattleUnit> BattleUnits { get; set; }

    public BattleTeam(int id, List<BattleUnit> battleUnits)
    {
        Id = id;
        BattleUnits = battleUnits;
    }

    public bool IsAllAlive()
    {
        bool ok = true;
        for(int i = 0; i < BattleUnits.Count; i++)
        {
            ok &= (BattleUnits[i].IsAlive());
        }
        return ok;
    }
}