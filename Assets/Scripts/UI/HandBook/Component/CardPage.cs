using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardPage : UIViewBase
{
    public int MaxSize;
    public GameObject card;
    public Transform content;
    private int baseIndex;

    public Button leftPage;
    public Button rightPage;

    protected override void Start()
    {
        base.Start();
        card = Resources.Load<GameObject>("UI/HandBook/Component/Card");
        content = transform.Find("Scroll View/Viewport/Content");
        
        // 添加GridLayoutGroup组件
        GridLayoutGroup grid = content.gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(321, 441); // 卡牌尺寸
        grid.spacing = new Vector2(40, 0); // 卡牌之间的水平间距为40，垂直间距为0
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 4; // 每行4个
        grid.padding = new RectOffset(0, 0, 50, 0); // 顶部间距50
        
        MaxSize = 4; // 每页显示4个卡片
        baseIndex = 0;
        for(int i = 0; i < MaxSize; i++)
        {
            GameObject go = Instantiate(card);
            go.transform.SetParent(content);
        }
        leftPage = transform.Find("leftBtn").GetComponent<Button>();
        rightPage = transform.Find("rightBtn").GetComponent<Button>();
        leftPage.onClick.AddListener(() => { updatePage(-1); });
        rightPage.onClick.AddListener(() => { updatePage(1); });
        updateCard();
    }

    public void updateCard()
    {
        List<CardClass> _list = CardConfig.GetAll();
        for(int i = 0; i < MaxSize; i++)
        {
            int idx = baseIndex + i;
            if(idx < _list.Count)
            {
                content.GetChild(i).GetComponent<CardShow>().SetData(_list[idx]);
            }
            else
            {
                content.GetChild(i).GetComponent<CardShow>().Hide();
            }
        }
    }

    public void updatePage(int op)
    {
        int nxtIndex = baseIndex + op * MaxSize; 
        
        if (nxtIndex >= CardConfig.m_Dic.Count || nxtIndex < 0)
        {
            return;
        }
        baseIndex = nxtIndex;
        updateCard();
    }
}
