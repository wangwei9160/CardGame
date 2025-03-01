using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : UIViewBase
{ 
    public override UIViewType Type => UIViewType.Multiple;

    public Slider slider;
    public GameObject Owner;

    public override void Init(GameObject obj)
    {
        Owner = obj;
    }

    public override void OnAddlistening()
    {
        EventCenter.AddListener<float , int>(EventDefine.OnHpChangeByName , OnHpChange);
    }

    public override void OnRemovelistening()
    {
        EventCenter.RemoveListener<float, int>(EventDefine.OnHpChangeByName, OnHpChange);
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

    private void OnHpChange(float val , int id)
    {
        if(id == Index)
        {
            slider.value = val;
        }
    }

}
