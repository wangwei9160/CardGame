using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCardUICom : MonoBehaviour , IPointerExitHandler , IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public int Index;
    public Text cardName;
    public int ID;

    private void Awake()
    {
        Index = -1;
        cardName = transform.Find("name").GetComponent<Text>();
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }
    public void Show() 
    {
        gameObject.SetActive(true);
        // transform.localScale = new Vector3(1.2f,1.2f,1.2f);
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
        if(Index != idx) 
        {
            EventCenter.Broadcast<int>(EventDefine.ON_CARD_UNSELECT , Index);
        }
        Show();
        Index = idx;
        ID = id;
        transform.position = pos;
        CardClass cfg = CardConfig.GetCardClassByKey(id);
        cardName.text = cfg.name;
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
            rectTransform.anchoredPosition = eventData.position + new Vector2(1920f, 1080f);
            SkillManager.Instance.PreExecuteSelecte((SkillSelectorType)1);
        }else
        {
            SkillManager.Instance.PreExecuteSelecteClose((SkillSelectorType)1);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_STOP);
        if(eventData.position.y >= 500f) 
        {
            EventCenter.Broadcast(EventDefine.OnDeleteCardByIndex , Index);
            int rd = RandomUtil.RandomInt(0,1 + 1);
            if(rd == 0){
                SkillManager.Instance.ExecuteEffect(SkillType.DAMAGE , "");
            }else {
                SkillManager.Instance.ExecuteEffect(SkillType.HEAL , "");
            }
        }
        else {
            EventCenter.Broadcast(EventDefine.ON_CARD_UNSELECT , Index);
        }
        SkillManager.Instance.PreExecuteSelecteClose((SkillSelectorType)1);
        Hide();
    }

    #endregion Drag operate
}