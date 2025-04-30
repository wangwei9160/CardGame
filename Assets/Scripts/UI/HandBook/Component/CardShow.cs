using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardShow : MonoBehaviour
{
    public Image icon;
    public Image r_u;
    public Image r_d;
    public Image l_d;
    public Image r_d_cost;
    public Image l_d_cost;
    public Text title;
    public Text description;
    public Text Type;
    public Action action;
    public Image cost;
    public RectTransform r_uRectTransform;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<Image>();
        title = transform.Find("name").GetComponent<Text>();
        description = transform.Find("description").GetComponent<Text>();
        r_u = transform.Find("r_u").GetComponent<Image>();
        r_d = transform.Find("r_d").GetComponent<Image>();
        l_d = transform.Find("l_d").GetComponent<Image>();
        r_uRectTransform = r_u.GetComponent<RectTransform>();
        r_uRectTransform.sizeDelta = new Vector3(80f, 80f);
        Type = transform.Find("Type").GetComponent<Text>();
        cost = transform.Find("cost").GetComponent<Image>();
        r_d_cost = transform.Find("r_d_cost").GetComponent<Image>();
        l_d_cost = transform.Find("l_d_cost").GetComponent<Image>();
    }

    public void SetData(int card_id)
    {
        CardClass cfg = CardConfig.GetCardClassByKey(card_id);
        SetData(cfg);
    }

    public void SetData(CardClass cfg)
    {
        gameObject.SetActive(true);
        name = cfg.name;
        icon.sprite = ResourceUtil.GetCardByName(cfg.icon);
        title.text = name;
        description.text = SkillManager.Instance.GetSkillDescription(cfg);
        TextUtil.AdjustTextComBySelf(description , 9 , 9);
        Type.text = ResourceUtil.GetCardTypeName(cfg.type);
        cost.sprite = ResourceUtil.GetWhiteCostImage(cfg.cost);
        if(cfg.leftAttribute != cfg.rightAttribute) 
        {
            l_d.gameObject.SetActive(true);
            l_d_cost.gameObject.SetActive(true);
            l_d.sprite = ResourceUtil.GetCardAttributeTypeImage(cfg.leftAttribute);
            r_d.sprite = ResourceUtil.GetCardAttributeTypeImage(cfg.rightAttribute);
        }else {
            l_d.gameObject.SetActive(false);
            l_d_cost.gameObject.SetActive(false);
            r_d.sprite = ResourceUtil.GetCardAttributeTypeImage(cfg.rightAttribute);
        }
        l_d_cost.sprite = ResourceUtil.GetWhiteCostImage(cfg.attribute[cfg.leftAttribute - 1]);
        r_d_cost.sprite = ResourceUtil.GetWhiteCostImage(cfg.attribute[cfg.rightAttribute - 1]);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
