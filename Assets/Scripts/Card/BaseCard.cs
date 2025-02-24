using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BaseCard : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    public virtual void Start()
    {

    }

    // �������¼�
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        transform.DOScale(0.75f , 0.25f);
    }

    // ����ƿ��¼�
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        transform.DOScale(0.5f, 0.25f);
    }

    
}
