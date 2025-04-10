using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureShow : MonoBehaviour
{
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
        name = cfg.Name;
        icon.sprite = ResourceUtil.GetTreasureByName(cfg.Icon);
        title.text = name;
        description.text = cfg.Description;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
