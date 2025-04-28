using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTreasure_S : MonoBehaviour
{
    private random randomComponent;
    private Image treasureImage;
    private TreasureClass currentTreasure;

    [SerializeField]
    private Image treasureImageReference; // 可以在Inspector中直接拖放引用

    // Start is called before the first frame update
    void Start()
    {
        // 获取random组件
        randomComponent = GetComponent<random>();
        if (randomComponent == null)
        {
            Debug.Log("添加random组件");
            randomComponent = gameObject.AddComponent<random>();
        }

        // 确保random组件已初始化
        randomComponent.InitializeTreasureLists();

        // 如果没有通过Inspector设置，则通过路径查找
        if (treasureImageReference == null)
        {
            // 首先尝试获取子对象的Image组件
            treasureImage = GetComponentInChildren<Image>();

            if(treasureImage != null)
            {
                Debug.Log("找到宝物图片组件");
            }
            
            // 如果在子对象中找不到，则通过完整路径查找
            if (treasureImage == null)
            {
                GameObject imageObject = GameObject.Find("Canvas/StoreUI/Treasures/StoreTreasure");
                if (imageObject != null)
                {
                    treasureImage = imageObject.GetComponent<Image>();
                    if (treasureImage == null)
                    {
                        Debug.LogError("在找到的对象上没有Image组件");
                        return;
                    }
                }
                else
                {
                    Debug.LogError("请在场景中创建正确的UI层级结构：Canvas/StoreUI/Treasures/StoreTreasure");
                    return;
                }
            }
        }
        else
        {
            treasureImage = treasureImageReference;
        }

        // 初始化时获取一个随机宝物
        GetRandomTreasure();
    }

    public void GetRandomTreasure()
    {
        if (randomComponent == null)
        {
            Debug.LogError("random组件不存在");
            return;
        }

        if (treasureImage == null)
        {
            Debug.LogError("treasureImage不存在");
            return;
        }

        currentTreasure = randomComponent.GetRandomTreasure();
        if (currentTreasure != null)
        {
            Debug.Log($"获取到宝物：{currentTreasure.Name}");
            // 加载并显示宝物图片
            Sprite treasureSprite = ResourceUtil.GetTreasureByName(currentTreasure.Icon);
            if (treasureSprite != null)
            {
                treasureImage.sprite = treasureSprite;
                Debug.Log($"设置宝物图片成功：{currentTreasure.Icon}");
            }
            else
            {
                Debug.LogWarning($"找不到宝物图片：{currentTreasure.Icon}");
            }
        }
        else
        {
            Debug.LogError("获取宝物失败");
        }
    }

    // 获取当前显示的宝物信息
    public TreasureClass GetCurrentTreasure()
    {
        return currentTreasure;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
