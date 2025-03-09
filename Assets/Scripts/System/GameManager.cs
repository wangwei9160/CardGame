using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Unknown,
    Battle,
    Reward
}

public class GameManager : ManagerBase<GameManager>
{
    [Tooltip("角色预制体")]public GameObject player;   
    [Tooltip("敌方预制体")]public GameObject enemy;

    [Tooltip("整个游戏的状态")] private GameState gameState;

    public bool IsGameState(GameState state)
    {
        return gameState == state;
    }

    private GameData gameData;
    [Tooltip("数据")] public GameData Data { get { return gameData; } private set { } }

    #region 临时使用
    private readonly int playerNum = 1;
    private readonly int dogNum = 0;
    private int enemyNum = 2;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        gameData = new GameData();
        EventCenter.AddListener(EventDefine.OnBattleStart, OnBattleStart);    // 战斗开始
        EventCenter.AddListener<int>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 添加一个监听
        EventCenter.AddListener(EventDefine.OnMergePanelShow, OnMergePanelShow);
        EventCenter.AddListener<int>(EventDefine.SelectMoneyReward, SelectMoneyReward);    // 添加一个监听
        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnBattleStart, OnBattleStart);    // 移除
        EventCenter.RemoveListener<int>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 移除监听
        EventCenter.RemoveListener(EventDefine.OnMergePanelShow, OnMergePanelShow);
        EventCenter.RemoveListener<int>(EventDefine.SelectMoneyReward, SelectMoneyReward);    // 上个版本遗忘的更新
    }

    private void Start()
    {
        EventCenter.Broadcast(EventDefine.OnBattleStart);   // 临时使用用于默认进入战斗
    }

    // 临时使用，用于初始化时添加我方角色
    private void AddPlayer(int PlayerNum , int CardNum)
    {
        PlayerNum = Math.Min(PlayerNum, 1);
        for (int i = 0; i < PlayerNum; i++)
        {
            if (ContainerManager.Instance.Players[i].childCount > 0) continue;  // 防止重复添加Player
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
            var obj = Instantiate(enemy, ContainerManager.Instance.Enemies[i]);
            obj.GetComponent<BaseEnemy>().Index = i;
        }
    }

    // 增加钱的数量的方法
    public void OnMoneyChange(int val)
    {
        gameData.money += val;
    }

   
    // 法力值更新方法
    public void OnMagicPowerChange(int val , int val2)
    {
        gameData.MaxMagicPower += val2; // 先增加最大的
        gameData.MagicPower += val;
        
        EventCenter.Broadcast(EventDefine.OnMagicPowerChange , gameData.MagicPower , gameData.MaxMagicPower);
    }

    public void OnEnemyDeath(int id)
    {
        enemyNum--;
        if(enemyNum == 0)
        {
            BettleWin(id);
        }
    }

    private void BettleWin(int id)
    {
        gameState = GameState.Reward;
        Debug.Log("进入奖励结算");
        System.Random rd = new System.Random();
        gameData.MoneyReward = rd.Next(50, 100);
        gameData.CardReward.Add(1);
        gameData.CardReward.Add(2);
        gameData.CardReward.Add(3);
        gameData.CurrentStage++;    // 当前地图阶段自增 
        UIManager.Instance.Close(GameString.BATTLEUI);
        UIManager.Instance.Show("RewardPanelUI", ContainerManager.Instance.Enemies[id].gameObject);
    }

    // 临时暴露奖励接口
    public int MoneyReward()
    {
        return gameData.MoneyReward;
    }
    // 临时暴露奖励接口
    public List<int> CardReward()
    {
        return gameData.CardReward;
    }

    // 是否拿取金币奖励
    private void SelectMoneyReward(int isGet)
    {
        bool isG = isGet == 1;
        if (isG) OnMoneyChange(gameData.MoneyReward);
        gameData.MoneyReward = 0;
    }

    // 是否拿取卡牌奖励
    private void SelectCardReward(int _select)
    {
        if(0 <= _select && _select < gameData.CardReward.Count)
        {
            // 获得对应的卡牌
        }
        gameData.CardReward.Clear();    // 清空
    }

    #region 战斗内相关、涉及回合切换

    private void OnBattleStart()
    {
        UIManager.Instance.Show(GameString.BATTLEUI);   //打开战斗UI
        AddPlayer(playerNum, dogNum);
        enemyNum = 2;
        AddEnemy(enemyNum);
        gameState = GameState.Battle;   // 暂时初始化为战斗开始
        BeforeTurn(); // 代替状态机的切换
    }

    // 回合开始
    public void BeforeTurn()
    {
        UIManager.Instance.Show("PlayerTurnTip");   // 新的回合
        EventCenter.Broadcast(EventDefine.OnBeforePlayerTurn); // 进入一个新的回合
        StartCoroutine(BeforePlayerTurn());
    }

    private IEnumerator BeforePlayerTurn()
    {
        // 提前预留卡牌的动画效果
        for(int i = 0; i < 2; i++)
        {
            Debug.Log(string.Format("Card{0} 回合开始效果触发中..........." , i));
            yield return new WaitForSeconds(0.2f);
        }
        if (IsGameState(GameState.Battle))
        {
            // 如果仍然是战斗回合 ， 则进入玩家回合
            EventCenter.Broadcast(EventDefine.OnPlayerTurnStart); // 进入玩家可操作回合事件广播
        }
    }

    // 回合结束
    public void FinishTurn()
    {
        EventCenter.Broadcast(EventDefine.OnFinishPlayerTurn); // 进入一个新的回合
        StartCoroutine(AfterPlayerTurn());
    }

    private IEnumerator AfterPlayerTurn()
    {
        // 提前预留卡牌的动画效果
        for (int i = 0; i < 2; i++)
        {
            Debug.Log(string.Format("Card{0} 回合结束效果触发中...........", i));
            yield return new WaitForSeconds(0.2f);
        }
        if (IsGameState(GameState.Battle))
        {
            // 如果仍然是战斗回合 ， 则进入新的回合开始效果
            BeforeTurn();
        }

    }

    private void OnMergePanelShow()
    {
        UIManager.Instance.Show(GameString.MERGEUI);    // 合成界面
        gameData.CurrentTurn = 0;
        gameData.MaxMagicPower = 0; // 先增加最大的
        gameData.MagicPower = 0;
    }

    #endregion

}
