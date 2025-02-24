using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BaseCard : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    public virtual void Start()
    {

    }

    // 鼠标进入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        transform.DOScale(0.75f , 0.25f);
    }

    // 鼠标移开事件
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        transform.DOScale(0.5f, 0.25f);
    }

    
}
