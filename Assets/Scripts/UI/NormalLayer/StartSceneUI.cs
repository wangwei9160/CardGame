using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUI : UIViewBase
{
    protected override void Start()
    {
        base.Start();
        Hide();
    }
    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener(EventDefine.OnStartSceneUIShow , Show);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener(EventDefine.OnStartSceneUIShow, Show);
    }
}
