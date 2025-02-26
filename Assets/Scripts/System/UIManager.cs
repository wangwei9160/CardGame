using System.Collections.Generic;
using UnityEngine;


public class UIManager : ManagerBase<UIManager>
{
    private Dictionary<string, UIViewBase> m_SingleDic = new Dictionary<string, UIViewBase>();

    private Dictionary<string, List<UIViewBase>> m_MultipDic = new Dictionary<string, List<UIViewBase>>();
    private Dictionary<string ,int > m_MultipDicCnt = new Dictionary<string ,int>();

    public void Show(string uiName)
    {
        if (!m_SingleDic.ContainsKey(uiName))
        {
            GameObject prefab = Resources.Load<GameObject>($"UI/{uiName}");
            GameObject obj = Instantiate(prefab , transform);
            UIViewBase ui = obj.GetComponent<UIViewBase>();
            m_SingleDic[uiName] = ui;
            m_SingleDic[uiName].Init(uiName);
        }
        m_SingleDic[uiName].Show();
    }

    public void Show(string uiName , GameObject val , ref int Index)
    {
        if (!m_MultipDic.ContainsKey(uiName))
        {
            m_MultipDic[uiName] = new List<UIViewBase>();
            m_MultipDicCnt[uiName] = 0;
        }

        for (int i = 0; i < m_MultipDic[uiName].Count; i++)
        {
            if (m_MultipDic[uiName][i].index == Index)
            {
                m_MultipDic[uiName][i].Show();
                return;
            }
        }
        
        GameObject prefab = Resources.Load<GameObject>($"UI/{uiName}");
        GameObject obj = Instantiate(prefab, transform);
        UIViewBase ui = obj.GetComponent<UIViewBase>();
        Index = m_MultipDicCnt[uiName]++;   // 自增赋值，需要注意多线程环境
        ui.ResetIndex(Index);
        ui.Init(val);
        m_MultipDic[uiName].Add(ui);
        
    }

    public void Hide(string uiName)
    {
        if(m_SingleDic.ContainsKey(uiName))
        {
            m_SingleDic[uiName].Hide();
        }
    }

    public void Hide(string uiName , ref int Index)
    {
        for (int i = 0; i < m_MultipDic[uiName].Count; i++)
        {
            if (m_MultipDic[uiName][i].index == Index)
            {
                m_MultipDic[uiName][i].Hide();
                return;
            }
        }
    }

    public void Close(string uiName)
    {
        if(m_SingleDic.ContainsKey(uiName))
        {
            m_SingleDic[uiName].Close();
            m_SingleDic.Remove(uiName);
        }
    }

    public void Close(string uiName , ref int Index)
    {
        for (int i = 0; i < m_MultipDic[uiName].Count; i++)
        {
            if (m_MultipDic[uiName][i].index == Index)
            {
                m_MultipDic[uiName][i].Close();
            }
        }

        if (m_MultipDic[uiName].Count == 0)
        {
            m_MultipDic.Remove(uiName);
            m_MultipDicCnt.Remove(uiName);
        }
    }

}

