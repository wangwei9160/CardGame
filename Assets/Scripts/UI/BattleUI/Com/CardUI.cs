using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventCenter.Broadcast(EventDefine.ON_CARD_SELECT , Index);
        Hide();
    }

}