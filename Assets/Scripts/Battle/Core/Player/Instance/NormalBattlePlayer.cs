using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
public class NormalBattlePlayer : BaseBattlePlayer
{
    public NormalBattlePlayer()
    {
        Debug.Log("NormalBattlePlayer");
    }

    // ===================FSM===================

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

    // ===================END FSM===================

    // ===================UNIT BT===================

    /* 战斗通用行为树
     1、一阶段重复节点 （所有节点都为false，则）
        a. 战斗状态检测
        b. 技能消耗（花费、一回合内最大次数......）
        c. 技能目标选择（我方单位需要拉起选择器，敌方由配置决定）
        d. 技能释放（拿到技能释放的目标）
    2、二阶段重复节点
        
     */

    public override void InitBTree()
    {
        BTBlackboard bTBlackboard = new();
        BTSequence sequence1 = new();
        root = new BTRoot(sequence1, bTBlackboard);
        // sequence1
        BattleStateCheck condition1 = new();
        sequence1.AddChildrens(condition1);
        UnitSkillCostCheck condition2 = new();
        sequence1.AddChildrens(condition2);
        UnitSkillChooseTarget unitSkillChooseTarget = new();
        sequence1.AddChildrens(unitSkillChooseTarget);
        UnitSkillFireAction unitSkillFireAction = new();
        sequence1.AddChildrens(unitSkillFireAction);

        // End sequence1
    }

    // ===================END UNIT BT===============

    public override void OnEnter()
    {
        base.LoadSceneByName("Prefabs/EchoEvent/FightEvent");
        UIManager.Instance.Show("BattleUI");
        fsm.ChangeState(waitingForStartState);
    }

    /* Enemy Turn */
    public override void OnEnemyTurnStart()
    {
        root.ClearParameter();
        OneEnemyAction(enemyActionNum = 0);
    }

    public void OneEnemyAction(int index)
    {
        if (index == battleTeams[1].BattleUnits.Count)
        {
            ChangeState(BattleEvent.FinishEnemyTurn);
            return;
        }
        BattleLogicUnit curActionEnemyUnit = battleTeams[1].BattleUnits[index];        
        root.SetParameter("Unit", curActionEnemyUnit);
        enemyActionNum++;
        root.OnUpdate();
        OneEnemyAction(index + 1);    
    }
}