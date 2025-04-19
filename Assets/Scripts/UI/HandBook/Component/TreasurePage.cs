using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasurePage : UIViewBase
{
    public int MaxSize;
    public GameObject Treasure;
    public Transform content;
    private int baseIndex;

    public Button leftPage;
    public Button rightPage;

    protected override void Start()
    {
        base.Start();
        Treasure = Resources.Load<GameObject>("UI/HandBook/Component/Treasure");
        content = transform.Find("Scroll View/Viewport/Content");
        MaxSize = 10;
        baseIndex = 0;
        for(int i = 0; i < MaxSize; i++)
        {
            GameObject go = Instantiate(Treasure);
            go.transform.SetParent(content);
        }
        leftPage = transform.Find("leftBtn").GetComponent<Button>();
        rightPage = transform.Find("rightBtn").GetComponent<Button>();
        leftPage.onClick.AddListener(() => { updatePage(-1); });
        rightPage.onClick.AddListener(() => { updatePage(1); });
        updateTreasure();
    }

    public void updateTreasure()
    {
        List<TreasureClass> _list = TreasureConfig.GetAll();
        for(int i = 0; i < MaxSize; i++)
        {
            int idx = baseIndex + i;
            if(idx < _list.Count)
            {
                content.GetChild(i).GetComponent<TreasureShow>().SetData(_list[idx]);
            }
            else
            {
                content.GetChild(i).GetComponent<TreasureShow>().Hide();
            }
        }
    }

    public void updatePage(int op)
    {
        int nxtIndex = baseIndex + op * MaxSize; 
        
        if (nxtIndex >= TreasureConfig.m_Dic.Count || nxtIndex < 0)
        {
            return;
        }
        baseIndex = nxtIndex;
        updateTreasure();
    }

}
