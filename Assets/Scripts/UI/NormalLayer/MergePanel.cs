using EchoEvent;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MergePanel : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;
    public override UILAYER Layer => UILAYER.M_NORMAL_LAYER;

    public Button GoBtn;

    public GameObject choseRoad;    // 选择的道路
    public GameObject mergeEvent;   // 合成选择路径

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<bool , int , EchoEventType>(EventDefine.ON_ECHOEVENT_SELECT, OnEchoEvenSelect);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener<bool, int, EchoEventType>(EventDefine.ON_ECHOEVENT_SELECT, OnEchoEvenSelect);
    }

    protected override void Start()
    {
        base.Start();
        // 进入当前页面默认打开合成界面
        choseRoad = transform.Find("Canvas/ChoseRoad").gameObject;
        choseRoad.SetActive(false);
        mergeEvent = transform.Find("Canvas/MergeEvent").gameObject;
        mergeEvent.SetActive(true);
        GoBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close(this.Name);    // 关闭这个界面即可
            EventCenter.Broadcast(EventDefine.OnBattleStart);
            // 根据合成结果选择不同分支，具体分支由GameManager操控
        });
        MergeEventInit();
    }

    #region MergeEvent
    public EchoEventRender[] Events;
    public Transform EventsTransform;

    public EchoEventRender echoEvent;

    private EchoEventType echoEventType;
    private Text echoEventMessage;

    public Button confirmBtn;

    public void MergeEventInit()
    {
        EventsTransform = mergeEvent.transform.Find("Events");
        Events = new EchoEventRender[EventsTransform.childCount];
        for (int i = 0; i < EventsTransform.childCount; i++)
        {
            Events[i] = EventsTransform.GetChild(i).GetComponent<EchoEventRender>();
            EchoEvent.EchoEventType typeId = RandomUtil.GetRandomValueInList(EchoEvent.EchoEventManager.eventBaseTypeList, i == 0 ? 2 : 4); // 限制前2个
            Events[i].SetData(typeId , i);
        }
        echoEventType = EchoEventType.UnKnow;
        echoEvent = mergeEvent.transform.Find("EchoEvent/Event").GetComponent<EchoEventRender>();
        echoEventMessage = mergeEvent.transform.Find("EchoEvent/Message/Text").GetComponent<Text>();
        SetEchoEvent();

        confirmBtn = mergeEvent.transform.Find("ConfirmBtn").GetComponent<Button>();
        confirmBtn.onClick.AddListener(() =>
        {
            if(echoEventType != EchoEventType.UnKnow)
            {
                mergeEvent.SetActive(false);
                choseRoad.SetActive(true);
            }
        });
    }

    private void SetEchoEvent()
    {
        echoEvent.SetData(echoEventType , 0);
        echoEventMessage.text = EchoEventManager.GetEchoEventConfigByType(echoEventType).message;
    }

    private int _echoCnt = 0;
    private void OnEchoEvenSelect(bool isChose , int pos , EchoEventType tp)
    {
        int op = isChose ? 1 : -1;
        _echoCnt += op;
        if(_echoCnt > 2)
        {
            _echoCnt -= op;
            Debug.Log("无法选择更多");
            Events[pos].SetClick(false);
            return;
        }
        echoEventType += (op * (int)tp);
        SetEchoEvent();
        Debug.Log(echoEventType.GetType());
    }

    #endregion

}
