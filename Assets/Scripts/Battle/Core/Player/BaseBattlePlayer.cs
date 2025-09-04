using System.Collections.Generic;
using UnityEngine;

public class BaseBattlePlayer
{
    public List<BattleTeam> battleTeams;

    public LogicModule logicModule;
    public PerformModule performModule;

    public FiniteStateMachine fsm;

    public BaseBattlePlayer()
    {
        battleTeams = new List<BattleTeam>(2);
        logicModule = new LogicModule();
        performModule = new PerformModule();
        fsm = new FiniteStateMachine();
        InitFsm();
    }

    public virtual void ChangeState(BattleEvent battleEvent) { }

    public void SetBattleTeam(BattleTeam left, BattleTeam right)
    {
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

}