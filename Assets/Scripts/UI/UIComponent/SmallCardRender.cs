using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SmallCardRender : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    public int Pos;                 // 当前位置
    public bool isLock;             // 强制上锁防止悬停修改
    public bool canDrag;            // 是否可以拖拽

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

    #region 卡牌拖拽悬停效果
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
        GetComponent<Image>().color = Color.black;                      // 当前用于提示更新这个卡
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_HOVER , Pos);    // 广播当前卡牌悬停
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canDrag || isLock) return;
        GetComponent<Image>().color = oldColor;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_HOVER, -1);    // 离开后重置悬停的卡牌
    }

    public void OnDragMove(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    #endregion
    // CardInfo --缺失

    // 设置索引，设置不可拖拽
    public void SetPosition(int pIndex)
    {
        Pos = pIndex;
        canDrag = false;
    }
    // 设置卡牌数据
    public void SetData()
    {
        Debug.Log("更新当前卡牌数据");
        isLock = true;          // 更新卡牌数据后强制上锁防止此时悬停bug
        GetComponent<Image>().color = Color.blue;
        StartCoroutine(WaitForTimeUnLock());
    }
    // 重置卡牌数据
    public void RefreshCardRender()
    {
        GetComponent<Image>().sprite = null;
    }

    // 定时器解锁
    IEnumerator WaitForTimeUnLock()
    {
        yield return new WaitForSeconds(0.5f);
        isLock = false;
    }
    
}
