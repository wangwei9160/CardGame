using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    [Tooltip("角色预制体")]public GameObject player;   
    [Tooltip("敌方预制体")]public GameObject enemy;
    [Tooltip("我方回合")] public PlayerTurn playerTurn;
    [Tooltip("敌方回合")] public EnemyTurn enemyTurn;
    [Tooltip("状态机")] public FightStateMachine stateMachine;


    private GameData gameData;
    [Tooltip("数据")] public GameData Data { get { return gameData; } private set { } }

    #region 临时使用
    private readonly int playerNum = 1;
    private readonly int dogNum = 0;
    private int enemyNum = 2;
    #endregion

    void Start()
    {
        gameData = new GameData();
        playerTurn = new PlayerTurn();
        enemyTurn = new EnemyTurn();
        UIManager.Instance.Show("BattleUI");
        AddPlayer(playerNum, dogNum);
        AddEnemy(enemyNum);
        stateMachine = new FightStateMachine(playerTurn); // 临时默认开始为我方回合
        EventCenter.AddListener(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 添加一个监听
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 添加一个监听
    }

    private void Update()
    {
        stateMachine.OnUpdate();    
    }

    // 临时使用，用于初始化时添加我方角色
    private void AddPlayer(int PlayerNum , int CardNum)
    {
        PlayerNum = Math.Min(PlayerNum, 1);
        for (int i = 0; i < PlayerNum; i++)
        {
            Instantiate(player, ContainerManager.Instance.Players[i]);
        }
        CardNum = Math.Min(CardNum, 5 - PlayerNum);
        for (int i = 0; i < CardNum; i++)
        {
            Instantiate(enemy, ContainerManager.Instance.Players[i + PlayerNum]);
        }
    }
    // 临时使用，用于初始化时添加敌方角色
    private void AddEnemy(int num)
    {
        num = Math.Min(num, 3);
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy, ContainerManager.Instance.Enemies[i]);
        }
    }

    public void OnMagicPowerChange(int val)
    {
        gameData.MagicPower += val;
        EventCenter.Broadcast(EventDefine.OnMagicPowerChange , gameData.magicPower);
    }

    public void OnEnemyDeath()
    {
        enemyNum--;
        if(enemyNum == 0)
        {
            Debug.Log("通关!!!!!!");
        }
    }

}
