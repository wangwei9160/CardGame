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
        MaxSize = 10;
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
        List<Test0Class> _list = Test0Manager.GetAllCard();
        for(int i = 0; i <= MaxSize; i++)
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
        
        if (nxtIndex >= Test0Manager.m_Dic.Count || nxtIndex < 0)
        {
            return;
        }
        baseIndex = nxtIndex;
        updateCard();
    }

}
