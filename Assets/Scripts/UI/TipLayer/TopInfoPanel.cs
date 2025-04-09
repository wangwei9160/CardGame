using System;
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

    public Transform treasures;

    protected override void Awake()
    {
        base.Awake();
        treasures = transform.Find("TopCanvas/Top/Treasures/Scroll View/Viewport/Content");
    }

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
        cardBookBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Show("HandBookUI");
        });
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<int>(EventDefine.OnMoneyChange , OnMoneyChange);
        EventCenter.AddListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
        EventCenter.AddListener(EventDefine.ON_LEVEL_INFO_CHANGE, Refresh);
        EventCenter.AddListener(EventDefine.ON_TREASURE_UPDATE_SHOW, OnUpdateTreasureShow);
        
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener<int>(EventDefine.OnMoneyChange, OnMoneyChange);
        EventCenter.RemoveListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
        EventCenter.RemoveListener(EventDefine.ON_LEVEL_INFO_CHANGE, Refresh);
        EventCenter.RemoveListener(EventDefine.ON_TREASURE_UPDATE_SHOW, OnUpdateTreasureShow);
    }

    public override void Show()
    {
        base.Show();
        OnMoneyChange(GameManager.Instance.Data.money);     // ���������õ�Ǯ������
        Refresh();
    }

    public void Refresh()
    {
        // ��ʾ��Ϣ�ĸ���
        levelInfo.text = string.Format(GameString.STAGEINFO,
            Constants.MapLength[GameManager.Instance.Data.CurrentLvel] - GameManager.Instance.Data.CurrentStage,
            Constants.MapName[GameManager.Instance.Data.CurrentLvel]);
        OnUpdateTreasureShow();
    }

    private void OnUpdateTreasureShow()
    {
        List<TreasureBase> _list = GameManager.Instance.GetAllTreasure();
        for(int i = 0; i <  _list.Count; i++)
        {
            Transform obj;
            if(i < treasures.childCount)
            {
                obj = treasures.GetChild(i);
            }
            else
            {
                var go = new GameObject();
                go.AddComponent<TreasureRender>();
                go.transform.SetParent(treasures);
                obj = go.transform;
            }
            TreasureRender item = obj.GetComponent<TreasureRender>();
            item.SetData(index, _list[i].ID);
        }

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
