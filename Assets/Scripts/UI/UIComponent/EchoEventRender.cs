using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EchoEventRender : MonoBehaviour
{
    public bool isChose;
    public bool canClick;
    public EchoEventType echoEventType;
    public Text text;
    public int Index;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>(); // 需要提前获取
        text.gameObject.SetActive(false);
        isChose = false;
        canClick = true;
    }

    void Start()
    {
        GetComponent<Outline>().enabled = isChose;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!canClick) return;
        isChose = !isChose; // 选中或者不选中
        SetClick(isChose);
        EventCenter.Broadcast(EventDefine.ON_ECHOEVENT_SELECT, isChose, Index, echoEventType);
    }

    public void SetClick(bool isClick)
    {
        isChose = isClick;
        GetComponent<Outline>().enabled = isChose;
    }

    public void SetData(EchoEventType id , int pos)
    {
        var type = EchoEventManager.GetEchoEventClassByKey((int)id);
        if (id != 0)
        {
            text.text = type.name;
            Index = pos;
            echoEventType = id;
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Arts/EchoEvent/" + type.icon);   // 加载图片
            Color c = GetComponent<Image>().color;
            c.a = 1;
            GetComponent<Image>().color = c;
        }
        else
        {
            text.text = "";
            echoEventType = id;
            canClick = false;
            DisableRaycastTarget();
            GetComponent<Image>().sprite = null;
            Color c = GetComponent<Image>().color;
            c.a = 0;
            GetComponent<Image>().color = c;
            return;
        }
    }

    // 屏蔽点击
    public void DisableRaycastTarget()
    {
        GetComponent<Image>().raycastTarget = false;
    }
}
