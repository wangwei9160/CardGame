/*
 逻辑层：
    1、一次性结算单词行动的所有内容；添加行动动作序列到表现层；
 */
public class LogicModule
{    
    public LogicModule(BaseBattlePlayer parent)
    {
        this.Parent = parent;
    }
    public BaseBattlePlayer Parent { get; set; }

    public BattleLogicUnit CurrentActionUnit { get; set; }

    public BTRoot root;

    // ===================UNIT BT===================

    /* 战斗通用行为树
     1、一阶段重复节点 （所有节点都为false，则）
        a. 战斗状态检测
        b. 技能消耗（花费、一回合内最大次数......）
        c. 技能目标选择（我方单位需要拉起选择器，敌方由配置决定）
        d. 技能释放（拿到技能释放的目标）
    2、二阶段重复节点
        
     */

    public void InitBTree()
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

    /* Enemy Turn */

    public int enemyActionNum = 0;
    public void OnEnemyTurnStart()
    {
        root.ClearParameter();
        OneEnemyAction(enemyActionNum = 0);
    }

    public void OneEnemyAction(int index)
    {
        if (index == Parent.battleTeams[1].BattleUnits.Count)
        {
            SendMessage(BattleEvent.FinishEnemyTurn);
            return;
        }
        BattleLogicUnit curActionEnemyUnit = Parent.battleTeams[1].BattleUnits[index];
        root.SetParameter("Unit", curActionEnemyUnit);
        enemyActionNum++;
        root.OnUpdate();
        OneEnemyAction(index + 1);
    }

    public void GetMessage(BattleEvent _event)
    {
        if(_event == BattleEvent.FinishEnemyTurn)
        {
            Parent.ChangeState(BattleEvent.FinishEnemyTurn);
        }
    }

    public void SendMessage(BattleEvent _event)
    {
        Parent.PerformModule.GetMessage(_event);
    }

}