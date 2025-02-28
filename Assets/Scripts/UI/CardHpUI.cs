using UnityEngine;
using UnityEngine.UI;

public class CardHpUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Multiple;
    public GameObject pos;
    public Image bg;
    public Text HpValue;
    public int MaxHp;
    public GameObject Owner;

    public override void Init(GameObject obj)
    {
        Owner = obj;
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<float, int>(EventDefine.OnHpChangeByName, OnHpChange);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();   
        EventCenter.RemoveListener<float, int>(EventDefine.OnHpChangeByName, OnHpChange);
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

    private void OnHpChange(float val, int id)
    {
        if (id == Index)
        {
            HpValue.text = val.ToString();
        }
    }

    public override void AdjustPosition()
    {
        pos.transform.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("HP").transform.position);
        Debug.Log(pos.transform.position);
    }
}
