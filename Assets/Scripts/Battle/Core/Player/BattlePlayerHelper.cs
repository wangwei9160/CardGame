public enum BattleEvent
{
    BattleInitFinish = 0,
    BigRoundCheckFinish = 1,
    FinishPlayerTurn = 2,
    FinishEnemyTurn = 3,
} 

public class BattlePlayerHelper
{
    public static BaseBattlePlayer SetBattlePlayer(BattleType battleType)
    {
        if (battleType == BattleType.Normal)
        {
            return new NormalBattlePlayer();
        }
        return new BaseBattlePlayer();
    }

}