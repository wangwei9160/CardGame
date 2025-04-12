using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureTip : MonoBehaviour
{
    public Image BG;
    public Text Name;
    public Text description;


    public void Start()
    {
        BG = transform.Find("BG").GetComponent<Image>();
        Name = transform.Find("Name").GetComponent<Text>();
        description = transform.Find("Description").GetComponent<Text>();
        EventCenter.AddListener<Vector3 , int>(EventDefine.TREASURE_TIP_SHOW , Show);
        EventCenter.AddListener(EventDefine.TREASURE_TIP_HIDE, Hide);
        Hide();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector3 , int>(EventDefine.TREASURE_TIP_SHOW, Show);
        EventCenter.RemoveListener(EventDefine.TREASURE_TIP_HIDE, Hide);
    }

    public void Show(Vector3 pos ,int id)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        TreasureBase ts = TreasureFactory.GetTreasure(id);
        Name.text = ts.treasureCfg.Name;
        description.text = ts.Description(true);
        TextUtil.ProcessTextWrap(description , 10 * (description.fontSize + 1));
        StartCoroutine(AdjustBackgroundAfterLayout());
    }

    IEnumerator AdjustBackgroundAfterLayout()
    {
        yield return null;
        BG.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Max(200f, 10 + description.rectTransform.rect.width));
        BG.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30 + description.rectTransform.rect.height);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
