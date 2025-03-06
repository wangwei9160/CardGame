using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;
    public override UILAYER Layer => UILAYER.M_POP_LAYER;

    public Image reward;
    public Transform Rewards;
    public Button GoMergeBtn;

    protected override void Start()
    {
        base.Start();
        Rewards.gameObject.SetActive(false);    // 设置隐身
        GoMergeBtn.onClick.AddListener(() =>
        {
            // 打开合成界面
            EventCenter.Broadcast(EventDefine.OnMergePanelShow);
            UIManager.Instance.Hide(Name);
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
    }

    private void ShowReward()
    {
        Rewards.gameObject.SetActive(true);
    }
}
