using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SmallCardRender : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    public int Pos;
    public bool canDrag;
    public Vector3 startPosition;

    public Sprite sprite;

    private void Awake()
    {
        canDrag = true;
    }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canDrag) return;
        GetComponent<Image>().color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canDrag) return;
        GetComponent<Image>().color = Color.white;
    }

    public void OnDragMove(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // CardInfo --缺失

    // 设置索引，设置不可拖拽
    public void SetPosition(int pIndex)
    {
        Pos = pIndex;
        canDrag = false;
    }

    public void RefreshCardRender()
    {
        GetComponent<Image>().sprite = null;
    }

    
}
