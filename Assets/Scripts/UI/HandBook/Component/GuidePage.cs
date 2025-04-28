using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePage : UIViewBase
{
    [SerializeField] private GameObject tutorialItemPrefab;  // TutorialItem预制体
    private List<TutorialClass> tutorials;                   // 所有教程数据
    private Text contentText;                                // 内容显示Text组件
    private Transform content;                               // Content Transform
    private Button downBtn;                                  // 下一页按钮
    private Button upBtn;                                    // 上一页按钮
    private int currentPage = 0;                            // 当前页码
    private const int ITEMS_PER_PAGE = 4;                   // 每页显示数量
    private int selectedTutorialId = -1;                    // 当前选中的教程ID

    [Header("TutorialItem布局设置")]
    private float itemWidth = 200f;        // 按钮宽度
    private float itemHeight = 96f;        // 按钮高度
    private float itemSpacing = 20f;       // 按钮间距
    private float startY = 20f;           // 起始Y坐标

    protected override void Start()
    {
        base.Start();
        Debug.Log("GuidePage Start");
        InitializeComponents();
        LoadTutorials();
        ShowCurrentPage();
    }

    private void InitializeComponents()
    {
        // 获取Content Transform
        content = transform.Find("Scroll View/Viewport/Content");
        if (content == null)
        {
            Debug.LogError("找不到Content Transform");
            return;
        }

        // 设置Content的RectTransform
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1);
            float contentHeight = (itemHeight + itemSpacing) * ITEMS_PER_PAGE;
            contentRect.sizeDelta = new Vector2(0, contentHeight);
        }

        // 获取内容显示Text
        contentText = transform.Find("Content Panel/Text").GetComponent<Text>();
        if (contentText == null)
        {
            Debug.LogError("找不到Content Text组件");
            return;
        }

        // 获取上一页按钮
        upBtn = transform.Find("upBtn").GetComponent<Button>();
        if (upBtn == null)
        {
            Debug.LogError("找不到upBtn按钮");
            return;
        }
        upBtn.onClick.AddListener(OnPreviousPage);

        // 获取下一页按钮
        downBtn = transform.Find("downBtn").GetComponent<Button>();
        if (downBtn == null)
        {
            Debug.LogError("找不到downBtn按钮");
            return;
        }
        downBtn.onClick.AddListener(OnNextPage);

        // 加载TutorialItem预制体
        tutorialItemPrefab = Resources.Load<GameObject>("UI/HandBook/Component/TutorialItem");
        if (tutorialItemPrefab == null)
        {
            Debug.LogError("找不到TutorialItem预制体");
            return;
        }
    }

    private void LoadTutorials()
    {
        tutorials = TutorialConfig.GetAll();
        if (tutorials == null || tutorials.Count == 0)
        {
            Debug.LogError("没有找到任何教程内容");
            return;
        }
        Debug.Log($"成功加载教程数据，共 {tutorials.Count} 条");

        // 显示第一条教程内容
        if (contentText != null && tutorials.Count > 0)
        {
            contentText.text = tutorials[0].content;
        }
    }

    private void ShowCurrentPage()
    {
        if (tutorials == null || tutorialItemPrefab == null || content == null) 
        {
            Debug.LogError($"ShowCurrentPage失败: tutorials={tutorials != null}, tutorialItemPrefab={tutorialItemPrefab != null}, content={content != null}");
            return;
        }

        // 清除现有的TutorialItem
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 计算当前页的起始和结束索引
        int startIndex = currentPage * ITEMS_PER_PAGE;
        int endIndex = Mathf.Min(startIndex + ITEMS_PER_PAGE, tutorials.Count);

        Debug.Log($"显示第 {currentPage + 1} 页，显示索引范围: {startIndex} - {endIndex}");

        // 创建当前页的TutorialItem
        for (int i = startIndex; i < endIndex; i++)
        {
            int itemIndex = i - startIndex;  // 当前页中的索引
            CreateTutorialItem(tutorials[i], itemIndex);
        }

        UpdateDownBtnState();
    }

    private void CreateTutorialItem(TutorialClass tutorial, int index)
    {
        GameObject itemObj = Instantiate(tutorialItemPrefab, content);
        
        // 设置RectTransform
        RectTransform rectTransform = itemObj.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 1);
            rectTransform.anchorMax = new Vector2(0.5f, 1);
            rectTransform.pivot = new Vector2(0.5f, 1);
            rectTransform.sizeDelta = new Vector2(itemWidth, itemHeight);
            float yPos = -startY - (itemHeight + itemSpacing) * index;
            rectTransform.anchoredPosition = new Vector2(0, yPos);
        }

        // 设置标题文本
        Text titleText = itemObj.transform.Find("Text").GetComponent<Text>();
        if (titleText != null)
        {
            titleText.text = tutorial.title;
        }
        else
        {
            Debug.LogError($"教程按钮 {tutorial.id} 的Text组件获取失败");
        }

        // 获取OnClick和NoClick对象
        GameObject onClickObj = itemObj.transform.Find("OnClick").gameObject;
        GameObject noClickObj = itemObj.transform.Find("NoClick").gameObject;
        
        if (onClickObj != null && noClickObj != null)
        {
            // 设置初始状态
            bool isSelected = tutorial.id == selectedTutorialId;
            onClickObj.SetActive(isSelected);
            noClickObj.SetActive(!isSelected);

            // 添加点击事件
            Button onClickBtn = onClickObj.GetComponent<Button>();
            Button noClickBtn = noClickObj.GetComponent<Button>();

            if (onClickBtn != null && noClickBtn != null)
            {
                // 为两个按钮添加相同的点击事件
                UnityEngine.Events.UnityAction clickAction = () => OnTutorialItemClick(tutorial, itemObj);
                onClickBtn.onClick.AddListener(clickAction);
                noClickBtn.onClick.AddListener(clickAction);
            }
            else
            {
                Debug.LogError($"教程按钮 {tutorial.id} 的Button组件获取失败");
            }
        }
        else
        {
            Debug.LogError($"教程按钮 {tutorial.id} 的OnClick或NoClick对象获取失败");
        }
    }

    private void OnTutorialItemClick(TutorialClass tutorial, GameObject clickedItem)
    {
        // 更新选中状态
        bool wasSelected = selectedTutorialId == tutorial.id;
        selectedTutorialId = wasSelected ? -1 : tutorial.id;

        // 更新所有TutorialItem的显示状态
        foreach (Transform child in content)
        {
            GameObject onClickObj = child.Find("OnClick").gameObject;
            GameObject noClickObj = child.Find("NoClick").gameObject;
            
            if (child.gameObject == clickedItem)
            {
                // 更新点击的项目状态
                onClickObj.SetActive(!wasSelected);
                noClickObj.SetActive(wasSelected);
            }
            else
            {
                // 其他项目都设置为未选中状态
                onClickObj.SetActive(false);
                noClickObj.SetActive(true);
            }
        }

        // 更新内容显示
        if (contentText != null && !wasSelected)
        {
            // 处理文本段落和缩进
            string formattedText = FormatContentText(tutorial.content);
            contentText.text = formattedText;
        }
        else if (contentText != null)
        {
            contentText.text = string.Empty;
        }
    }

    private string FormatContentText(string content)
    {
        if (string.IsNullOrEmpty(content))
            return string.Empty;

        // 分割段落（按双换行符分割）
        string[] paragraphs = content.Split(new string[] { "\n\n", "\r\n\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        
        // 处理每个段落
        for (int i = 0; i < paragraphs.Length; i++)
        {
            // 移除段落首尾的空白字符
            paragraphs[i] = paragraphs[i].Trim();
            
            // 添加缩进和段落间距
            if (!string.IsNullOrEmpty(paragraphs[i]))
            {
                // 使用两个空格作为缩进
                paragraphs[i] = "  " + paragraphs[i];
            }
        }

        // 使用单换行符连接段落
        return string.Join("\n", paragraphs);
    }

    private void OnPreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowCurrentPage();
        }
    }

    private void OnNextPage()
    {
        if ((currentPage + 1) * ITEMS_PER_PAGE < tutorials.Count)
        {
            currentPage++;
            ShowCurrentPage();
        }
    }

    private void UpdateDownBtnState()
    {
        if (downBtn != null && upBtn != null)
        {
            bool isLastPage = (currentPage + 1) * ITEMS_PER_PAGE >= tutorials.Count;
            bool isFirstPage = currentPage == 0;
            
            downBtn.gameObject.SetActive(!isLastPage);
            upBtn.gameObject.SetActive(!isFirstPage);
        }
    }
}
