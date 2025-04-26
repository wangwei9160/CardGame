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

    [Tooltip("整个游戏的状态")] private GameState gameState;

    public bool IsGameState(GameState state)
    {
        return gameState == state;
    }

    private GameData gameData;
    [Tooltip("数据")] public GameData Data { get { return gameData; } private set { } }

    protected override void Awake()
    {
        base.Awake();
        gameData = new GameData();
        EventCenter.AddListener(EventDefine.OnBattleStart, OnBattleStart);    // 战斗开始
        EventCenter.AddListener(EventDefine.OnMergePanelShow, OnMergePanelShow);
        EventCenter.AddListener<int>(EventDefine.SelectMoneyReward, SelectMoneyReward);    // 添加一个监听
        EventCenter.AddListener<int>(EventDefine.SelectCardReward, SelectCardReward);    // 添加一个监听
        EventCenter.AddListener<EchoEventType>(EventDefine.ON_ENTER_ECHOEVENT, OnEnterEchoEvent);
        EventCenter.AddListener(EventDefine.ON_STAGE_INCREMENT, OnStageIncrement);
        EventCenter.AddListener<int>(EventDefine.ON_GET_TREASURE_BY_ID , GetTreasureByID);  // 获得遗物接口
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnBattleStart, OnBattleStart);    // 移除
        EventCenter.RemoveListener(EventDefine.OnMergePanelShow, OnMergePanelShow);
        EventCenter.RemoveListener<int>(EventDefine.SelectMoneyReward, SelectMoneyReward);    // 上个版本遗忘的更新
        EventCenter.RemoveListener<int>(EventDefine.SelectCardReward, SelectCardReward);    // 添加一个监听
        EventCenter.RemoveListener<EchoEventType>(EventDefine.ON_ENTER_ECHOEVENT, OnEnterEchoEvent);
        EventCenter.RemoveListener(EventDefine.ON_STAGE_INCREMENT, OnStageIncrement);
        EventCenter.AddListener<int>(EventDefine.ON_GET_TREASURE_BY_ID, GetTreasureByID);  // 获得遗物接口
    }

    private void Start()
    {
        EventCenter.Broadcast(EventDefine.ON_ENTER_ECHOEVENT, Data.EchoEventType);// 临时使用用于默认进入战斗
        UIManager.Instance.Show("TopInfo");
    }

    #region 角色属性

    public int GetPlayerHP()
    {
        return gameData.hp;
    }

    public int GetPlayerMaxHP()
    {
        return gameData.maxHp;
    }

    public void OnPlayerGetHp(int val)
    {
        gameData.hp = Mathf.Min(gameData.maxHp , gameData.hp + val);
    }

    public void OnPlayerGetMaxHp(int val)
    {
        gameData.maxHp += val;
        OnPlayerGetHp(val);
    }

    public void OnHpChangeByValue(int val)
    {

    }

    public bool isEnoughMoney(int val)
    {
        return gameData.money >= val;
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

    #endregion 

    public void BettleWin(int id)
    {
        if (!BattleManager.Instance.IsInBattle()) return;
        gameState = GameState.Reward;
        Debug.Log("进入奖励结算");
        System.Random rd = new System.Random();
        gameData.MoneyReward = rd.Next(50, 100);
        gameData.CardReward.Add(1);
        gameData.CardReward.Add(2);
        gameData.CardReward.Add(3);
        UIManager.Instance.Show("RewardPanelUI", ContainerManager.Instance.Enemies.GetChild(id).gameObject);
        UIManager.Instance.Close(GameString.BATTLEUI);   // 隐藏战斗ui
    }

    public void OnStageIncrement()
    {
        gameData.CurrentStage++;    // 当前地图阶段自增 
        EventCenter.Broadcast(EventDefine.ON_LEVEL_INFO_CHANGE);
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

    public List<TreasureBase> GetAllTreasure()
    {
        return gameData.treasureList;
    }

    public void GetTreasureByID(int id)
    {
        TreasureBase treasure = TreasureFactory.GetTreasure(id);
        if(treasure == null) {
            return ;
        }
        Debug.Log($"获得了遗物{treasure.treasureCfg.Name}");
        gameData.treasureList.Add(treasure);
        treasure.OnGet();   // 获得遗物
        EventCenter.Broadcast(EventDefine.ON_TREASURE_UPDATE_SHOW);
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
        if(0 < _select && _select < gameData.CardReward.Count)
        {
            // 获得对应的卡牌
        }else
        {
            OnMoneyChange(_select);
        }
        gameData.CardReward.Clear();    // 清空
    }

    #region 战斗内相关、涉及回合切换

    // 进入启示事件
    public static void OnEnterEchoEvent(EchoEventType type)
    {
        EchoEventClass cls = EchoEventConfig.GetEchoEventClassByKey((int)type);
        if (cls.type == "combat")
        {
            UIManager.Instance.Show("BattleUI", JsonUtility.ToJson(cls));
        }
        else if(cls.type == "event")
        {
            // 去事件池子随机找一个事件 cls.type
            List<EventClass> eventClassList = new List<EventClass>();
            foreach (var kv in EventConfig.m_Dic)
            {
                if (kv.Value.event_type == cls.type)
                {
                    eventClassList.Add(kv.Value);
                }
            }
            EventClass getEventClass = RandomUtil.GetRandomValueInList(eventClassList);
            UIManager.Instance.Show("EventUI", JsonUtility.ToJson(getEventClass));
        }else if(cls.type == "rest")
        {
            UIManager.Instance.Show("StoreUI");
        }
        
    }



    private void OnBattleStart()
    {
        //UIManager.Instance.Show(GameString.BATTLEUI);   //打开战斗UI
        gameData.CurrentTurn = 0;
        gameData.MaxMagicPower = 0; // 先增加最大的
        gameData.MagicPower = 0;
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
        List<TreasureBase> allTreasure = GetAllTreasure();
        foreach (var treasure in allTreasure)
        {
            treasure.OnTurnStart();
        }
        // 提前预留卡牌的动画效果
        for(int i = 0; i < 2; i++)
        {
            Debug.Log(string.Format("Card{0} 回合开始效果触发中..........." , i));
            yield return new WaitForSeconds(0.2f);
        }
        if (IsGameState(GameState.Battle))
        {
            // 如果仍然是战斗回合 , 则进入玩家回合
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
            // 如果仍然是战斗回合 , 则进入新的回合开始效果
            BeforeTurn();
        }
    }

    private void OnMergePanelShow()
    {
        UIManager.Instance.Show(GameString.MERGEUI);    // 合成界面
        OnStageIncrement();
    }

    #endregion

}
