using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase<UIManager>
{
    private Dictionary<int , Transform> m_LayerTransform = new Dictionary<int , Transform>();               // Layer -> Transform
    private Dictionary<int , Stack<UIViewBase>> m_LayerUIViews = new Dictionary<int, Stack<UIViewBase>>();    // 每一层持有的ui

    private Dictionary<string, UIViewBase> m_SingleDic = new Dictionary<string, UIViewBase>();

    private Dictionary<string, List<UIViewBase>> m_MultipDic = new Dictionary<string, List<UIViewBase>>();
    private Dictionary<string ,int > m_MultipDicCnt = new Dictionary<string ,int>();

    protected override void Awake()
    {
        base.Awake();
        foreach (UILAYER layer in Enum.GetValues(typeof(UILAYER)))
        {
            int _layer = (int)layer;
            m_LayerUIViews[_layer] = new Stack<UIViewBase>();
            GameObject obj = new GameObject(layer.ToString());
            obj.transform.SetParent(gameObject.transform);
            m_LayerTransform[_layer] = obj.transform;
        }

    }

    #region layer 相关操作
    // 特有的遮挡关系
    private bool isNeed(int _layer)
    {
        return (_layer & (int)UILAYER.M_NORMAL_LAYER) != 0 || (_layer & (int)UILAYER.M_POP_LAYER) != 0;
    }

    private void Push(UIViewBase ui)
    {
        if (!isNeed((int)ui.Layer)) return;
        int layerID = (int)ui.Layer;
        if(!m_LayerUIViews.ContainsKey(layerID)) m_LayerUIViews[(int)UILAYER.M_NORMAL_LAYER] = new Stack<UIViewBase>();   // model层
        if (m_LayerUIViews[layerID].Count > 0)
        {
            UIViewBase top = m_LayerUIViews[layerID].Peek();
            if (top.Type == UIViewType.Singleton)
            {
                Hide(top.Name);
            }else if(top.Type == UIViewType.Multiple)
            {
                Hide(top.Name ,top.Index);
            }
        }
        m_LayerUIViews[layerID].Push(ui);
    }

    private void Pop(UIViewBase ui)
    {
        if (!isNeed((int)ui.Layer)) return;
        int layerID = (int)ui.Layer;
        m_LayerUIViews[layerID].Pop();
        if (m_LayerUIViews[layerID].Count > 0)
        {
            UIViewBase top = m_LayerUIViews[layerID].Peek();
            if (top.Type == UIViewType.Singleton)
            {
                ReShow(top.Name);
            }
            else if (top.Type == UIViewType.Multiple)
            {
                ReShow(top.Name, top.Index);
            }
        }
    }

    #endregion


    #region ui相关操作
    // 通用的通过uiName 创建UIViewBase
    private UIViewBase CreatePrefabByName(string uiName)
    {
        GameObject prefab = Resources.Load<GameObject>($"UI/{uiName}");
        GameObject obj = Instantiate(prefab);
        UIViewBase ui = obj.GetComponent<UIViewBase>(); Push(ui);
        obj.transform.SetParent(m_LayerTransform[(int)ui.Layer]);
        return ui;
    }

    private GameObject CreatePrefabByPath(string path)
    {
        GameObject prefab = Resources.Load<GameObject>($"{path}");
        GameObject obj = Instantiate(prefab);
        return obj;
    }

    public void Show(string uiName , string data = "{}")
    {
        UiConfig cfg = UIConfigManager.Configs[uiName];
        if (!m_SingleDic.ContainsKey(cfg.Name))
        {
            if(cfg.BindScene != "")
            {
                // 通过prefab 创建场景相关内容
                var obj = CreatePrefabByPath(cfg.BindScene);
                obj.name = cfg.BindScene;
            }

            var uiObj = CreatePrefabByPath(cfg.uiPath);
            UIViewBase ui = uiObj.GetComponent<UIViewBase>(); Push(ui);
            uiObj.name = uiName;
            uiObj.transform.SetParent(m_LayerTransform[(int)ui.Layer]);
            m_SingleDic[uiName] = ui;
            m_SingleDic[uiName].Init(uiName , data);
        }
        m_SingleDic[uiName].Show();
    }

    public void Show(string uiName , GameObject parent , string data = "{}")
    {
        UiConfig cfg = UIConfigManager.Configs[uiName];
        if (!m_SingleDic.ContainsKey(uiName))
        {
            if (cfg.BindScene != "")
            {
                // 通过prefab 创建场景相关内容
                var obj = CreatePrefabByPath(cfg.BindScene);
                obj.name = cfg.BindScene;
            }

            var uiObj = CreatePrefabByPath(cfg.uiPath);
            uiObj.name = uiName;
            UIViewBase ui = uiObj.GetComponent<UIViewBase>(); Push(ui);
            uiObj.transform.SetParent(m_LayerTransform[(int)ui.Layer]);
            m_SingleDic[uiName] = ui;
            m_SingleDic[uiName].Init(uiName, parent , data);
        }
        m_SingleDic[uiName].Show();
    }

    public void Show(string uiName , GameObject parent, ref int Index , string data = "{}")
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

        var ui = CreatePrefabByName(uiName);
        Index = m_MultipDicCnt[uiName]++;   // 自增赋值,需要注意多线程环境
        ui.ResetIndex(Index);
        ui.Init(uiName, parent , data);
        m_MultipDic[uiName].Add(ui);
        
    }

    private void ReShow(string uiName)
    {
        m_SingleDic[uiName].Show();
    }

    private void ReShow(string uiName, int Index)
    {
        for (int i = 0; i < m_MultipDic[uiName].Count; i++)
        {
            if (m_MultipDic[uiName][i].index == Index)
            {
                m_MultipDic[uiName][i].Show();
                return;
            }
        }
    }

    public void Hide(string uiName)
    {
        if(m_SingleDic.ContainsKey(uiName))
        {
            m_SingleDic[uiName].Hide();
        }
    }

    public void Hide(string uiName , int Index)
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
            Pop(m_SingleDic[uiName]);
            m_SingleDic[uiName].Close();
            m_SingleDic.Remove(uiName);
            UiConfig cfg = UIConfigManager.Configs[uiName];
            if(cfg.BindScene != "")
            {
                Destroy(GameObject.Find(cfg.BindScene));
            }
        }
    }

    public void Close(string uiName , int Index)
    {
        if(m_SingleDic.ContainsKey(uiName))
        {
            Pop(m_SingleDic[uiName]);
            m_SingleDic[uiName].Close();
            UiConfig cfg = UIConfigManager.Configs[uiName];
            if(cfg.BindScene != "")
            {
                GameObject obj = GameObject.Find(cfg.BindScene);
                if (obj != null) Destroy(obj);
            }
            m_SingleDic.Remove(uiName);
        }
        for (int i = 0; i < m_MultipDic[uiName].Count; i++)
        {
            if (m_MultipDic[uiName][i].index == Index)
            {
                Pop(m_MultipDic[uiName][i]);
                m_MultipDic[uiName][i].Close();
            }
        }

        if (m_MultipDic[uiName].Count == 0)
        {
            m_MultipDic.Remove(uiName);
            m_MultipDicCnt.Remove(uiName);
        }
    }
    #endregion

}

