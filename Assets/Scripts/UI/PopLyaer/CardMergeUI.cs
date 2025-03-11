using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMergeUI : UIViewBase
{
    public override UIViewType Type => UIViewType.Singleton;

    public override UILAYER Layer => UILAYER.M_POP_LAYER;

    public Sprite smallSpirte;
    public Sprite largeSpirte;

    public Button CloseBtn;
    public Button MergeBtn;
    public Button BreakBtn;

    public GameObject[] SmallCards;
    public GameObject smallDragCard;
    public Transform OwnerSmallCards;
    public Transform OwnerLargeCards;

    protected override void Start()
    {
        base.Start();
        CloseBtn = transform.Find("Canvas/Buttons/CloseBtn").GetComponent<Button>();
        MergeBtn = transform.Find("Canvas/Buttons/MergeBtn").GetComponent<Button>();
        BreakBtn = transform.Find("Canvas/Buttons/BreakBtn").GetComponent<Button>();
        SmallCards = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            SmallCards[i] = transform.Find("Canvas/SmallCard" + i.ToString()).gameObject;
            SmallCards[i].AddComponent<SmallCardRender>();  // 加上渲染器
            SmallCards[i].GetComponent<SmallCardRender>().SetPosition(i);
        }
        smallDragCard = transform.Find("Canvas/SmallDragCard").gameObject;
        smallDragCard.AddComponent<SmallCardRender>();
        OwnerSmallCards = transform.Find("Canvas/SmallCards");
        foreach (Transform item in OwnerSmallCards)
        {
            item.AddComponent<SmallCardRender>();
        }
        OwnerLargeCards = transform.Find("Canvas/LargeCards");

        CloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close(Name);
            Destroy(gameObject);
        });
        MergeBtn.onClick.AddListener(() =>
        {
            Debug.Log("合成一张大卡");
        });
        BreakBtn.onClick.AddListener(() =>
        {
            Debug.Log("拆分成三张小卡");
        });
    }

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener(EventDefine.ON_CARD_DRAG_START, OnCardDragStart);
        EventCenter.AddListener(EventDefine.ON_CARD_DRAG_STOP, OnCardDragStop);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener(EventDefine.ON_CARD_DRAG_START, OnCardDragStart);
        EventCenter.RemoveListener(EventDefine.ON_CARD_DRAG_STOP, OnCardDragStop);
    }

    public void OnCardDragStart()
    {
        smallDragCard.gameObject.SetActive(true);
        smallDragCard.GetComponent<Image>().raycastTarget = false;
        EventCenter.AddListener<PointerEventData>(EventDefine.ON_CARD_DRAG, smallDragCard.GetComponent<SmallCardRender>().OnDragMove);
    }

    public void OnCardDragStop()
    {
        smallDragCard.gameObject.SetActive(false);
        smallDragCard.GetComponent<Image>().raycastTarget = true;
        EventCenter.RemoveListener<PointerEventData>(EventDefine.ON_CARD_DRAG, smallDragCard.GetComponent<SmallCardRender>().OnDragMove);
    }

    public void InitUIView()
    {
        
    }

}
