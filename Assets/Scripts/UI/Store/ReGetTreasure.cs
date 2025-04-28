using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReGetTreasure : MonoBehaviour
{
    private Button reGetButton;
    private GetTreasure_T treasureT;
    private GetTreasure_S treasureS;
    private GetTreasure_F treasureF;

    // Start is called before the first frame update
    void Start()
    {
        // 获取按钮组件
        reGetButton = GetComponent<Button>();
        if (reGetButton == null)
        {
            Debug.LogError("ReGetTreasure: 找不到Button组件");
            return;
        }

        // 查找三个GetTreasure组件
        GameObject storeUI = GameObject.Find("StoreUI/Store/Treasures");
        if (storeUI != null)
        {
            treasureT = storeUI.GetComponentInChildren<GetTreasure_T>();
            treasureS = storeUI.GetComponentInChildren<GetTreasure_S>();
            treasureF = storeUI.GetComponentInChildren<GetTreasure_F>();

            if (treasureT == null || treasureS == null || treasureF == null)
            {
                Debug.LogError("ReGetTreasure: 找不到一个或多个GetTreasure组件");
                return;
            }
        }
        else
        {
            Debug.LogError("ReGetTreasure: 找不到StoreUI对象");
            return;
        }

        // 添加按钮点击事件
        reGetButton.onClick.AddListener(ReGetAllTreasures);
    }

    private void ReGetAllTreasures()
    {
        // 重新获取三个宝物
        if (treasureT != null)
        {
            treasureT.GetRandomTreasure();
        }

        if (treasureS != null)
        {
            treasureS.GetRandomTreasure();
        }

        if (treasureF != null)
        {
            treasureF.GetRandomTreasure();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
