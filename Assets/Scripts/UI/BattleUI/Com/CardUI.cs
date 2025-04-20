using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler
{
    private int Index;
    private Text cardName;
    private CardClass config;

    private void Awake()
    {
        cardName = transform.Find("name").GetComponent<Text>();
    }

    public void SetIndex(int idx)
    {
        Index = idx;
    }

    public void SetData(int idx , int id)
    {
        SetIndex(idx);
        config = CardConfig.GetCardClassByKey(id);
        cardName.text = config.name;
    }

    public void Hide() {gameObject.SetActive(false);}
    public void Show() {gameObject.SetActive(true);}

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventCenter.Broadcast(EventDefine.ON_CARD_SELECT , Index , config.id);
        Hide();
    }

}