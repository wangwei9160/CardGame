using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInList : MonoBehaviour
{
    public Action<int> onClickItem;

    public Button btn;
    public GameObject show;

    private void Awake()
    {
        btn = GetComponent<Button>();
        show = transform.Find("onClick").gameObject;
    }

    public void Create(int idx , string _name)
    {
        name = _name;
        
        btn.onClick.AddListener(() =>
        {
            //Debug.Log("idx = " + idx.ToString());
            if(onClickItem != null) onClickItem(idx);
        });
    }

    public void ClickShow()
    {
        show.SetActive(true);
    }

    public void ClickHide()
    {
        show.SetActive(false);
    }

}
