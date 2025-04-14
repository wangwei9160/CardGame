using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureShow : MonoBehaviour 
{
    public int ID;
    public Image icon;
    public Text title;
    public Text description;
    public Action action;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<Image>();
        title = transform.Find("name").GetComponent<Text>();
        description = transform.Find("description").GetComponent<Text>();
    }

    public void SetData(TreasureClass cfg)
    {
        gameObject.SetActive(true);
        ID = cfg.ID;
        name = ID + "-" + cfg.Name;
        icon.sprite = ResourceUtil.GetTreasureByName(cfg.Icon);
        title.text = cfg.Name;
        description.text = cfg.Description;
        TextUtil.ProcessTextWrap(description , 250f , 5);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
