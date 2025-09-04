public class BattleProxy
{
    public static bool isRunning = false;
    private static BaseBattlePlayer battlePlayer;

    public static void EntryBattle(BattleTeam left, BattleTeam right , BattleType battleType)
    {
        isRunning = true;
        battlePlayer = BattlePlayerHelper.SetBattlePlayer(battleType);
        BattleManager.Instance.BaseBattlePlayer = battlePlayer;
        battlePlayer.SetBattleTeam(left,right);
        battlePlayer.OnEnter();
    }
}
