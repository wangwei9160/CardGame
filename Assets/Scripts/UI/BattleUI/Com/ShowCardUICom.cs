using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCardUICom : MonoBehaviour , IPointerExitHandler , IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public int Index;
    public Text cardName;

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

    public void SetData(int idx , Vector3 pos)
    {
        if(Index != idx) 
        {
            EventCenter.Broadcast<int>(EventDefine.ON_CARD_UNSELECT , Index);
            // 设置时是按第一帧计算的
            // SetPositionToMouse(); // 强制落到鼠标中心位置，防止由于原物体的旋转导致的exit事件无法触发
        }
        Show();
        Debug.Log($"tryShow {idx} , CurIndex {Index}");
        Index = idx;
        transform.position = pos;
        cardName.text = $"卡牌-{idx}";
    }

    public void SetPositionToMouse()
    {
        Debug.Log(Input.mousePosition);
        GetComponent<RectTransform>().position = Input.mousePosition;
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
        // 等待这一帧结束，防止因为原物体的旋转导致卡牌的区域变化
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        EventCenter.Broadcast(EventDefine.ON_CARD_DRAG_STOP);
        if(eventData.position.y >= 700f) 
        {
            EventCenter.Broadcast(EventDefine.OnDeleteCardByIndex , Index);
        }
        else {
            EventCenter.Broadcast(EventDefine.ON_CARD_UNSELECT , Index);
        }
        Hide();
    }

    #endregion Drag operate
}