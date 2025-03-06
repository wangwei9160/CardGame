using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;
    public override UILAYER Layer => UILAYER.M_POP_LAYER;


    public Button CloseBtn;

    protected override void Start()
    {
        CloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close(Name);
        });
    }

}

