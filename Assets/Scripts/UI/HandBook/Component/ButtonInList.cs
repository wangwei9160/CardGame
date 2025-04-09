using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInList : MonoBehaviour
{
    public Action<int> onClickItem;

    public Button btn;

    public void Create(int idx , string _name)
    {
        name = _name;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Debug.Log("idx = " + idx.ToString());
            if(onClickItem != null) onClickItem(idx);
        });
    }

}
