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
    public Text passive;
    public Text active;
    public Text Type;
    public Action action;
    public Image cost;
    public RectTransform r_uRectTransform;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<Image>();
        title = transform.Find("name").GetComponent<Text>();
        active = transform.Find("active").GetComponent<Text>();
        passive = transform.Find("passive").GetComponent<Text>();
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

    public void SetData(CardClass cfg)
    {
        gameObject.SetActive(true);
        name = cfg.name;
        icon.sprite = ResourceUtil.GetCardByName(cfg.icon);
        title.text = name;

        int temp_attribute;
        if(cfg.leftAttribute > cfg.rightAttribute){
            temp_attribute = cfg.leftAttribute;
            cfg.leftAttribute = cfg.rightAttribute;
            cfg.rightAttribute = temp_attribute;
        }

        switch (cfg.type)
        {
            case 1:
                Type.text = "武器";
                break;
            case 2:
                Type.text = "饰品";
                break;
            case 3:
                Type.text = "随从";
                break;
            case 4:
                Type.text = "法术";
                break;      
        }

        // 根据 cost 值设置不同的图片
        string costIconPath = "";
        switch (cfg.cost)
        {
            case 0:
                costIconPath = "UI/HandBook/CostW/cost0";
                break;
            case 1:
                costIconPath = "UI/HandBook/CostW/cost1";
                break;
            case 2:
                costIconPath = "UI/HandBook/CostW/cost2";
                break;
            case 3:
                costIconPath = "UI/HandBook/CostW/cost3";
                break;
            case 4:
                costIconPath = "UI/HandBook/CostW/cost4";
                break;        
            case 5:
                costIconPath = "UI/HandBook/CostW/cost5";
                break;
            case 6:
                costIconPath = "UI/HandBook/CostW/cost6";
                break;
            case 7:
                costIconPath = "UI/HandBook/CostW/cost7";
                break;
            case 8:
                costIconPath = "UI/HandBook/CostW/cost8";
                break;
            case 9:
                costIconPath = "UI/HandBook/CostW/cost9";
                break;
        }

        string r_d_costIconPath = "";
        if(cfg.leftAttribute == 1){
            switch (cfg.blade)
            {
                case 0:
                    r_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    r_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    r_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    r_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    r_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    r_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    r_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    r_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    r_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    r_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }            
        } else if (cfg.leftAttribute == 2 ){
            switch (cfg.magic)
            {
                case 0:
                    r_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    r_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    r_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    r_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    r_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    r_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    r_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    r_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    r_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    r_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        } else if (cfg.leftAttribute == 3 ){
            switch (cfg.defence)
            {
                case 0:
                    r_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    r_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    r_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    r_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    r_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    r_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    r_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    r_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    r_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    r_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        } else if (cfg.leftAttribute == 4 ){
            switch (cfg.glass)
            {
                case 0:
                    r_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    r_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    r_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    r_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    r_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    r_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    r_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    r_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    r_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    r_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        } else if (cfg.leftAttribute == 5 ){
            switch (cfg.vatality)
            {
                case 0:
                    r_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    r_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    r_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    r_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    r_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    r_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    r_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    r_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    r_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    r_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        }

        string l_d_costIconPath = "";
        if(cfg.rightAttribute == 1){
            switch (cfg.blade)
            {
                case 0:
                    l_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    l_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    l_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    l_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    l_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    l_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    l_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    l_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    l_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    l_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }            
        } else if (cfg.rightAttribute == 2 ){
            switch (cfg.magic)
            {
                case 0:
                    l_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    l_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    l_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    l_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    l_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    l_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    l_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    l_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    l_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    l_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        } else if (cfg.rightAttribute == 3 ){
            switch (cfg.defence)
            {
                case 0:
                    l_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    l_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    l_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    l_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    l_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    l_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    l_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    l_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    l_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    l_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        } else if (cfg.rightAttribute == 4 ){
            switch (cfg.glass)
            {
                case 0:
                    l_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    l_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    l_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    l_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    l_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    l_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    l_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    l_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    l_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    l_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        } else if (cfg.rightAttribute == 5 ){
            switch (cfg.vatality)
            {
                case 0:
                    l_d_costIconPath = "UI/HandBook/CostW/cost0";
                    break;
                case 1:
                    l_d_costIconPath = "UI/HandBook/CostW/cost1";
                    break;
                case 2:
                    l_d_costIconPath = "UI/HandBook/CostW/cost2";
                    break;
                case 3:
                    l_d_costIconPath = "UI/HandBook/CostW/cost3";
                    break;
                case 4:
                    l_d_costIconPath = "UI/HandBook/CostW/cost4";
                    break;        
                case 5:
                    l_d_costIconPath = "UI/HandBook/CostW/cost5";
                    break;
                case 6:
                    l_d_costIconPath = "UI/HandBook/CostW/cost6";
                    break;
                case 7:
                    l_d_costIconPath = "UI/HandBook/CostW/cost7";
                    break;
                case 8:
                    l_d_costIconPath = "UI/HandBook/CostW/cost8";
                    break;
                case 9:
                    l_d_costIconPath = "UI/HandBook/CostW/cost9";
                    break;
            }             
        }

        string leftAttributePath = "";
        switch (cfg.leftAttribute)
        {
            case 1:
                leftAttributePath = "UI/HandBook/Attribute/blade";
                break;
            case 2:
                leftAttributePath = "UI/HandBook/Attribute/magic";
                break;
            case 3:
                leftAttributePath = "UI/HandBook/Attribute/defence";
                break;
            case 4:
                leftAttributePath = "UI/HandBook/Attribute/glass";
                break;   
            case 5:
                leftAttributePath = "UI/HandBook/Attribute/vatality";
                break;      
        }

        string rightAttributePath = "";
        switch (cfg.rightAttribute)
        {
            case 1:
                rightAttributePath = "UI/HandBook/Attribute/blade";
                break;
            case 2:
                rightAttributePath = "UI/HandBook/Attribute/magic";
                break;
            case 3:
                rightAttributePath = "UI/HandBook/Attribute/defence";
                break;
            case 4:
                rightAttributePath = "UI/HandBook/Attribute/glass";
                break;   
            case 5:
                rightAttributePath = "UI/HandBook/Attribute/vatality";
                break;      
        }

        Sprite costSprite = Resources.Load<Sprite>(costIconPath);
        if (costSprite != null)
        {
            cost.sprite = costSprite;
        }
        else
        {
            Debug.LogWarning($"找不到cost图片: {costIconPath}");
        }

        Sprite leftAttributeSprite = Resources.Load<Sprite>(leftAttributePath);
        if (leftAttributeSprite != null)
        {
            l_d.sprite = leftAttributeSprite;
        }
        else
        {
            Debug.LogWarning($"找不到cost图片: {leftAttributePath}");
        }

        Sprite rightAttributeSprite = Resources.Load<Sprite>(rightAttributePath);
        if (rightAttributeSprite != null)
        {
            r_d.sprite = rightAttributeSprite;
        }
        else
        {
            Debug.LogWarning($"找不到cost图片: {rightAttributePath}");
        }    

        Sprite r_d_costIconPathSprite = Resources.Load<Sprite>(r_d_costIconPath);
        if (r_d_costIconPathSprite != null)
        {
            r_d_cost.sprite = r_d_costIconPathSprite;
        }
        else
        {
            Debug.LogWarning($"找不到cost图片: {r_d_costIconPath}");
        }  

        Sprite l_d_costIconPathSprite = Resources.Load<Sprite>(l_d_costIconPath);
        if (l_d_costIconPathSprite != null)
        {
            l_d_cost.sprite = l_d_costIconPathSprite;
        }
        else
        {
            Debug.LogWarning($"找不到cost图片: {l_d_costIconPath}");
        }   
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
