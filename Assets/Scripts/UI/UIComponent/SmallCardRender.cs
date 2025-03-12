using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SmallCardRender : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    public int Pos;                 // ��ǰλ��
    public bool isLock;             // ǿ��������ֹ��ͣ�޸�
    public bool canDrag;            // �Ƿ������ק

    public Sprite sprite;

    private void Awake()
    {
        canDrag = true;
        isLock = false;
    }

    private void Start()
    {
        EventCenter.AddListener(EventDefine.ON_CARD_DRAG_START, OnCardDragStart);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ON_CARD_DRAG_START, OnCardDragStart);
    }

    public void OnCardDragStart()
    {
        isLock = false;
    }

    #region ������ק��ͣЧ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        sprite = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().raycastTarget = false;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_START);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG , eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        GetComponent<Image>().sprite = sprite;
        GetComponent<Image>().raycastTarget = true;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_STOP);
    }

    public Color oldColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canDrag || isLock) return;
        oldColor = GetComponent<Image>().color;
        GetComponent<Image>().color = Color.black;                      // ��ǰ������ʾ���������
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_HOVER , Pos);    // �㲥��ǰ������ͣ
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canDrag || isLock) return;
        GetComponent<Image>().color = oldColor;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_HOVER, -1);    // �뿪��������ͣ�Ŀ���
    }

    public void OnDragMove(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    #endregion
    // CardInfo --ȱʧ

    // �������������ò�����ק
    public void SetPosition(int pIndex)
    {
        Pos = pIndex;
        canDrag = false;
    }
    // ���ÿ�������
    public void SetData()
    {
        Debug.Log("���µ�ǰ��������");
        isLock = true;          // ���¿������ݺ�ǿ��������ֹ��ʱ��ͣbug
        GetComponent<Image>().color = Color.blue;
        StartCoroutine(WaitForTimeUnLock());
    }
    // ���ÿ�������
    public void RefreshCardRender()
    {
        GetComponent<Image>().sprite = null;
    }

    // ��ʱ������
    IEnumerator WaitForTimeUnLock()
    {
        yield return new WaitForSeconds(0.5f);
        isLock = false;
    }
    
}
