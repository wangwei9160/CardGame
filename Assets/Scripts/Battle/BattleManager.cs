using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData
{
    public int id;
    public int maxEnemyNum;
    public int maxPlayerTeamNum;
    public List<BaseEnemy> enemies;
    public List<BaseCharacter> playerTeam;

    public BattleData()
    {
        id = 0;
        maxEnemyNum = 3;
        maxPlayerTeamNum = 5;
        enemies = new List<BaseEnemy>();
        playerTeam = new List<BaseCharacter>();
    }

    public void Init()
    {
        id = 0;
        enemies.Clear();
        playerTeam.Clear();
    }

} 

public class BattleManager : ManagerBase<BattleManager>
{
    [Tooltip("角色预制体")] public GameObject player;
    [Tooltip("敌方预制体")] public GameObject enemy;
    public BattleData battleData;
    public bool isBattle = false;

    #region 临时使用
    private readonly int playerNum = 1;
    private readonly int dogNum = 0;
    private int enemyNum = 2;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        EventCenter.AddListener(EventDefine.OnBattleStart, OnBattleStart);    // 战斗开始
        EventCenter.AddListener<int>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 敌人死亡
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnBattleStart, OnBattleStart);    // 移除
        EventCenter.RemoveListener<int>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 移除监听
    }

    public void Start()
    {
        battleData = new BattleData();
    }

    public bool IsInBattle()
    {
        return isBattle;
    }

    public void OnBattleStart()
    {
        isBattle = true;
        enemyNum = 2;
        battleData.Init();
        AddPlayer(playerNum, dogNum);
        AddEnemy(enemyNum);
    }

    // 临时使用，用于初始化时添加我方角色
    private void AddPlayer(int PlayerNum, int CardNum)
    {
        PlayerNum = Math.Min(PlayerNum, 1);
        for (int i = 0; i < PlayerNum; i++)
        {
            if (ContainerManager.Instance.Players[i].childCount > 0) continue;  // 防止重复添加Player
            var go = Instantiate(player, ContainerManager.Instance.Players[i]);
            battleData.playerTeam.Add(go.GetComponent<BaseCharacter>());
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
            var obj = Instantiate(enemy, ContainerManager.Instance.Enemies[i]);
            obj.GetComponent<BaseEnemy>().Index = i;
        }
    }

    public void OnEnemyDeath(int id)
    {
        enemyNum--;
        Debug.Log($"敌人{id} 死亡");
        if (enemyNum == 0)
        {
            GameManager.Instance.BettleWin(id);
            isBattle = false;
        }
    }

    public int getPlayerTeamNum()
    {
        return battleData.playerTeam.Count;
    }

}
