public class BattleUnit
{
    public int UnitId { get; set; }
    public int UnitTeam { get; set; }
    public int[] Attributes = new int[(int)UnitAttribute.COUNT];

    public BattleUnit()
    {
        for (int i = 0; i < (int)UnitAttribute.COUNT; i++)
        {
            Attributes[i] = 0;
        }
    }

    public BattleUnit(int id,int teamId):this() 
    {
        UnitId = id;
        UnitTeam = teamId;
    }

    public bool IsAlive()
    {
        return true;
    }

}