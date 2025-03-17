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

    void Start()
    {
        text = transform.Find("Text").GetComponent<Text>();
        isChose = false;
        GetComponent<Outline>().enabled = isChose;
        canClick = true;
        GetComponent<Button>().onClick.AddListener(() => {
            if (!canClick) return;
            isChose = !isChose; // 选中或者不选中
            SetClick(isChose);
            EventCenter.Broadcast(EventDefine.ON_ECHOEVENT_SELECT , isChose , Index, echoEventType);
        });
    }

    public void SetClick(bool isClick)
    {
        isChose = isClick;
        GetComponent<Outline>().enabled = isChose;
    }

    public void SetData(EchoEvent.EchoEventType id , int pos)
    {
        Index = pos;
        if (id == EchoEvent.EchoEventType.UnKnow)
        {
            canClick = false;
        }
        echoEventType = id;
        text.text = EchoEvent.EchoEventManager.GetEchoEventConfigByType(echoEventType).name;
    }
}
