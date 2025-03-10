using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopInfoPanel : UIViewBase
{
    public override UIViewType Type => base.Type;
    public override UILAYER Layer => UILAYER.M_TIP_LAYER;

    [Header("��ť")]
    [Tooltip("���ð�ť")] public Button settingBtn;   // ���ð�ť
    [Tooltip("���鰴ť")] public Button deckBtn;      // ���鰴ť
    [Tooltip("ͼ����ť")] public Button cardBookBtn;  // ͼ����ť

    [Header("�ı�")]
    [Tooltip("Ѫ��")] public Text hpText; // Ѫ��
    [Tooltip("Ѫ��")] public Slider hpSlider; //  Ѫ��
    [Tooltip("Ǯ")] public Text moneyText;   // Ǯ������
    [Tooltip("�ؿ���Ϣ")] public Text levelInfo;   // �ؿ���Ϣ

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
        OnMoneyChange(GameManager.Instance.Data.money);     // ���������õ�Ǯ������
        // ��ʾ��Ϣ�ĸ���
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
