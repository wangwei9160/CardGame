using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;
    public override UILAYER Layer => UILAYER.M_POP_LAYER;

    public Image reward;
    public GameObject WaitForShow;      // 等待被打开
    public Transform Rewards;
    public Button GoMergeBtn;

    public GameObject RewardPrefab;   // 预制体
    protected override void Start()
    {
        base.Start();
        WaitForShow.gameObject.SetActive(false);
        GoMergeBtn.onClick.AddListener(() =>
        {
            // 打开合成界面
            EventCenter.Broadcast(EventDefine.OnMergePanelShow);
            UIManager.Instance.Hide(Name);
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
            UIManager.Instance.Show(GameString.SELECTCARDREWARD);   // 打开选择卡牌界面
        });
    }

    public override void OnAddlistening()
    {
        EventCenter.AddListener(EventDefine.AfterEffectShowReward , ShowReward);
    }

    public override void OnRemovelistening()
    {
        EventCenter.RemoveListener(EventDefine.AfterEffectShowReward, ShowReward);
    }

    public override void Init(string str, GameObject obj)
    {
        base.Init(str);

        reward.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position); // 设置奖励的初始位置为最后一个死亡的角色的位置
        Debug.Log(reward.transform.position);
    }

    private void ShowReward()
    {
        WaitForShow.gameObject.SetActive(true);
    }

    public override void Show()
    {
        base.Show();
        int _money = GameManager.Instance.MoneyReward();
        Rewards.GetChild(0).GetComponentInChildren<Text>().text = string.Format("获得{0}金币" , _money);
        Rewards.GetChild(0).gameObject.SetActive(_money > 0);
        int _cnt = GameManager.Instance.CardReward().Count;
        Rewards.GetChild(1).GetComponentInChildren<Text>().text = "选择卡牌奖励";
        Rewards.GetChild(1).gameObject.SetActive(_cnt > 0);
    }
}
