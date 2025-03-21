using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    [Header("按钮")]
    [Tooltip("结束按钮")] public Button endTurnBtn;   // 回合结束按钮
    private bool isLock = false;                    // 回合结束锁
    public Transform CardsTransform;    // 临时存放所有卡牌的位置

    [Header("文本")]
    [Tooltip("回合计数")] public Text turnInfo;   // 回合计数
    [Tooltip("法力值")] public Text magicPowerInfo;   // 法力值信息

    public List<GameObject> Cards;
    //private EchoEventConfig cfg;

    public override void Init(string str, string data)
    {
        base.Init(str, data);
        //cfg = JsonUtility.FromJson<EchoEventConfig>(data);
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.AddListener(EventDefine.OnPlayerTurnStart, OnPlayerTurn);
        EventCenter.AddListener<int , int>(EventDefine.OnMagicPowerChange, OnMagicPowerChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener(EventDefine.OnBeforePlayerTurn, OnBeforePlayerTurn);
        EventCenter.RemoveListener(EventDefine.OnPlayerTurnStart, OnPlayerTurn);
        EventCenter.RemoveListener<int , int>(EventDefine.OnMagicPowerChange, OnMagicPowerChange);
    }

    protected override void Start()
    {
        base.Start();
        Cards = new List<GameObject>();
        
        endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(0.6f);
        endTurnBtn.onClick.AddListener(() =>
        {
            if (isLock) return;
            // 切换到敌方回合
            isLock = true;      // 防止连点异常
            StartCoroutine(WaitTimeUnLock());   // 定时解锁
            endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(0.6f);
            GameManager.Instance.FinishTurn();
        });
    }

    public override void Show()
    {
        base.Show();
        isLock = true;
        EventCenter.Broadcast(EventDefine.OnBattleStart);   // 进入战斗准备
        OnMagicPowerChange(GameManager.Instance.Data.MagicPower , GameManager.Instance.Data.MaxMagicPower);// 法力值信息的更新
    }

    public override void Close()
    {
        base.Close();
        //if(cfg.prefabName != null)
        //{
        //    var go = GameObject.Find(cfg.prefabName);
        //    Destroy(go);
        //}
    }

    IEnumerator WaitTimeUnLock()
    {
        yield return new WaitForSeconds(0.5f);
        isLock = false;
    }

    // 修正当前回合数
    private void OnBeforePlayerTurn()
    {
        GameManager.Instance.Data.CurrentTurn++;
        turnInfo.text = GameManager.Instance.Data.CurrentTurn.ToString();
    }

    private void OnPlayerTurn()
    {
        // 进入到玩家可操作回合
        isLock = false;
        endTurnBtn.GetComponent<EndTurnBtnHover>().OnChange(1f);
        GameManager.Instance.OnMagicPowerChange(1,1);
        //endTurnBtn.GetComponent<Image>().color = Color.green;
    }
    
    private void OnMagicPowerChange(int val , int maxVal)
    {
        magicPowerInfo.text = string.Format(GameString.MAGICEINFO, val, maxVal);
    }

}