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
    [Tooltip("卡牌预制体")] public GameObject handcard;
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

    public void GetHandCard()
    {
        int curNum = ContainerManager.Instance.HandCard.childCount;
        if(curNum < 8) 
        {
            GameObject handCard = Instantiate(handcard , ContainerManager.Instance.HandCard);
        }
        AdjustCardPosition();
    }

    // 临时使用删除一张卡
    public void RemoveOneHandCard()
    {
        int curNum = ContainerManager.Instance.HandCard.childCount;
        if(curNum > 0) 
        {
            Destroy(ContainerManager.Instance.HandCard.GetChild(0).gameObject);
        }
        StartCoroutine(AdjustCardIEnumerator());
    }

    IEnumerator AdjustCardIEnumerator()
    {
        yield return new WaitForEndOfFrame();
        AdjustCardPosition();
    }

    [Tooltip("持有不同数量卡牌时的间隔(类型:float)[数量0-8]")]
    public float[] spacingList = {0f,0f,4f,3f,3f,2f,1.5f,1.3f,1f};
    [Tooltip("持有多少张卡牌时需要携带一点旋转(类型:int)")]
    public int applyRotationTime = 5;

    public void AdjustCardPosition()
    {
        int cardCount = ContainerManager.Instance.HandCard.childCount;
        if(cardCount <= 1) 
        {
            Transform card = ContainerManager.Instance.HandCard.GetChild(0);
            card.localPosition = new Vector3(0,0,0);
            return;
        }
        // 计算中间偏移量
        float middleOffset = (cardCount - 1) / 2f;

        float spacing = spacingList[cardCount];
        bool applyRotation = cardCount >= applyRotationTime ? true : false ;
        
        for(int i = 0; i < cardCount; i++)
        {
            float offset = i - middleOffset;
            float xPos = (i - (cardCount - 1) / 2f) * spacing;
            float yPos = 0f;
            if(applyRotation) yPos = -Mathf.Abs(offset) * 0.2f * spacing; 
            
            Vector3 position = new Vector3(xPos, yPos, 0);
            
            Transform card = ContainerManager.Instance.HandCard.GetChild(i);
            card.localPosition = position;
            float rotationZ = applyRotation ? -offset * 10f : 0f;
            card.localRotation = Quaternion.Euler(0, 0, rotationZ);
            
            card.name = $"Card_{i+1}";
            card.GetComponent<SpriteRenderer>().sortingOrder = i + 1;
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
