using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    public Button settingBtn;   // ���ð�ť
    public Button deckBtn;      // ���鰴ť
    public Button cardBookBtn;  // ͼ����ť
    public Button endTurnBtn;   // �غϽ�����ť
    public Transform CardsTransform;    // ��ʱ������п��Ƶ�λ��

    public int CurrentTurn = 0;
    public Text turnInfo;   // �غϼ���

    public List<GameObject> Cards;

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<IFightState>(EventDefine.ChangeState,OnChangeState);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.AddListener<IFightState>(EventDefine.ChangeState, OnChangeState);
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
            // �л����з��غ�
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
        if(state == GameManager.Instance.enemyTurn)
        {
            endTurnBtn.GetComponent<Image>().color = Color.gray;
        }else
        {
            CurrentTurn++;
            turnInfo.text = string.Format("��{0}�غ�",CurrentTurn);
            endTurnBtn.GetComponent<Image>().color = Color.green;
        }
    }
    

}

