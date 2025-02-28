using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 屏幕分辨率调整后部分ui位置的修改
public class ScreenResolutionHandler : MonoBehaviour
{
    private Vector2 lastScreenSize;

    void Start()
    {
        // 初始化时记录当前的屏幕分辨率
        lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // 检查屏幕分辨率是否发生变化
        if (lastScreenSize.x != Screen.width || lastScreenSize.y != Screen.height)
        {
            // 分辨率发生变化，更新位置
            lastScreenSize = new Vector2(Screen.width, Screen.height);
            StartCoroutine(FAdjustPosition());
        }
    }

    IEnumerator FAdjustPosition()
    {
        // 部分ui需要等待其他物体位置修改改变后再同步新的位置
        yield return new WaitForSeconds(0.1f);
        EventCenter.Broadcast(EventDefine.AdjustPosition);
    }

}
