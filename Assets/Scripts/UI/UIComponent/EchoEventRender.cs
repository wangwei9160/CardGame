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
        text = transform.Find("Text").GetComponent<Text>(); // ��Ҫ��ǰ��ȡ
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
        isChose = !isChose; // ѡ�л��߲�ѡ��
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
        if (type != null)
        {
            text.text = type.name;
            Index = pos;
            echoEventType = id;
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Arts/EchoEvent/" + type.icon);   // ����ͼƬ
        }
        else
        {
            text.text = "";
            echoEventType = id;
            canClick = false;
            DisableRaycastTarget();
            GetComponent<Image>().sprite = null;
            return;
        }
    }

    // ���ε��
    public void DisableRaycastTarget()
    {
        GetComponent<Image>().raycastTarget = false;
    }
}
