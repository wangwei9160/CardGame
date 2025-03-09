using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
{

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointUp");
    }
}
