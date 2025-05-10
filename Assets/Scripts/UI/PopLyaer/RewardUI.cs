using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;
    public override UILAYER Layer => UILAYER.M_POP_LAYER;

    // Obj
    public Transform[] Panels;
    public GameObject MASK; // 遮罩

    public Image reward;
    public GameObject WaitForShow;      // 等待被打开
    public Transform Rewards;
    public Button GoMergeBtn;

    // SelectCardPanel

    public Button BreakBtn;
    public Button CloseBtn;
    private int _money;

    public Transform Cards;


    protected override void Start()
    {
        base.Start();
        Panels = new Transform[2];
        Panels[0] = transform.Find("RewardPanel");
        Panels[1] = transform.Find("SelectCardRewardPanel");
        ShowPanel(0);

        WaitForShow.gameObject.SetActive(false);
        GoMergeBtn.onClick.AddListener(() =>
        {
            // 打开合成界面
            EventCenter.Broadcast(EventDefine.OnMergePanelShow);
            UIManager.Instance.Close(Name);
            EventCenter.Broadcast(EventDefine.SelectMoneyReward, 0);    // 不拿
            EventCenter.Broadcast(EventDefine.SelectCardReward, 0);
        });
        Rewards.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SelectMoneyReward , 1);    // 拿
            Rewards.GetChild(0).gameObject.SetActive(false);
        });
        Rewards.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            ShowPanel(1);
        });

        // SelectCardPanel
        _money = 100;
        BreakBtn.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SelectCardReward, _money);
            ShowPanel(0);
        });
        CloseBtn.onClick.AddListener(() =>
        {
            ShowPanel(0);
        });
        for (int i = 0; i < Cards.childCount; i++)
        {
            int idx = i + 1;
            Cards.GetChild(i).Find("ConfirmBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                EventCenter.Broadcast(EventDefine.SelectCardReward, idx);
                ShowPanel(0);
            });
        }
    }

    public override void OnAddlistening()
    {
        EventCenter.AddListener(EventDefine.AfterEffectShowReward , ShowReward);
    }

    public override void OnRemovelistening()
    {
        EventCenter.RemoveListener(EventDefine.AfterEffectShowReward, ShowReward);
    }

    public override void Init(string str, GameObject obj , string data)
    {
        base.Init(str);

        reward.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position); // 设置奖励的初始位置为最后一个死亡的角色的位置
        Debug.Log(reward.transform.position);
    }

    public override void Init(string str, object[] data)
    {
        base.Init(str);
        if(data[0] is GameObject obj)
        {
            reward.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position); // 设置奖励的初始位置为最后一个死亡的角色的位置
        }
        if(data[1] is List<int> _list)
        {
            for (int i = 0; i < Cards.childCount; i++)
            {
                var card = Cards.GetChild(i);
                int idx = i + 1;
                if(i < _list.Count)
                {
                    card.gameObject.SetActive(true);
                    card.Find("Card").GetComponent<CardUI>().SetShowOnly(true);
                    card.Find("Card").GetComponent<CardShow>().SetData(_list[i]);
                }else {
                    card.gameObject.SetActive(false);
                }
                
            }
        }
        Debug.Log(reward.transform.position);
    }

    private void ShowPanel(int id)
    {
        for(int i = 0; i < Panels.Length; i++)
        {
            Panels[i].gameObject.SetActive(i == id);
        }
        if (id == 0) Refresh(); // 回到开始界面 强制刷新
    }

    private void ShowReward()
    {
        MASK.gameObject.SetActive(true);
        WaitForShow.gameObject.SetActive(true);
    }

    public override void Show()
    {
        base.Show();
        Refresh();
    }

    public void Refresh()
    {
        int _money = GameManager.Instance.MoneyReward();
        Rewards.GetChild(0).GetComponentInChildren<Text>().text = string.Format("获得{0}金币", _money);
        Rewards.GetChild(0).gameObject.SetActive(_money > 0);
        int _cnt = GameManager.Instance.CardReward().Count;
        Rewards.GetChild(1).GetComponentInChildren<Text>().text = "选择卡牌奖励";
        Rewards.GetChild(1).gameObject.SetActive(_cnt > 0);
    }
}
