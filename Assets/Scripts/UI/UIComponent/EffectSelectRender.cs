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

    public bool isCanSelect = true;

    Dictionary<int, Action> actions = new Dictionary<int, Action>();

    private void Awake()
    {
        btn = GetComponent<Button>();
        title = transform.Find("Title").GetComponent<Text>();
        actions.Add(111,OnEnterCombat);
        actions.Add(112,OnEnterRelics);
        actions.Add(113,OnEnterSkip);
        actions.Add(114,OnEnterBuff);
        actions.Add(115,OnEnterFriend);
        actions.Add(116,OnEnterTrade);
        actions.Add(117,OnEnterDelete);
    }

    private void Start()
    {
        btn.onClick.AddListener(OnButtonClick);
        CheckSelectable();
    }

    public void SetData(EffectClass data)
    {
        effectClass = data;
        title.text = effectClass.op_name;
    }

    public void OnButtonClick()
    {
        if (!isCanSelect) return;
        actions[effectClass.effect_ids]();
    }

    public void CheckSelectable()
    {
        if(effectClass.op_ids == 41 || effectClass.op_ids == 42)
        {
            isCanSelect = GameManager.Instance.isEnoughMoney(20);
        }
        if (!isCanSelect)
        {
            // 不可点击
        }
    }

    public void OnEnterCombat()
    {
        Debug.Log("OnEnterCombat");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

    public void OnEnterRelics()
    {
        Debug.Log("OnEnterRelics");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

    public void OnEnterBuff()
    {
        Debug.Log("OnEnterBuff");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    public void OnEnterSkip()
    {
        Debug.Log("OnEnterSkip");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    public void OnEnterFriend()
    {
        Debug.Log("OnEnterFriend");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    
    public void OnEnterTrade()
    {
        Debug.Log("OnEnterTrade");
        if(effectClass.op_ids == 41) 
        {
            //op_name = "买个苹果", effect_ids = 116, effect_types = "trade", effect = "花费20金币，恢复1/3的生命值" 
            int maxHp = GameManager.Instance.GetPlayerMaxHP();
            GameManager.Instance.OnPlayerGetHp(maxHp / 3);
        }
        else if(effectClass.op_ids == 42) 
        {
            // op_name = "买点装备", effect_ids = 116, effect_types = "trade", effect = "花费20金币，获得两个随机卡"
            
        }
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }
    public void OnEnterDelete()
    {
        Debug.Log("OnEnterDelete");
        EventCenter.Broadcast(EventDefine.OnMergePanelShow);
    }

}
