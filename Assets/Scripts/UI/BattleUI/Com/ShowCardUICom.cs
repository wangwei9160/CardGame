using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCardUICom : MonoBehaviour , IPointerExitHandler , IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public int Index;
    public CardUI showCard;
    public CardShow cardShow;
    public int cardID;

    public bool isFollower;

    private void Awake()
    {
        Index = -1;
        isFollower = false;
        showCard = transform.Find("Card").GetComponent<CardUI>();
        cardShow = transform.Find("Card").GetComponent<CardShow>();
        showCard.gameObject.SetActive(true);
        showCard.SetShowOnly(true);
        EventCenter.AddListener(EventDefine.ON_FOLLOWER_SKILL_SELECT_FINISH , ON_FOLLOWER_SKILL_SELECT_FINISH);
    }

    public void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ON_FOLLOWER_SKILL_SELECT_FINISH , ON_FOLLOWER_SKILL_SELECT_FINISH);
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
        isFollower = (CARD_TYPE)cfg.type == CARD_TYPE.FOLLOWER;
        showCard.SetData(cfg.id);
        cardShow.SetData(cfg.id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isDrag) return ;
        int lastIndex = Index;
        Index = -1;
        if(lastIndex != -1) EventCenter.Broadcast<int>(EventDefine.ON_CARD_UNSELECT , lastIndex);
        StartCoroutine(WaitTimeForHide());
    }

    IEnumerator WaitTimeForHide()
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
        // 流程需要重新调整，临时处理不通用
        if(isFollower) 
        {
            if(eventData.position.y >= 500f)
            {
                showCard.gameObject.SetActive(false);
                EventCenter.Broadcast(EventDefine.ON_FOLLOWER_DRAG , eventData.position);
            }else 
            {
                showCard.gameObject.SetActive(true);
                EventCenter.Broadcast(EventDefine.ON_FOLLOWER_DRAG_END);
            }
        }else {
            if(eventData.position.y >= 500f)
            {
                if(SkillManager.Instance.CheckNeedHideCard(cardID))
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_STOP);
        if(isFollower) 
        {
            if(eventData.position.y >= 500f) 
            {
                EventCenter.Broadcast(EventDefine.ON_FOLLOWER_SET);
                EventCenter.Broadcast(EventDefine.OnDeleteCardByIndex , Index);
                if(SkillManager.Instance.CheckNeedHideCard(cardID))
                {
                    StartCoroutine(WaitForCondition(cardID));
                }else 
                {
                    SkillManager.Instance.ExecuteEffect(cardID);
                    SkillManager.Instance.PreExecuteSelecteClose(cardID);
                }
                Index = -1;
            }else {
                showCard.gameObject.SetActive(false);
                EventCenter.Broadcast(EventDefine.ON_FOLLOWER_DRAG_END);
                EventCenter.Broadcast(EventDefine.ON_CARD_UNSELECT , Index);
                SkillManager.Instance.PreExecuteSelecteClose(cardID);
            }
            
        }else 
        {
            if(eventData.position.y >= 500f) 
            {
                if(SkillManager.Instance.CheckTypeAndSelect(cardID)){
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
        }
        Hide();
    }

    private bool _isFollowerSelectConditionMet = false;

    public void ON_FOLLOWER_SKILL_SELECT_FINISH()
    {
        _isFollowerSelectConditionMet = true;
    }

    IEnumerator WaitForCondition(int cardID)
    {
        SkillManager.Instance.PreExecuteSelecte(cardID , true);
        // 等待直到条件满足
        yield return new WaitUntil(() => _isFollowerSelectConditionMet);
        SkillManager.Instance.PreExecuteSelecteClose(cardID);
        _isFollowerSelectConditionMet = false;
        SkillManager.Instance.ExecuteEffect(cardID);
    }

    #endregion Drag operate
}