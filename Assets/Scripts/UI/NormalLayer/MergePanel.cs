using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        ChoseRoadInit();
    }

    #region MergeEvent
    public Text Title;
    public EchoEventRender[] Events;
    public Transform EventsTransform;

    public EchoEventRender echoEvent;

    private EchoEventType echoEventType;
    private Text echoEventMessage;

    public Button confirmBtn;
    public List<EchoEventType> baseTypes;

    public void MergeEventInit()
    {
        Title = mergeEvent.transform.Find("Title/text").GetComponent<Text>();
        Title.text = "请选择你的道路";
        EventsTransform = mergeEvent.transform.Find("Events");
        Events = new EchoEventRender[EventsTransform.childCount];
        baseTypes = new List<EchoEventType>();
        foreach (var kv in EchoEventManager.m_Dic)
        {
            if(kv.Value.ps == "基础类型")
            {
                baseTypes.Add((EchoEventType)kv.Key);
            }
        }
        for (int i = 0; i < EventsTransform.childCount; i++)
        {
            Events[i] = EventsTransform.GetChild(i).GetComponent<EchoEventRender>();
            
            EchoEventType typeId = RandomUtil.GetRandomValueInList(baseTypes, i == 0 ? 2 : 4); // 限制前2个
            Events[i].SetData(typeId, i);
        }
        echoEventType = EchoEventType.UnKnow;   // 初始化为空
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
                RefreshChoseRoad();
            }
        });
    }

    private void SetEchoEvent()
    {
        echoEvent.SetData(echoEventType , 0);
        var type = EchoEventManager.GetEchoEventClassByKey((int)echoEventType);
        string msg = type.description;
        echoEventMessage.text = msg;
    }

    private int _echoCnt = 0;
    private void OnEchoEvenSelect(bool isChose , int pos , EchoEventType tp)
    {
        int op = isChose ? 1 : -1;
        _echoCnt += op;
        if(_echoCnt > 2)
        {
            _echoCnt -= op;
            //Debug.Log("无法选择更多");
            Events[pos].SetClick(false);
            return;
        }
        echoEventType += (op * (int)tp);
        SetEchoEvent();
        //Debug.Log(echoEventType.GetType());
    }

    #endregion

    #region ChoseRoad

    public Button rightEventBtn;
    public Button leftEventBtn;

    public EchoEventRender rightEventRender;
    public EchoEventRender leftEventRender;

    public void ChoseRoadInit()
    {
        rightEventBtn = transform.Find("Canvas/ChoseRoad/RightEvent").GetComponent<Button>();
        rightEventBtn.onClick.AddListener(() => {GoNext(1); });
        rightEventRender = transform.Find("Canvas/ChoseRoad/RightEvent/EventRender").GetComponent<EchoEventRender>();

        leftEventBtn = transform.Find("Canvas/ChoseRoad/LeftEvent").GetComponent<Button>();
        leftEventBtn.onClick.AddListener(() => { GoNext(0); });
        leftEventRender = transform.Find("Canvas/ChoseRoad/LeftEvent/EventRender").GetComponent<EchoEventRender>();
    }

    public void RefreshChoseRoad()
    {
        rightEventRender.SetData(EchoEventType.FightEvent, -1);
        leftEventRender.SetData(echoEventType, -1);
    }

    public void GoNext(int turn)
    {
        Debug.Log("选择" + (turn == 1 ? "既定的道路" : "自我的探索"));
        UIManager.Instance.Close(Name);
        if(turn == 1)
        {
            echoEventType = EchoEventType.FightEvent;
        }else
        {
            int rd = RandomUtil.RandomInt(0, 2);
            if(rd == 0)
            {
                echoEventType = EchoEventType.ChanceEvent;
            }else
            {
                echoEventType = EchoEventType.PeaceEvent;
            }
        }
        GameManager.OnEnterEchoEvent(echoEventType);
    }

    #endregion

}
