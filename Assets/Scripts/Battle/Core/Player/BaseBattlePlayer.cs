using System.Collections.Generic;
using UnityEngine;

public class BaseBattlePlayer
{
    public BattleEventCenter EventCenter { get; private set; }

    public List<BattleTeam> battleTeams;

    public LogicModule LogicModule { get; private set; }
    public PerformModule PerformModule { get; private set; }

    public FiniteStateMachine fsm;
    

    public BaseBattlePlayer()
    {
        EventCenter = new();
        battleTeams = new List<BattleTeam>(2);
        LogicModule = new LogicModule(this);
        PerformModule = new PerformModule(this);
        fsm = new FiniteStateMachine();
        InitFsm();
        LogicModule.InitBTree();
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
    public virtual void OnEnemyTurnStart() { }
    public virtual void OnEnemyTurnEndCheck() { }
}