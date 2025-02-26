using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    public Button settingBtn;
    public Button deckBtn;
    public Button cardBookBtn;
    public Button endTurnBtn;
    public Transform CardsTransform;

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
        if(state == GameManager.Instance.enemyTurn)
        {
            endTurnBtn.GetComponent<Image>().color = Color.gray;
        }else
        {
            endTurnBtn.GetComponent<Image>().color = Color.green;
        }
    }
    

}

