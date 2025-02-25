using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

// �������ܣ���ʱʹ�ã������ĳ���ļ̳к���Ϸ�ʽʵ�֡�
public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IBeginDragHandler ,IDragHandler , IEndDragHandler
{
    protected virtual bool _enlarge => true;    // �Ŵ�Ч��

    private Outline _outline;   // ����
    
    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    // �������¼�
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if(!_enlarge) { return; }
        Debug.Log("OnPointerEnter");
        transform.DOScale(0.75f , 0.25f);
        _outline.enabled = true;
    }

    // ����ƿ��¼�
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!_enlarge) { return; }
        Debug.Log("OnPointerExit");
        transform.DOScale(0.5f, 0.25f);
        _outline.enabled = false;
    }

    Vector2 _position;
    // ��ʼ��ק��¼��ʼ����
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _position = transform.GetComponent<RectTransform>().anchoredPosition;
    }

    // �Ƶ����λ��
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 _pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,eventData.pressEventCamera,out _pos))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = _pos;
        }
    }

    // ��ԭ
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = _position;
    }
}
