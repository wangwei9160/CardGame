using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : UIViewBase
{ 
    public override UIViewType Type => UIViewType.Multiple;
    public override UILAYER Layer => UILAYER.M_INFO_LAYER;

    public Slider slider;
    public GameObject Owner;

    public override void Init(string uiName,GameObject obj)
    {
        Init(uiName);
        Owner = obj;
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<int,int,int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
    }

    protected override void Start()
    {
        base.Start();
        if (Owner != null)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("HP").transform.position);
            BaseCharacter character = Owner.GetComponent<BaseCharacter>();
            slider.value = 1.0f * character.hp / character.maxHp;
        }
    }

    private void OnHpChange(int hp , int maxHp , int id)
    {
        if(id == Index)
        {
            slider.value = 1.0f * hp / maxHp;
        }
    }

    public override void AdjustPosition()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("HP").transform.position);
    }
}
