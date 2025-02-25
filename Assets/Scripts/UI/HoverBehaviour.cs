using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
    }
}
