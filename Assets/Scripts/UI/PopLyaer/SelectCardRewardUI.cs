using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCardRewardUI : UIViewBase
{
    public override UILAYER Layer => UILAYER.M_POP_LAYER;

    public Button BreakBtn;
    public Button CloseBtn;
    private int _money;

    public Transform Cards;

    protected override void Start()
    {
        base.Start();
        _money = 100;
        BreakBtn.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SelectCardReward, _money);
        });
        CloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide(Name);
        });
        for(int i = 0; i < Cards.childCount; i++)
        {
            int idx = i + 1;
            Cards.Find("ConfirmBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                EventCenter.Broadcast(EventDefine.SelectCardReward , idx);
            });
        }
    }

}
