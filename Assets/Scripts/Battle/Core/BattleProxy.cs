using System.Collections.Generic;

public static class BattleProxy
{
    public static bool isRunning = false;
    private static BaseBattlePlayer battlePlayer;

    public static void EntryBattle(List<int> left, List<int> right , BattleType battleType  )
    {
        isRunning = true;
        battlePlayer = BattlePlayerHelper.SetBattlePlayer(battleType    );
        BattleManager.Instance.BaseBattlePlayer = battlePlayer;

        List<BattleLogicUnit> _list = new();
        foreach(var id in left)
        {
            _list.Add(new BattleLogicUnit(id, (int)UnitTeamType.OWNER));
        }
        BattleTeam leftTeam = new(0, _list);

        _list.Clear();
        foreach (var id in right)
        {
            _list.Add(new BattleLogicUnit(id, (int)UnitTeamType.ENEMY));
        }
        BattleTeam rightTeam = new(1, _list);

        battlePlayer.SetBattleTeam(leftTeam, rightTeam);
        battlePlayer.OnEnter(   );
    }
}
