using System.Collections.Generic;

public class BattleTeam
{
    public int Id { get; set; }
    public List<BattleLogicUnit> BattleUnits { get; set; }

    public List<BaseBuff> buffs = new List<BaseBuff>(); // 部分buff挂在 Team上，如特殊战斗Buff等

    public int TeamUnitMaxNum { get; set; } 

    public BattleTeam(int id, List<BattleLogicUnit> battleUnits)
    {
        Id = id;
        BattleUnits = battleUnits;
        TeamUnitMaxNum = 4;
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

    public bool CheckCanSummon()
    {
        return TeamUnitMaxNum > BattleUnits.Count;
    }

    public void AddUnitToTeam(BattleLogicUnit _unit)
    {
        BattleUnits.Add(_unit); 
    }
}