using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardShow : MonoBehaviour
{
    public Image icon;
    public Text title;
    public Text description;
    public Text Type;
    public Action action;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<Image>();
        title = transform.Find("name").GetComponent<Text>();
        description = transform.Find("description").GetComponent<Text>();
        Type = transform.Find("Type").GetComponent<Text>();
    }

    public void SetData(Test0Class cfg)
    {
        gameObject.SetActive(true);
        name = cfg.Name;
        icon.sprite = ResourceUtil.GetCardByName(cfg.Icon);
        title.text = name;
        description.text = cfg.Description;
        Type.text = cfg.Type;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
