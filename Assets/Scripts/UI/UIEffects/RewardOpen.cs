using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RewardOpen : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.AfterEffectShowReward);
        });
        transform.DOScale(new Vector3(2, 2, 2), 1f).SetEase(Ease.OutQuad);
    }
}
