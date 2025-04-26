using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData
{
    public int id;
    public int maxEnemyNum;
    public int maxPlayerTeamNum;
    public List<BaseCharacter> enemies;
    public List<BaseCharacter> playerTeam;
    public BaseCharacter player;
    public BattleData()
    {
        id = 0;
        maxEnemyNum = 3;
        maxPlayerTeamNum = 5;
        enemies = new List<BaseCharacter>();
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
    // [Tooltip("卡牌预制体")] public GameObject handcard;
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
        battleData = new BattleData();
        EventCenter.AddListener(EventDefine.OnBattleStart, OnBattleStart);    // 战斗开始
        EventCenter.AddListener<int>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 敌人死亡
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.OnBattleStart, OnBattleStart);    // 移除
        EventCenter.RemoveListener<int>(EventDefine.OnEnemyDeath, OnEnemyDeath);    // 移除监听
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

    // 临时使用,用于初始化时添加我方角色
    private void AddPlayer(int PlayerNum, int CardNum)
    {
        PlayerNum = Math.Min(PlayerNum, 1);
        for (int i = 0; i < PlayerNum; i++)
        {
            if (ContainerManager.Instance.Players[i].childCount > 0) continue;  // 防止重复添加Player
            var go = Instantiate(player, ContainerManager.Instance.Players[i]);
            battleData.playerTeam.Add(go.GetComponent<BaseCharacter>());
            battleData.player = go.GetComponent<BaseCharacter>();
        }
        CardNum = Math.Min(CardNum, 5 - PlayerNum);
        for (int i = 0; i < CardNum; i++)
        {
            Instantiate(enemy, ContainerManager.Instance.Players[i + PlayerNum]);
        }
    }
    // 临时使用,用于初始化时添加敌方角色
    private void AddEnemy(int num)
    {
        num = Math.Min(num, 3);
        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(enemy, ContainerManager.Instance.Enemies[i]);
            BaseEnemy obj = go.GetComponent<BaseEnemy>();
            obj.Index = i;
            battleData.enemies.Add(obj);
        }
    }

    public void GetHandCard()
    {
        EventCenter.Broadcast(EventDefine.OnGetCard);
    }
    public void GetHandCardByID(int id)
    {
        EventCenter.Broadcast(EventDefine.OnGetCardByID , id);
    }

    // 临时使用删除一张卡
    public void RemoveOneHandCard()
    {
        EventCenter.Broadcast(EventDefine.OnDeleteCard);
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
