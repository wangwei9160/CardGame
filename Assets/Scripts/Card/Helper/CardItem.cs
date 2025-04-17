using UnityEngine;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour
{
    public bool dragable = true;
    public bool enableDrag => dragable;

    public bool enableSelect => throw new System.NotImplementedException();

    public bool enableFocus => true;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void StartDrag()
    {
        startPos = transform.position;
    }

    private void OnMouseDrag()
    {
        // 获取鼠标的世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // 保持原有的z值
        
        // 更新对象位置
        transform.position = mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;
    }
}
