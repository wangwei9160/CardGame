using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EchoEventRender : MonoBehaviour
{
    public bool isChose;
    public bool canClick;
    public EchoEvent.EchoEventType echoEventType;
    public Text  text;
    public int Index;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>(); // 需要提前获取
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

    public void SetData(EchoEvent.EchoEventType id , int pos)
    {
        Debug.Log(EchoEvent.EchoEventManager.GetEchoEventConfigByType(id).name + " " + pos.ToString());
        Index = pos;
        if (id == EchoEvent.EchoEventType.UnKnow || pos == -1)
        {
            canClick = false;
            DisableRaycastTarget();
        }
        echoEventType = id;
        //Debug.Log(echoEventType + " " + EchoEvent.EchoEventManager.GetEchoEventConfigByType(echoEventType).name);
        text.text = EchoEvent.EchoEventManager.GetEchoEventConfigByType(echoEventType).name;
    }

    // 屏蔽点击
    public void DisableRaycastTarget()
    {
        GetComponent<Image>().raycastTarget = false;
    }
}
