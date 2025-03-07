using UnityEngine;
using UnityEngine.UI;

public class CardHpUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Multiple;
    public override UILAYER Layer => UILAYER.M_BATTLE_LAYER;
    public GameObject pos;
    public Image bg;
    public Text HpValue;
    public int MaxHp;
    public GameObject Owner;

    public override void Init(string uiName,GameObject obj)
    {
        Init(uiName);
        Owner = obj;
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<int, int>(EventDefine.OnHpChangeByName, OnHpChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();   
        EventCenter.RemoveListener<int, int>(EventDefine.OnHpChangeByName, OnHpChange);
    }

    protected override void Start()
    {
        base.Start();
        if (Owner != null)
        {
            pos.transform.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("HP").transform.position);
            BaseCharacter character = Owner.GetComponent<BaseCharacter>();
            HpValue.text = character.hp.ToString();
            MaxHp = character.maxHp;
        }
    }

    private void OnHpChange(int val, int id)
    {
        if (id == Index)
        {
            HpValue.text = val.ToString();
            if(val <= 0)
            {
                UIManager.Instance.Close(Name , Index);
            }
        }
    }

    public override void AdjustPosition()
    {
        pos.transform.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("HP").transform.position);
        Debug.Log(pos.transform.position);
    }
}
