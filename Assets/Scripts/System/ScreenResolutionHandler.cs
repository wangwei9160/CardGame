using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ļ�ֱ��ʵ����󲿷�uiλ�õ��޸�
public class ScreenResolutionHandler : MonoBehaviour
{
    private Vector2 lastScreenSize;

    void Start()
    {
        // ��ʼ��ʱ��¼��ǰ����Ļ�ֱ���
        lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // �����Ļ�ֱ����Ƿ����仯
        if (lastScreenSize.x != Screen.width || lastScreenSize.y != Screen.height)
        {
            // �ֱ��ʷ����仯������λ��
            lastScreenSize = new Vector2(Screen.width, Screen.height);
            StartCoroutine(FAdjustPosition());
        }
    }

    IEnumerator FAdjustPosition()
    {
        // ����ui��Ҫ�ȴ���������λ���޸ĸı����ͬ���µ�λ��
        yield return new WaitForSeconds(0.1f);
        EventCenter.Broadcast(EventDefine.AdjustPosition);
    }

}
