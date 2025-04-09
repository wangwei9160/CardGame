using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandBookUI : UIViewBase
{
    public override UILAYER Layer => UILAYER.M_TIP_LAYER;

    public ButtonInList[] tableBtnList;

    public HandBookPageHelper pageHelper;

    public List<UIViewBase> uiCache;
    public Transform page;
    public Button closeBtn;

    protected override void Start()
    {
        base.Start();
        pageHelper = new HandBookPageHelper();
        uiCache = new List<UIViewBase>();
        tableBtnList = transform.Find("tableList").GetComponentsInChildren<ButtonInList>();
        page = transform.Find("page");
        closeBtn = transform.Find("closeBtn").GetComponent<Button>();
        for (int i = 0; i < tableBtnList.Length; i++)
        {
            ButtonInList btn = tableBtnList[i];
            btn.Create(i, pageHelper.getCfg(i).Name);
            btn.onClickItem = (index) => this.OnClickItem(index);
        }
        closeBtn.onClick.AddListener(() => { UIManager.Instance.Close(name); });
    }

    public void OnClickItem(int idx)
    {
        string uiName = "";
        for(int i = 0; i < tableBtnList.Length; i++)
        {
            if(i != idx)
            {
                // ÇÐ»»Õý³£×´Ì¬
            }else
            {
                // ÇÐ»»µã»÷×´Ì¬
                uiName = tableBtnList[i].name;
            }
        }
        bool isOpen = false;
        foreach(UIViewBase ui in uiCache)
        {
            if(ui.name == uiName)
            {
                isOpen = true;
                ui.Show();
            }else
            {
                ui.Hide();
            }
        }
        if (isOpen) return;
        UiConfig uiConfig = pageHelper.getCfg(uiName);
        if (uiConfig == null)
        {
            Debug.Log("¡¾UI²»´æÔÚ¡¿ uiName = " + uiName);
            return;
        }
        CreatePrefabByName(uiConfig);
    }

    private UIViewBase CreatePrefabByName(UiConfig cfg)
    {
        GameObject prefab = Resources.Load<GameObject>($"{cfg.uiPath}");
        GameObject obj = Instantiate(prefab);
        UIViewBase ui = obj.GetComponent<UIViewBase>();
        obj.transform.SetParent(page);
        obj.name = cfg.Name;
        uiCache.Add(ui);
        ui.Init(cfg.Name);
        return ui;
    }

}
