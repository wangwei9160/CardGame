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
    private Image backgroundImage;

    protected override void Start()
    {
        base.Start();
        pageHelper = new HandBookPageHelper();
        uiCache = new List<UIViewBase>();
        tableBtnList = transform.Find("tableList").GetComponentsInChildren<ButtonInList>();
        page = transform.Find("page");
        closeBtn = transform.Find("closeBtn").GetComponent<Button>();
        backgroundImage = transform.Find("BG").GetComponent<Image>();
        
        for (int i = 0; i < tableBtnList.Length; i++)
        {
            ButtonInList btn = tableBtnList[i];
            btn.Create(i, pageHelper.getCfg(i).Name);
            btn.onClickItem = (index) => this.OnClickItem(index);
        }
        closeBtn.onClick.AddListener(() => { UIManager.Instance.Close(name); });
        OnClickItem(0);
    }

    public void OnClickItem(int idx)
    {
        string uiName = "";
        for(int i = 0; i < tableBtnList.Length; i++)
        {
            if(i != idx)
            {
                // 隐藏其他按钮
                tableBtnList[i].ClickHide();
            }else
            {
                // 显示当前按钮
                tableBtnList[i].ClickShow();
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

        // 无论 UI 是否已经打开，都更新背景
        UpdateBackground(uiName);

        if (isOpen) return;
        
        UiConfig uiConfig = pageHelper.getCfg(uiName);
        if (uiConfig == null)
        {
            Debug.Log("该UI不存在。 uiName = " + uiName);
            return;
        }
        CreatePrefabByName(uiConfig);
    }

    private void UpdateBackground(string pageName)
    {
        if (backgroundImage == null) return;
        
        string bgPath = "";
        if (pageName == "CardPage")
        {
            bgPath = "UI/HandBook/Background/CardBg";
        }
        else
        {
            bgPath = "UI/HandBook/Background/DefaultBg";
        }

        Sprite bgSprite = Resources.Load<Sprite>(bgPath);
        if (bgSprite != null)
        {
            backgroundImage.sprite = bgSprite;
        }
        else
        {
            Debug.LogWarning($"找不到背景图: {bgPath}");
        }
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
