using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIViewBase
{
    public override string Name => "BattleUI";

    public Button settingBtn;
    public Button deckBtn;
    public Button cardBookBtn;
    public Transform CardsTransform;

    public List<GameObject> Cards;


    protected override void Start()
    {
        Cards = new List<GameObject>();
        settingBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Show("SettingUI");
        });
        //deckBtn.onClick.AddListener(() =>
        //{
        //    UIManager.Instance.Show("DeckUI");
        //});
        //cardBookBtn.onClick.AddListener(() =>
        //{
        //    UIManager.Instance.Show("CardBookUI");
        //});
    }

}

