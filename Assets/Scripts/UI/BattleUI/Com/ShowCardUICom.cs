using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCardUICom : MonoBehaviour , IPointerExitHandler , IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public int Index;
    public CardUI showCard;
    public int cardID;

    private void Awake()
    {
        Index = -1;
        showCard = transform.Find("Card").GetComponent<CardUI>();
        showCard.gameObject.SetActive(true);
        showCard.SetShowOnly(true);
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }
    public void Show() 
    {
        gameObject.SetActive(true);
        showCard.gameObject.SetActive(true);
    }

    public void TryHide(int id) 
    {
        Debug.Log($"tryHide {id} , CurIndex {Index}");
        if(id == Index) Hide();
    }

    public bool IsSomethingSlect()
    {
        return Index == -1;
    }

    public void SetData(int idx , Vector3 pos , int id)
    {
        if(Index != idx && Index != -1) 
        {
            EventCenter.Broadcast<int>(EventDefine.ON_CARD_UNSELECT , Index);
        }
        Show();
        Index = idx;
        cardID = id;
        transform.position = pos;
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        showCard.SetData(cfg.id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isDrag) return ;
        int lastIndex = Index;
        Index = -1;
        if(lastIndex != -1) EventCenter.Broadcast<int>(EventDefine.ON_CARD_UNSELECT , lastIndex);
        StartCoroutine(waitTimeForHide());
    }

    IEnumerator waitTimeForHide()
    {
        // 等待这一帧结束,防止因为原物体的旋转导致卡牌的区域变化
        yield return new WaitForEndOfFrame();
        if(Index == -1) 
        {
            Hide();
            EventCenter.Broadcast(EventDefine.AdjustCardPosition);
        }
    }

    #region Drag operate
    public bool isDrag = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_START);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        float halfHeight = rectTransform.rect.height * 0.5f;
        
        rectTransform.anchoredPosition =  eventData.position + new Vector2(0, halfHeight);
        if(eventData.position.y >= 500f)
        {
            if(SkillManager.Instance.OpenSelector(cardID) != SkillSelectorType.NONE)
            {
                showCard.gameObject.SetActive(false);
                SkillManager.Instance.PreExecuteSelecte(cardID);
            }
        }else
        {
            showCard.gameObject.SetActive(true);
            SkillManager.Instance.PreExecuteSelecteClose(cardID);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_STOP);
        if(eventData.position.y >= 500f) 
        {
            if(SkillManager.Instance.checkTypeAndSelect(cardID)){
                EventCenter.Broadcast(EventDefine.OnDeleteCardByIndex , Index);
                Index = -1;
                SkillManager.Instance.ExecuteEffect(cardID);
            }else {
                EventCenter.Broadcast(EventDefine.ON_CARD_UNSELECT , Index);
            }
        }
        else {
            EventCenter.Broadcast(EventDefine.ON_CARD_UNSELECT , Index);
        }
        SkillManager.Instance.PreExecuteSelecteClose(cardID);
        Hide();
    }

    #endregion Drag operate
}