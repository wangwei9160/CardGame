using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardOpen : MonoBehaviour
{
    private void Start()
    {
        // 使用 DOTween 移动并放大 
        transform.DOLocalMove(new Vector2(0,0), 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        transform.DOScale(new Vector3(2, 2, 2), 1f).SetEase(Ease.OutQuad);
    }
}
