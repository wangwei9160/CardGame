using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.Mathematics;
using System.Collections; // 使用DOTween实现平滑动画

public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler , IPointerEnterHandler , IPointerExitHandler
{
    private int Index;
    private RectTransform rectTransform;
    private Canvas rootCanvas;
    private Vector2 offset;
    private Transform originalParent;
    private Vector3 originalScale;

    private Vector3 originalPosition;

    [Header("悬停效果设置")]
    public float hoverYOffset = 250f; // 位置上移量

    [Header("拖拽时放大至dragScale比例")]
    public Vector3 dragScale = new Vector3(0.8f,0.8f,0.8f);

    [Header("选中时放大至selectScale比例")]
    public Vector3 selectScale = new Vector3(1.5f,1.5f,1.5f);

    public float animationDuration = 0.3f; // 动画持续时间

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>();
        originalParent = transform.parent;
        originalScale = transform.localScale;
    }

    public void SetIndex(int idx)
    {
        Index = idx;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DOTween.Kill(this);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out offset);
        
        transform.SetParent(rootCanvas.transform);
        EventCenter.Broadcast(EventDefine.AdjustCardPosition);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = dragScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        DOTween.Kill(this);
        Vector2 localPointerPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)rootCanvas.transform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out localPointerPos))
        {
            rectTransform.anchoredPosition = localPointerPos - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReOrigin();
        EventCenter.Broadcast(EventDefine.AdjustCardPosition);
    }

    # region 鼠标选中

    private bool isProcessingHover = false;
    private bool isMouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isProcessingHover) return;
        
        isMouseOver = true;
        EventCenter.Broadcast(EventDefine.ON_CARD_SELECT,Index);
        StartCoroutine(HandleHoverEffect());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        
        if (!isProcessingHover)
        {
            StartCoroutine(HandleExitEffect());
        }
    }

    private IEnumerator HandleHoverEffect()
    {
        isProcessingHover = true;
        
        // 保存原始状态
        originalPosition = rectTransform.anchoredPosition;
        
        // 临时提升到Canvas层级
        transform.SetParent(rootCanvas.transform);
        
        // 立即重置旋转但不触发退出
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        
        // 等待一帧确保旋转变化不会触发Exit事件
        yield return null;
        
        // 只有鼠标仍在对象上才继续动画
        if (isMouseOver)
        {
            EventCenter.Broadcast(EventDefine.ON_CARD_SELECT,Index);
            Vector2 targetPosition = rectTransform.anchoredPosition + new Vector2(0, hoverYOffset);
            
            DOTween.Sequence()
                .Join(rectTransform.DOAnchorPos(targetPosition, animationDuration).SetEase(Ease.OutQuad))
                .Join(transform.DOScale(selectScale, animationDuration).SetEase(Ease.OutBack))
                .SetId(this);
            
            transform.SetAsLastSibling();
        }
        else
        {
            // 如果鼠标已经离开，立即恢复
            ReOrigin();
        }
        
        isProcessingHover = false;
    }

    private IEnumerator HandleExitEffect()
    {
        // 确保没有并发的进入处理
        yield return new WaitWhile(() => isProcessingHover);
        
        DOTween.Kill(this);
        ReOrigin();
        
    }
    #endregion

    public void ReOrigin()
    {
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(Index);
        transform.localScale = originalScale;
        EventCenter.Broadcast(EventDefine.ON_CARD_UNSELECT,Index);
    }
}
