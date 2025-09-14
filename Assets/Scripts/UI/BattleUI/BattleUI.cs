using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    [Header("按钮")]
    [Tooltip("结束按钮")] public Button endTurnBtn;   // 回合结束按钮
    private bool isLock = false;                    // 回合结束锁

    [Header("文本")]
    [Tooltip("回合计数")] public Text turnInfo;   // 回合计数
    [Tooltip("法力值")] public Text magicPowerInfo;   // 法力值信息

    public Transform cardArea;

    public List<GameObject> Cards;
    public ShowCardUICom showCard;
    public override void Init(string str, string data)
    {
        base.Init(str, data);
        //cfg = JsonUtility.FromJson<EchoEventConfig>(data);
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.AddListener(EventDefine.OnPlayerTurnStart, OnPlayerTurn);
        EventCenter.AddListener(EventDefine.OnGetCard, OnGetCard);
        EventCenter.AddListener<int>(EventDefine.OnGetCardByID, OnGetCardByID);
        EventCenter.AddListener(EventDefine.OnDeleteCard, OnDeleteCard);
        EventCenter.AddListener(EventDefine.ON_CARD_DRAG_START, ON_CARD_DRAG_START);
        EventCenter.AddListener(EventDefine.ON_CARD_DRAG_STOP, ON_CARD_DRAG_STOP);
        EventCenter.AddListener<int>(EventDefine.OnDeleteCardByIndex, OnDeleteCardByIndex);
        EventCenter.AddListener<int,int>(EventDefine.ON_CARD_SELECT, ON_CARD_SELECT);
        EventCenter.AddListener<int>(EventDefine.ON_CARD_UNSELECT, ON_CARD_UNSELECT);
        EventCenter.AddListener(EventDefine.AdjustCardPosition, StartAdjustCard);
        EventCenter.AddListener<int , int>(EventDefine.OnMagicPowerChange, OnMagicPowerChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.RemoveListener(EventDefine.OnPlayerTurnStart, OnPlayerTurn);
        EventCenter.RemoveListener(EventDefine.OnGetCard, OnGetCard);
        EventCenter.RemoveListener<int>(EventDefine.OnGetCardByID, OnGetCardByID);
        EventCenter.RemoveListener(EventDefine.OnDeleteCard, OnDeleteCard);
        EventCenter.RemoveListener(EventDefine.ON_CARD_DRAG_START, ON_CARD_DRAG_START);
        EventCenter.RemoveListener(EventDefine.ON_CARD_DRAG_STOP, ON_CARD_DRAG_STOP);
        EventCenter.RemoveListener<int>(EventDefine.OnDeleteCardByIndex, OnDeleteCardByIndex);
        EventCenter.RemoveListener<int,int>(EventDefine.ON_CARD_SELECT, ON_CARD_SELECT);
        EventCenter.RemoveListener<int>(EventDefine.ON_CARD_UNSELECT, ON_CARD_UNSELECT);
        EventCenter.RemoveListener(EventDefine.AdjustCardPosition, StartAdjustCard);
        EventCenter.RemoveListener<int , int>(EventDefine.OnMagicPowerChange, OnMagicPowerChange);
    }

    protected override void Awake()
    {
        base.Awake();
        // spacingList = new float[] {0f,0f,300f,300f,300f,200f,160f,140f,120f};
        // applyRotationTime = 2;
        showCard = transform.Find("HandCard/ShowCard").GetComponent<ShowCardUICom>();
        showCard.Hide();
        endTurnBtn = transform.Find("MiddleCanvas/Middle/EndTurnBtn").GetComponent<Button>();
        turnInfo = transform.Find("TopCanvas/Top/LevelInfo/currentTurn").GetComponent<Text>();
        magicPowerInfo = transform.Find("MiddleCanvas/Middle/EndTurnBtn/Info/MagicPowerText").GetComponent<Text>();
        cardArea = transform.Find("HandCard/HandCardArea");
        MASK = transform.Find("HandCard/Mask");
        MASK.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        
        endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(0.6f);
        endTurnBtn.onClick.AddListener(() =>
        {
            if (isLock) return;
            // 切换到敌方回合
            isLock = true;      // 防止连点异常
            StartCoroutine(WaitTimeUnLock());   // 定时解锁
            endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(0.6f);
            BattleManager.Instance.BaseBattlePlayer.ChangeState(BattleEvent.FinishPlayerTurn);
        });
    }

    public override void Show()
    {
        base.Show();
        OnMagicPowerChange(GameManager.Instance.Data.MagicPower , GameManager.Instance.Data.MaxMagicPower);// 法力值信息的更新
    }

    public override void Close()
    {
        base.Close();
        //if(cfg.prefabName != null)
        //{
        //    var go = GameObject.Find(cfg.prefabName);
        //    Destroy(go);
        //}
    }

    IEnumerator WaitTimeUnLock()
    {
        yield return new WaitForSeconds(1f);
        isLock = false;
    }

    // 修正当前回合数
    private void OnBeforePlayerTurn()
    {
        GameManager.Instance.Data.CurrentTurn++;
        turnInfo.text = GameManager.Instance.Data.CurrentTurn.ToString();
    }

    private void OnPlayerTurn()
    {
        // 进入到玩家可操作回合
        isLock = false;
        endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(1f);
        GameManager.Instance.OnMagicPowerChange(1,1);
        //endTurnBtn.GetComponent<Image>().color = Color.green;
    }
    
    private void OnMagicPowerChange(int val , int maxVal)
    {
        magicPowerInfo.text = string.Format(GameString.MAGICEINFO, val, maxVal);
    }
    public int ID = 1;
    public void OnGetCard()
    {
        if(cardArea.childCount == maxCardCount) return;
        GameObject card = ResourceUtil.GetCard();
        var go = Instantiate(card , cardArea);
        CardUI cardUI = go.GetComponent<CardUI>();
        CardShow cardShow = go.GetComponent<CardShow>();
        // 随机获得一张卡牌
        List<CardClass> cards = CardConfig.GetAll();
        CardClass cfg = RandomUtil.GetRandomValueInList(cards);
        cardUI.SetData(cardArea.childCount, cfg);
        cardShow.SetData(cfg);
        card.name = $"卡牌-{ID}-{cfg.name}";
        ID++;
        AdjustCardPosition();
    }

    public void OnGetCardByID(int id)
    {
        if (cardArea.childCount == maxCardCount) return;
        GameObject card = ResourceUtil.GetCard();
        var go = Instantiate(card, cardArea);
        CardUI cardUI = go.GetComponent<CardUI>();
        CardShow cardShow = go.GetComponent<CardShow>();
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        card.name = $"卡牌-{ID}-{cfg.name}";
        cardUI.SetData(cardArea.childCount, cfg);
        cardShow.SetData(cfg);
        ID++;
        AdjustCardPosition();
    }

    public void OnDeleteCard()
    {
        if(cardArea.childCount == 0) return;
        Destroy(cardArea.GetChild(0).gameObject);
        StartCoroutine(AdjustCardIEnumerator());
    }

    public void OnDeleteCardByIndex(int id)
    {
        Destroy(cardArea.GetChild(id).gameObject);
        StartCoroutine(AdjustCardIEnumerator());
    }

    public void ON_CARD_SELECT(int pos , int id)
    {
        int cardCount = cardArea.childCount;
        bool applyRotation = cardCount >= applyRotationTime;
        float middleOffset = (cardCount - 1) / 2f;
        float offset = pos - middleOffset;
        float spacing = spacingXposList[cardCount];
        float spacingY = spacingYposList[cardCount];
        float fixPos = Mathf.Abs(offset) * spacingY;
        Vector3 posistion = cardArea.GetChild(pos).position + new Vector3(0 , 100f + fixPos , 0);
        showCard.SetData(pos, posistion , id);
        AdjustCardPositionWhenCardSelect(pos);
    }

    public void ON_CARD_UNSELECT(int id)
    {
        if(id == -1) return ;
        showCard.TryHide(id);
        cardArea.GetChild(id).GetComponent<CardUI>().Show();
        if(!showCard.IsSomethingSlect())
        {
            AdjustCardPosition();
        }
    }

    public void StartAdjustCard()
    {
        StartCoroutine(AdjustCardIEnumerator());
    }

    IEnumerator AdjustCardIEnumerator()
    {
        yield return new WaitForEndOfFrame();
        AdjustCardPosition();
    }

    [Header("持有卡牌上限")]
    public int maxCardCount = 8;

    [Header("持有不同数量时的x坐标间隔(类型:float)")]
    public float[] spacingXposList = {0f,0f,300f,280,260f,240f,200f,160f,150f};
    [Header("持有不同数量时的y坐标间隔(类型:float)")]
    public float[] spacingYposList = {0f,0f,60f,50f,50f,40f,32f,28f,26f};
    
    [Header("持有多少张卡牌时需要携带一点旋转(类型:int)")]
    public int applyRotationTime = 2;
    [Header("持有不通数量卡牌时统一两卡之间旋转幅度(类型:float)[数量0-8]")]
    public float[] rotationList = {0f,0f,10f,10f,10f,8f,8f,8f,9f};
    [Header("有选中卡牌时左右偏移量")]
    public int spaceWhenSelect = 30;

    public void AdjustCardPosition()
    {
        int cardCount = cardArea.childCount;
        if(cardCount == 0) return;
        
        if(cardCount == 1) 
        {
            RectTransform card = cardArea.GetChild(0) as RectTransform;
            card.anchoredPosition = Vector2.zero;
            card.localRotation = Quaternion.Euler(0, 0, 0);
            card.localScale = Vector3.one;
            card.GetComponent<CardUI>().SetIndex(0);
            return;
        }

        // 计算中间偏移量
        float middleOffset = (cardCount - 1) / 2f;
        float spacingX = spacingXposList[cardCount];
        float spacingY = spacingYposList[cardCount];
        bool applyRotation = cardCount >= applyRotationTime;
        float rot = rotationList[cardCount];
        
        for(int i = 0; i < cardCount; i++)
        {
            float offset = i - middleOffset;
            float xPos = (i - (cardCount - 1) / 2f) * spacingX;
            float yPos = 0f;
            
            if(applyRotation) 
                yPos = -Mathf.Abs(offset) * spacingY; 
            
            Vector2 position = new Vector2(xPos, yPos);
            
            RectTransform card = cardArea.GetChild(i) as RectTransform;
            card.GetComponent<CardUI>().SetIndex(i);
            // 使用anchoredPosition而不是localPosition
            card.anchoredPosition = position;
            float rotationZ = applyRotation ? -offset * rot : 0f;
            card.localRotation = Quaternion.Euler(0, 0, rotationZ);
            
        }
    }

    public void AdjustCardPositionWhenCardSelect(int id)
    {
        int cardCount = cardArea.childCount;
        if(cardCount <= 1) return;
        AdjustCardPosition();
        
        for(int i = 0; i < cardCount; i++)
        {
            if(id == i) continue;

            float op = i <= id ? -1 : 1;
            float xPos = op * spaceWhenSelect;
            Vector2 position = new Vector2(xPos, 0);
            RectTransform card = cardArea.GetChild(i) as RectTransform;
            card.anchoredPosition += position;
        }
    }

    #region Drag Event Mask

    private Transform MASK;

    public void ON_CARD_DRAG_START()
    {
        MASK.gameObject.SetActive(true);
    }

    public void ON_CARD_DRAG_STOP()
    {
        MASK.gameObject.SetActive(false);
    }

    #endregion

}