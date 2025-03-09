using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    [Header("按钮")]
    [Tooltip("设置按钮")]public Button settingBtn;   // 设置按钮
    [Tooltip("牌组按钮")] public Button deckBtn;      // 牌组按钮
    [Tooltip("图鉴按钮")] public Button cardBookBtn;  // 图鉴按钮
    [Tooltip("结束按钮")] public Button endTurnBtn;   // 回合结束按钮
    private bool isLock = false;                    // 回合结束锁
    public Transform CardsTransform;    // 临时存放所有卡牌的位置

    [Header("文本")]
    [Tooltip("血量")] public Text hpText; // 血量
    [Tooltip("血条")] public Slider hpSlider; //  血条
    [Tooltip("钱")] public Text moneyText;   // 钱的数量
    [Tooltip("回合计数")] public Text turnInfo;   // 回合计数
    [Tooltip("关卡信息")] public Text levelInfo;   // 关卡信息
    [Tooltip("法力值")] public Text magicPowerInfo;   // 法力值信息

    public List<GameObject> Cards;

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.AddListener(EventDefine.OnPlayerTurnStart, OnPlayerTurn);
        EventCenter.AddListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
        EventCenter.AddListener<int , int>(EventDefine.OnMagicPowerChange, OnMagicPowerChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.RemoveListener(EventDefine.OnPlayerTurnStart, OnPlayerTurn);
        EventCenter.RemoveListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
        EventCenter.RemoveListener<int , int>(EventDefine.OnMagicPowerChange, OnMagicPowerChange);
    }

    protected override void Start()
    {
        base.Start();
        Cards = new List<GameObject>();
        settingBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Show("SettingUI");
        });
        endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(0.6f);
        endTurnBtn.onClick.AddListener(() =>
        {
            if (isLock) return;
            // 切换到敌方回合
            isLock = true;      // 防止连点异常
            StartCoroutine(WaitTimeUnLock());   // 定时解锁
            endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(0.6f);
            GameManager.Instance.FinishTurn();
        });
        //deckBtn.onClick.AddListener(() =>
        //{
        //    UIManager.Instance.Show("DeckUI");
        //});
        //cardBookBtn.onClick.AddListener(() =>
        //{
        //    UIManager.Instance.Show("CardBookUI");
        //});
    }

    public override void Show()
    {
        base.Show();
        isLock = true;
        OnMoneyChange(GameManager.Instance.Data.money);     // 从数据里拿到钱的数量
        // 显示信息的更新
        levelInfo.text = string.Format(GameString.STAGEINFO,
            Constants.MapLength[GameManager.Instance.Data.CurrentLvel] - GameManager.Instance.Data.CurrentStage,
            Constants.MapName[GameManager.Instance.Data.CurrentLvel]);
        magicPowerInfo.text = string.Format(GameString.MAGICEINFO, GameManager.Instance.Data.MagicPower , GameManager.Instance.Data.MaxMagicPower);
    }

    IEnumerator WaitTimeUnLock()
    {
        yield return new WaitForSeconds(0.5f);
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

    private void OnHpChange(int hp, int maxHp, int id)
    {
        hpText.text = string.Format("{0}/{1}",hp,maxHp);
        hpSlider.value = 1.0f * hp / maxHp;
    }

    private void OnMoneyChange(int val)
    {
        moneyText.text = val.ToString();
    }
    
    private void OnMagicPowerChange(int val , int maxVal)
    {
        magicPowerInfo.text = string.Format(GameString.MAGICEINFO, GameManager.Instance.Data.MagicPower, GameManager.Instance.Data.MaxMagicPower);
    }

}