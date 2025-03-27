using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectSelectRender : MonoBehaviour
{
    public Button btn;
    public Text title;
    public EffectClass effectClass;

    Dictionary<int, Action> actions = new Dictionary<int, Action>
    {
        {111 , OnEnterCombat },
        {112 , OnEnterRelics },
        {113,OnEnterSkip },
        {114, OnEnterBuff },
        {115, OnEnterFriend},
        {116 , OnEnterTrade },
        {117 , OnEnterDelete }
    };

    private void Awake()
    {
        btn = GetComponent<Button>();
        title = transform.Find("Title").GetComponent<Text>();
    }

    private void Start()
    {
        
        btn.onClick.AddListener(OnButtonClick);
    }

    public void SetData(EffectClass data)
    {
        effectClass = data;
        title.text = effectClass.op_name;
    }

    public void OnButtonClick()
    {
        actions[effectClass.effect_ids]();
    }

    static public void OnEnterCombat()
    {
        Debug.Log("OnEnterCombat");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

    static public void OnEnterRelics()
    {
        Debug.Log("OnEnterRelics");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

    static public void OnEnterBuff()
    {
        Debug.Log("OnEnterBuff");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    static public void OnEnterSkip()
    {
        Debug.Log("OnEnterSkip");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    static public void OnEnterFriend()
    {
        Debug.Log("OnEnterFriend");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    
    static public void OnEnterTrade()
    {
        Debug.Log("OnEnterTrade");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    static public void OnEnterDelete()
    {
        Debug.Log("OnEnterDelete");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

}
