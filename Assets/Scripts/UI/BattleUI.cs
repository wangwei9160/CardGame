using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    [Header("按钮")]
    [Tooltip("设置按钮")]public Button settingBtn;   // 设置按钮
    [Tooltip("牌组按钮")] public Button deckBtn;      // 牌组按钮
    [Tooltip("图鉴按钮")] public Button cardBookBtn;  // 图鉴按钮
    [Tooltip("结束按钮")] public Button endTurnBtn;   // 回合结束按钮
    public Transform CardsTransform;    // 临时存放所有卡牌的位置

    [Header("文本")]
    [Tooltip("血量")] public Text hpText; // 血量
    [Tooltip("钱")] public Text moneyText;   

    public int CurrentTurn = 0;
    public Text turnInfo;   // 回合计数

    public List<GameObject> Cards;

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<IFightState>(EventDefine.ChangeState, OnChangeState);
        EventCenter.AddListener<int,int,int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener<IFightState>(EventDefine.ChangeState, OnChangeState);
        EventCenter.RemoveListener<int, int, int>(EventDefine.OnPlayerAttributeChange, OnHpChange);
    }

    protected override void Start()
    {

        Cards = new List<GameObject>();
        settingBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Show("SettingUI");
        });
        endTurnBtn.onClick.AddListener(() =>
        {
            // 切换到敌方回合
            GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.enemyTurn);
        });
        //deckBtn.onClick.AddListener(() =>
        //{
        //    UIManager.Instance.Show("DeckUI");
        //});
        //cardBookBtn.onClick.AddListener(() =>
        //{
        //    UIManager.Instance.Show("CardBookUI");
        //});
        base.Start();
    }

    private void OnChangeState(IFightState state)
    {
        if (state == GameManager.Instance.enemyTurn)
        {
            endTurnBtn.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            CurrentTurn++;
            turnInfo.text = string.Format("第{0}回合", CurrentTurn);
            endTurnBtn.GetComponent<Image>().color = Color.green;
        }
    }

    private void OnHpChange(int hp, int maxHp, int id)
    {
        hpText.text = string.Format("{0}/{1}",hp,maxHp);
    }


    

}