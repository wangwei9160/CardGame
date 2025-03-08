using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergePanel : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;
    public override UILAYER Layer => UILAYER.M_NORMAL_LAYER;

    public Button GoBtn;

    protected override void Start()
    {
        base.Start();
        GoBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close(this.Name);    // 关闭这个界面即可
            EventCenter.Broadcast(EventDefine.OnBattleStart);
            // 根据合成结果选择不同分支，具体分支由GameManager操控
        });
    }

}
