using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnBtnHover : HoverBehaviour
{
    public Image Shadow;    // 阴影
    public GameObject Info; // 主体部分
    public Image BG;
    public Text a;      // 文本
    public Text b;      // 数字
    public bool isLock;

    public Sprite cantUse;
    public Sprite canUse;
    public Sprite pressed;

    public Color aNormalColor = Color.white;                    // 正常白色
    public Color bNormalColor = GameString.NUNUMBERCOLOR;       // 正常使用颜色
    public Color aClickColor = GameString.ONCLICKTEXTCOLOR;     // 文本
    public Color bClickColor = GameString.ONCLICKNUMBERCOLOR;   // 数字

    private void Start()
    {
        Shadow.gameObject.SetActive(false);
    }

    // val 的范围 [ 0 , 1f]
    public void OnChange(float val)
    {
        if (val == 1f)
        {
            // 可以点击
            isLock = false;
            BG.GetComponent<Image>().sprite = canUse;
        }
        else
        {
            isLock = true;
            BG.GetComponent<Image>().sprite = cantUse;
        }
        Color newColor = BG.color;
        newColor.a = val;
        BG.color = newColor;    // 背景的透明度
        a.color = newColor;     // 白色字体直接沿用背景的
        Color textColor = b.color;
        textColor.a = val;
        b.color = textColor;    // 法力值的透明度调整
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        //if (isLock) return;
        base.OnPointerEnter(eventData);
        Shadow.gameObject.SetActive(true);
        // .SetLink(gameObject) 绑定动画和物体的生命周期
        Info.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 7), 0.5f).SetEase(Ease.OutQuad).SetLink(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        //if (isLock) return;
        base.OnPointerExit(eventData);
        Shadow.gameObject.SetActive(false);
        Info.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.OutQuad).SetLink(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isLock) return;
        BG.GetComponent<Image>().sprite = pressed;
        base.OnPointerDown(eventData);
        a.color = aClickColor;
        b.color = bClickColor;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (isLock) return;
        BG.GetComponent<Image>().sprite = canUse;
        base.OnPointerDown(eventData);
        a.color = aNormalColor;
        b.color = bNormalColor;
    }

}
