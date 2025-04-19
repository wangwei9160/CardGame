using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler , IPointerEnterHandler
{
    private int Index;
    private Text cardName;

    private void Awake()
    {
        cardName = transform.Find("name").GetComponent<Text>();
    }

    public void SetData(int idx)
    {
        Index = idx;
        cardName.text = $"卡牌-{idx}";
    }

    public void Hide() {gameObject.SetActive(false);}
    public void Show() {gameObject.SetActive(true);}

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventCenter.Broadcast(EventDefine.ON_CARD_SELECT , Index);
        Hide();
    }

}