using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnBtnHover : HoverBehaviour
{
    public Image Shadow;    // ��Ӱ
    public GameObject Info; // ���岿��
    public Image BG;
    public Text a;      // �ı�
    public Text b;      // ����
    public bool isLock;

    public Sprite cantUse;
    public Sprite canUse;
    public Sprite pressed;

    public Color aNormalColor = Color.white;                    // ������ɫ
    public Color bNormalColor = GameString.NUNUMBERCOLOR;       // ����ʹ����ɫ
    public Color aClickColor = GameString.ONCLICKTEXTCOLOR;     // �ı�
    public Color bClickColor = GameString.ONCLICKNUMBERCOLOR;   // ����

    private void Start()
    {
        Shadow.gameObject.SetActive(false);
    }

    // val �ķ�Χ [ 0 , 1f]
    public void OnChange(float val)
    {
        if (val == 1f)
        {
            // ���Ե��
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
        BG.color = newColor;    // ������͸����
        a.color = newColor;     // ��ɫ����ֱ�����ñ�����
        Color textColor = b.color;
        textColor.a = val;
        b.color = textColor;    // ����ֵ��͸���ȵ���
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        //if (isLock) return;
        base.OnPointerEnter(eventData);
        Shadow.gameObject.SetActive(true);
        // .SetLink(gameObject) �󶨶������������������
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
