using UnityEngine;
using UnityEngine.UI;

public class CardHpUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Multiple;
    public override UILAYER Layer => UILAYER.M_BATTLE_LAYER;
    public Transform left;
    public Transform right;
    public Image licon;
    public Image ricon;
    public Text lcost;
    public Text rcost;
    public int MaxHp;
    public GameObject Owner;

    public override void Init(string uiName,GameObject obj, string data)
    {
        Init(uiName);
        Owner = obj;
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener<int, int>(EventDefine.OnHpChangeByName, OnHpChange);
        EventCenter.AddListener<int>(EventDefine.OnFollowerHpReSetPostion, ReSetPostion);
        EventCenter.AddListener(EventDefine.OnMergePanelShow, OnMergePanelShow);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();   
        EventCenter.RemoveListener<int, int>(EventDefine.OnHpChangeByName, OnHpChange);
        EventCenter.RemoveListener<int>(EventDefine.OnFollowerHpReSetPostion, ReSetPostion);
        EventCenter.RemoveListener(EventDefine.OnMergePanelShow, OnMergePanelShow);
    }

    protected override void Start()
    {
        base.Start();
        right = transform.Find("Canvas/right");
        left = transform.Find("Canvas/left");
        licon = left.Find("icon").GetComponent<Image>();
        lcost = left.Find("value").GetComponent<Text>();
        ricon = right.Find("icon").GetComponent<Image>();
        rcost = right.Find("value").GetComponent<Text>();
        if (Owner != null)
        {
            right.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("Right").transform.position);
            left.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("Left").transform.position);
            BattlePerformUnit unit = Owner.GetComponent<BattlePerformUnit>();
            var leftAttr = unit.GetAttributeShow(0);
            if(leftAttr.Count >= 2)
            {
                licon.sprite = ResourceUtil.GetCardAttributeTypeImage(leftAttr[0]);
                lcost.text = leftAttr[1].ToString();
            }
            var rightAttr = unit.GetAttributeShow(1);
            if (rightAttr.Count >= 2)
            {
                ricon.sprite = ResourceUtil.GetCardAttributeTypeImage(rightAttr[0]);
                rcost.text = rightAttr[1].ToString();
            }
            MaxHp = unit.maxHp;
        }
    }

    private void OnHpChange(int val, int id)
    {
        if (id == Index)
        {
            //HpValue.text = val.ToString();
            if(val <= 0)
            {
                UIManager.Instance.Close(Name , Index);
            }
        }
    }

    public override void AdjustPosition()
    {
        right.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("Right").transform.position);
        left.position = Camera.main.WorldToScreenPoint(Owner.transform.Find("Left").transform.position);
    }

    public void ReSetPostion(int id)
    {
        if(id == Index)
        {
            AdjustPosition();
        }
    }    

    public void OnMergePanelShow()
    {
        Destroy(gameObject);
    }
}
