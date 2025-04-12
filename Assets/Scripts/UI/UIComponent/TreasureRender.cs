using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureRender : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public int ID;
    public int Index;
    public Image icon;

    private void Awake()
    {
        icon = transform.AddComponent<Image>();
    }

    public void SetData(int index , int id)
    {
        gameObject.SetActive(true);
        Index = index; 
        ID = id;
        TreasureClass tcls = TreasureManager.GetTreasureClassByKey(ID);
        icon.sprite = ResourceUtil.GetTreasureByID(id);
        transform.name = tcls.Name;
    }
        
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventCenter.Broadcast(EventDefine.TREASURE_TIP_HIDE);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventCenter.Broadcast(EventDefine.TREASURE_TIP_SHOW, transform.position , ID);
    }
}
