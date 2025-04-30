using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler
{
    private int Index;

    private CardClass config;

    public bool isShowOnly = false;

    public void SetShowOnly(bool _isShowOnly)
    {
        isShowOnly = _isShowOnly;
    }

    public void SetIndex(int idx)
    {
        Index = idx;
    }

    public void SetData(int id)
    {
        config = CardConfig.GetCardClassByKey(id);
    }

    public void SetData(int idx , int id)
    {
        SetIndex(idx);
        SetData(id);
    }

    public void SetData(int idx , CardClass cfg)
    {
        SetIndex(idx);
        SetData(cfg.id);
    }

    public void Hide() {gameObject.SetActive(false);}
    public void Show() {gameObject.SetActive(true);}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isShowOnly) return ;
        EventCenter.Broadcast(EventDefine.ON_CARD_SELECT , Index , config.id);
        Hide();
    }

}