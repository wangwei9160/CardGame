using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

// 交互功能，临时使用，后续改成类的继承和组合方式实现。
public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IBeginDragHandler ,IDragHandler , IEndDragHandler
{
    protected virtual bool _enlarge => true;    // 放大效果

    private Outline _outline;   // 轮廓
    
    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    // 鼠标进入事件
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if(!_enlarge) { return; }
        Debug.Log("OnPointerEnter");
        transform.DOScale(0.75f , 0.25f);
        _outline.enabled = true;
    }

    // 鼠标移开事件
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!_enlarge) { return; }
        Debug.Log("OnPointerExit");
        transform.DOScale(0.5f, 0.25f);
        _outline.enabled = false;
    }

    Vector2 _position;
    // 开始拖拽记录初始坐标
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _position = transform.GetComponent<RectTransform>().anchoredPosition;
    }

    // 移到鼠标位置
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

    // 还原
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = _position;
    }
}
