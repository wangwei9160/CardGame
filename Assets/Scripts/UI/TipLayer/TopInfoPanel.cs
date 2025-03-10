using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopInfoPanel : UIViewBase
{
    public override UIViewType Type => base.Type;
    public override UILAYER Layer => UILAYER.M_TIP_LAYER;

    [Header("按钮")]
    [Tooltip("设置按钮")] public Button settingBtn;   // 设置按钮
    [Tooltip("牌组按钮")] public Button deckBtn;      // 牌组按钮
    [Tooltip("图鉴按钮")] public Button cardBookBtn;  // 图鉴按钮

    [Header("文本")]
    [Tooltip("血量")] public Text hpText; // 血量
    [Tooltip("血条")] public Slider hpSlider; //  血条
    [Tooltip("钱")] public Text moneyText;   // 钱的数量
    [Tooltip("关卡信息")] public Text levelInfo;   // 关卡信息

    protected override void Start()
    {
        base.Start();
        settingBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Show("SettingUI");
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

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<int>(EventDefine.OnMoneyChange , OnMoneyChange);
        EventCenter.AddListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener<int>(EventDefine.OnMoneyChange, OnMoneyChange);
        EventCenter.RemoveListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
    }

    public override void Show()
    {
        base.Show();
        OnMoneyChange(GameManager.Instance.Data.money);     // 从数据里拿到钱的数量
        // 显示信息的更新
        levelInfo.text = string.Format(GameString.STAGEINFO,
            Constants.MapLength[GameManager.Instance.Data.CurrentLvel] - GameManager.Instance.Data.CurrentStage,
            Constants.MapName[GameManager.Instance.Data.CurrentLvel]);
    }

    private void OnMoneyChange(int val)
    {
        moneyText.text = val.ToString();
    }

    private void OnHpChange(int hp, int maxHp, int id)
    {
        hpText.text = string.Format("{0}/{1}", hp, maxHp);
        hpSlider.value = 1.0f * hp / maxHp;
    }

}
