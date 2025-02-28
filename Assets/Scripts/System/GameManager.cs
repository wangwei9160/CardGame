using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    [Tooltip("��ɫԤ����")]public GameObject player;   
    [Tooltip("�з�Ԥ����")]public GameObject enemy;
    [Tooltip("�ҷ��غ�")] public PlayerTurn playerTurn;
    [Tooltip("�з��غ�")] public EnemyTurn enemyTurn;
    [Tooltip("״̬��")] public FightStateMachine stateMachine;

    void Start()
    {
        playerTurn = new PlayerTurn();
        enemyTurn = new EnemyTurn();
        UIManager.Instance.Show("BattleUI");
        AddPlayer(1,1);
        AddEnemy(2);
        stateMachine = new FightStateMachine(playerTurn); // ��ʱĬ�Ͽ�ʼΪ�ҷ��غ�
    }

    private void Update()
    {
        stateMachine.OnUpdate();    
    }

    // ��ʱʹ�ã����ڳ�ʼ��ʱ����ҷ���ɫ
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
    // ��ʱʹ�ã����ڳ�ʼ��ʱ��ӵз���ɫ
    private void AddEnemy(int num)
    {
        num = Math.Min(num, 3);
        for (int i = 0; i < num; i++)
        {
            Instantiate(enemy, ContainerManager.Instance.Enemies[i]);
        }
    }

}
