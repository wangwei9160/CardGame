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
            UIManager.Instance.Close(Name); // 需要删除才能实现stack的层次显示
        });
        for(int i = 0; i < Cards.childCount; i++)
        {
            int idx = i + 1;
            Cards.GetChild(i).Find("ConfirmBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                EventCenter.Broadcast(EventDefine.SelectCardReward , idx);
            });
        }
    }

}
