using System.Collections.Generic;
using UnityEngine;


public class UIManager : ManagerBase<UIManager>
{
    private Dictionary<string, UIViewBase> m_Dic = new Dictionary<string, UIViewBase>();

    private void Start()
    {
        Show("BattleUI");
    }

    public void Show(string uiName)
    {
        if (!m_Dic.ContainsKey(uiName))
        {
            GameObject prefab = Resources.Load<GameObject>($"UI/{uiName}");
            GameObject obj = Instantiate(prefab , transform);
            UIViewBase ui = obj.GetComponent<UIViewBase>();
            m_Dic[uiName] = ui;
        }
        m_Dic[uiName].Show();
    }

    public void Hide(string uiName)
    {
        if(m_Dic.ContainsKey(uiName))
        {
            m_Dic[uiName].Hide();
        }
    }

    public void Close(string uiName)
    {
        if(m_Dic.ContainsKey(uiName))
        {
            m_Dic[uiName].Close();
            m_Dic.Remove(uiName);
        }
    }

}

