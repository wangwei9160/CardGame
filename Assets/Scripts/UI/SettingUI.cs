using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIViewBase
{
    public override string Name => "SettingUI";

    public Button CloseBtn;

    protected override void Start()
    {
        CloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close(Name);
        });
    }

}

