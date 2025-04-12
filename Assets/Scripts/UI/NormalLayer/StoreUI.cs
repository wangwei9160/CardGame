using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : UIViewBase
{
    public Button outBtn;

    protected override void Start()
    {
        base.Start();
        outBtn = transform.Find("Store/outBtn").GetComponent<Button>();
        outBtn.onClick.AddListener(GoOut);
    }

    public void GoOut()
    {
        UIManager.Instance.Close(name);
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

}
