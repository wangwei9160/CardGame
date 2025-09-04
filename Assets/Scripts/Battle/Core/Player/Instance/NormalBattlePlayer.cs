using UnityEngine;
public class NormalBattlePlayer : BaseBattlePlayer
{
    public NormalBattlePlayer()
    {
        Debug.Log("NormalBattlePlayer");
    }

    public WaitingForStartState waitingForStartState;
    public BigRoundChangeState bigRoundChangeState;
    public PlayerTurnState playerTurnState;
    public EnemyTurnState enemyTurnState;

    public override void InitFsm()
    {
        waitingForStartState = new WaitingForStartState(this);
        bigRoundChangeState = new BigRoundChangeState(this);
        playerTurnState = new PlayerTurnState(this);
        enemyTurnState = new EnemyTurnState(this);
    }

    public override void ChangeState(BattleEvent battleEvent)
    {
        if(battleEvent == BattleEvent.BattleInitFinish)
        {
            fsm.ChangeState(bigRoundChangeState);
        }else if(battleEvent == BattleEvent.BigRoundCheckFinish)
        {
            fsm.ChangeState(playerTurnState);
        }else if(battleEvent == BattleEvent.FinishPlayerTurn)
        {
            fsm.ChangeState(enemyTurnState);
        }else if(battleEvent == BattleEvent.FinishEnemyTurn)
        {
            fsm.ChangeState(bigRoundChangeState);
        }
    }

    public override void OnEnter()
    {
        base.LoadSceneByName("Prefabs/EchoEvent/FightEvent");
        UIManager.Instance.Show("BattleUI");
        fsm.ChangeState(waitingForStartState);
    }

    /* Enemy Turn */
    public void OnEnemyTurnStart()
    {

    }

}