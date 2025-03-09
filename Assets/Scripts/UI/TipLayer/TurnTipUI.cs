using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTipUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Multiple;
    public override UILAYER Layer => UILAYER.M_TIP_LAYER;

    public Text text;

    protected override void Start()
    {
        base.Start();
        text = transform.Find("Info").GetComponent<Text>();
        text.text = string.Format(GameString.TURNINFO, GameManager.Instance.Data.CurrentTurn);
    }
}
