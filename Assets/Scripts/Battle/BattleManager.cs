using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData
{
    public int combat_id;
    public CombatClass Cfg => CombatConfig.GetCombatClassByKey(combat_id);

    public int current_turn;

    public int maxEnemyNum;
    public int maxPlayerTeamNum;
    public List<BaseCharacter> enemies;     // 敌方
    public List<BaseCharacter> playerTeam;  // 我方
    public BaseCharacter player;            // 巫真
    public int maxCardCount;                // 手牌上限
    public List<int> handCards;             // 手牌
    public List<int> deckCards;             // 牌堆
    public List<int> discardPile;           // 弃牌堆
    public List<int> grave;                 // 墓地
    public BattleData()
    {
        combat_id = 60001;
        current_turn = 0;
        maxEnemyNum = 3;
        maxPlayerTeamNum = 5;
        maxCardCount = 8;
        enemies = new List<BaseCharacter>();
        playerTeam = new List<BaseCharacter>();
        handCards = new List<int>();
        deckCards = new List<int>();
        discardPile = new List<int>();
        grave = new List<int>();
    }

    public void Init()
    {
        current_turn = 0;
        combat_id = RandomUtil.GetRandomValueInList(CombatConfig.GetAll()).combat_ids;
        maxEnemyNum = 3;
        maxPlayerTeamNum = 5;
        maxCardCount = 8;
        enemies.Clear();
        playerTeam.Clear();
        handCards.Clear();
        deckCards.Clear();
        discardPile.Clear();
        grave.Clear();
    }

    public void ResetCombatId(int _id) 
    {
        combat_id = _id;
    }
} 

public class BattleManager : ManagerBase<BattleManager>
{
    [Tooltip("角色预制体")] public GameObject player;
    
    // [Tooltip("卡牌预制体")] public GameObject handcard;
    public BattleData battleData;
    public bool isBattle = false;

    #region 临时使用
    private readonly int playerNum = 1;
    private readonly int dogNum = 0;
    private int enemyNum = 0;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        battleData = new BattleData();
        EventCenter.AddListener(EventDefine.OnBattleStart, OnBattleStart);    // 战斗开始
        EventCenter.AddListener(EventDefine.OnFinishPlayerTurn, OnFinishPlayerTurn);    // 战斗开始
        EventCenter.AddListener<int,CharacterType>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 敌人死亡
        EventCenter.AddListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.AddListener<int>(EventDefine.OnGetCardByID, OnGetCardByID);
        EventCenter.AddListener<int>(EventDefine.OnDeleteCardByIndex, OnDeleteCardByIndex);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnBattleStart, OnBattleStart);    // 移除
        EventCenter.RemoveListener(EventDefine.OnFinishPlayerTurn, OnFinishPlayerTurn);    // 移除
        EventCenter.RemoveListener<int,CharacterType>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 移除监听
        EventCenter.RemoveListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.RemoveListener<int>(EventDefine.OnGetCardByID, OnGetCardByID);
        EventCenter.RemoveListener<int>(EventDefine.OnDeleteCardByIndex, OnDeleteCardByIndex);
    }

    public bool IsInBattle()
    {
        return isBattle;
    }

    public void OnBattleStart()
    {
        // 由于GameManager内OnBattleStart 调用OnBeforePlayerTurn 会导致此处初始化延迟（待优化
        isBattle = true;
        AddPlayer(playerNum, dogNum);
        AddEnemy();
    }

    // 临时使用,用于初始化时添加我方角色
    private void AddPlayer(int PlayerNum, int CardNum)
    {
        PlayerNum = Math.Min(PlayerNum, 1);
        for (int i = 0; i < PlayerNum; i++)
        {
            var go = ContainerManager.Instance.AddPlayer();
            battleData.playerTeam.Add(go);
            battleData.player = go;
        }
    }
    // 临时使用,用于初始化时添加敌方角色
    private void AddEnemy()
    {
        int num = 2;
        for (int i = 0; i < num; i++)
        {
            AddEnemyById(1001);
        }
    }

    private void AddEnemyById(int id)
    {
        if(id == 0) return ;
        enemyNum++;
        var obj = ContainerManager.Instance.AddEnemy();
        battleData.enemies.Add(obj);
    }

    public void GetHandCard()
    {
        EventCenter.Broadcast(EventDefine.OnGetCard);
    }
    public void GetHandCardByID(int id)
    {
        EventCenter.Broadcast(EventDefine.OnGetCardByID , id);
    }

    public void OnGetCardByID(int id)
    {
        battleData.handCards.Add(id);
    }

    public void OnDeleteCardByIndex(int idx)
    {
        battleData.handCards.Remove(idx);
    }

    // 临时使用删除一张卡
    public void RemoveOneHandCard()
    {
        EventCenter.Broadcast(EventDefine.OnDeleteCard);
    }

    public void OnEnemyDeath(int id , CharacterType tp)
    {
        Debug.Log(tp);
        if(tp == CharacterType.Enemy) 
        {
            enemyNum--;
            if (enemyNum == 0)
            {
                GameManager.Instance.BettleWin(id);
                isBattle = false;
                battleData.Init();
            }
        }
    }

    public void OnBeforePlayerTurn()
    {
        Debug.Log($"牌堆卡牌数量{battleData.deckCards.Count}");
        battleData.current_turn++;
        if(battleData.current_turn == 1)
        {
            OnEnterFirstTurn();
        }
        int getCardNum = 2;
        int cardChangeNum = battleData.maxCardCount - battleData.handCards.Count;
        if(cardChangeNum <= 0) cardChangeNum = 0;
        int canGetCardNum = RandomUtil.Min(getCardNum, battleData.deckCards.Count,cardChangeNum);
        for (int i = 0; i < getCardNum && i < battleData.deckCards.Count; i++)
        {
            EventCenter.Broadcast(EventDefine.OnGetCardByID, battleData.deckCards[i]);
        }
        Debug.Log($"牌堆卡牌数量[{battleData.deckCards.Count}]，移除数量[{canGetCardNum}]");
        battleData.deckCards.RemoveRange(0, canGetCardNum);

        if (canGetCardNum < getCardNum && cardChangeNum > canGetCardNum)
        {
            RandomUtil.Shuffle(battleData.discardPile);
            battleData.deckCards = new List<int>(battleData.discardPile);
            battleData.discardPile.Clear();

            canGetCardNum = getCardNum - canGetCardNum;
            for (int i = 0; i < canGetCardNum && i < battleData.deckCards.Count; i++)
            {
                EventCenter.Broadcast(EventDefine.OnGetCardByID, battleData.deckCards[i]);
            }
            Debug.Log($"牌堆卡牌数量[{battleData.deckCards.Count}]，移除数量[{canGetCardNum}]");
            battleData.deckCards.RemoveRange(0, canGetCardNum);
        }
        Debug.Log($"{battleData.current_turn} --- {battleData.Cfg.enemy_id.Count}");
        if(battleData.current_turn <= battleData.Cfg.enemy_id.Count)
        {
            AddEnemyById(battleData.Cfg.enemy_id[battleData.current_turn - 1][0]);
        }
    }

    public void OnFinishPlayerTurn()
    {
        Debug.Log($"{battleData.current_turn} -- {battleData.Cfg.enemy_id.Count}");
        if(battleData.current_turn <= battleData.Cfg.enemy_id.Count)
        {
            AddEnemyById(battleData.Cfg.enemy_id[battleData.current_turn - 1][1]);
        }
    }

    public void OnEnterFirstTurn()
    {
        battleData.deckCards = new List<int>(GameManager.Instance.Data.hasCard);
        RandomUtil.Shuffle(battleData.deckCards);
        int getCardNum = 2;
        int canGetCardNum = Math.Min(getCardNum , battleData.deckCards.Count);
        for(int i = 0 ; i < getCardNum && i < battleData.deckCards.Count ; i++)
        {
            EventCenter.Broadcast(EventDefine.OnGetCardByID , battleData.deckCards[i]);
        }
        battleData.deckCards.RemoveRange(0,canGetCardNum);
    }

    public int getPlayerTeamNum()
    {
        return battleData.playerTeam.Count;
    }

    #region 外部接口,提供数据

    // 获取所有敌方目标
    public List<BaseCharacter> getAllEnemy()
    {
        return battleData.enemies;
    }

    public BaseCharacter getEnemyByIndex(int pos)
    {
        if(pos < 0 || pos >= battleData.enemies.Count) return null;
        return battleData.enemies[pos];
    }

    // 获取所有我方目标
    public List<BaseCharacter> getAllPlayerTeam()
    {
        return battleData.playerTeam;
    }

    // 获取巫真
    public BaseCharacter getPlayer()
    {
        return battleData.player;
    }

    public BaseCharacter getPlayerTeamByIndex(int pos)
    {
        if(pos < 0 || pos >= battleData.playerTeam.Count) return null;
        return battleData.playerTeam[pos];
    }

    #endregion

    private int currentSelectCard = -1; // 使用私有字段作为后备存储

    public int CURRENT_SELECT_CARD
    {
        get { return currentSelectCard; }
        set 
        {
            if (currentSelectCard != value)
            {
                int previousCard = currentSelectCard;
                currentSelectCard = value; // 更新实际值
                Debug.Log($"Selected card changed from {previousCard} to {currentSelectCard}");
            }
        }
    }

}
