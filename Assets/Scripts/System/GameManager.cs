using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    public GameObject player;
    public GameObject enemy;
    public PlayerTurn playerTurn;
    public EnemyTurn enemyTurn;
    public FightStateMachine stateMachine;

    void Start()
    {
        playerTurn = new PlayerTurn();
        enemyTurn = new EnemyTurn();
        UIManager.Instance.Show("BattleUI");
        AddPlayer(2);
        AddEnemy(2);
        stateMachine = new FightStateMachine(playerTurn);
    }

    private void Update()
    {
        stateMachine.OnUpdate();    
    }

    private void AddPlayer(int num)
    {
        num = Math.Min(num, 5);
        for (int i = 0; i < num; i++)
        {
            Instantiate(player, ContainerManager.Instance.Players[i]);
        }
    }
    private void AddEnemy(int num)
    {
        num = Math.Min(num, 3);
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy, ContainerManager.Instance.Enemies[i]);
        }
    }

}
