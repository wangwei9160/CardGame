using System.Collections.Generic;
using UnityEngine;

public class BaseBattlePlayer
{
    public List<BattleTeam> battleTeams;

    public LogicModule logicModule;
    public PerformModule performModule;

    public FiniteStateMachine fsm;
    public BTRoot root;

    public BaseBattlePlayer()
    {
        battleTeams = new List<BattleTeam>(2);
        logicModule = new LogicModule();
        performModule = new PerformModule();
        fsm = new FiniteStateMachine();
        InitFsm();
        InitBTree();
    }

    public virtual void ChangeState(BattleEvent battleEvent) { }

    public void SetBattleTeam(BattleTeam left, BattleTeam right)
    {
        battleTeams.Clear();
        battleTeams.Add(left);
        battleTeams.Add(right);
    }

    public virtual void LoadSceneByName(string path)
    {
        GameObject prefab = Resources.Load<GameObject>($"{path}");
        GameObject obj = UnityEngine.Object.Instantiate(prefab);
    }

    public virtual void OnEnter() { }

    public virtual void InitFsm() { }
    public virtual void InitBTree() { }

    public int enemyActionNum = 0;
    public virtual void OnEnemyTurnStart() { }
}