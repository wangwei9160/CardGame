using UnityEngine;
using UnityEngine.UI;

public class AdaptiveTextBackground : MonoBehaviour
{
    public Text text;
    public Image background;
    public Image leftEdge;
    public Image midEdge;
    public Image rightEdge;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
        background = transform.Find("BG").GetComponent<Image>();
        leftEdge = transform.Find("Left").GetComponent<Image>();
        midEdge = transform.Find("Mid").GetComponent<Image>();
        rightEdge = transform.Find("Right").GetComponent<Image>();
    }

    public void SetData(string msg)
    {
        text.text = "\"" + msg + "\"";
        AdjustBackgroundSize();
    }

    private const float INITIAL_MID_WIDTH = 384f; // midEdge的初始宽度
    private const float INITIAL_HEIGHT = 66f;     // 初始高度
    void AdjustBackgroundSize()
    {
        float textWidth = text.fontSize * text.text.Length;
        float preferredWidth = textWidth + 20 * 2; // 包含左右边距
        text.rectTransform.sizeDelta = new Vector2(textWidth, text.preferredHeight);

        background.rectTransform.sizeDelta = new Vector2(preferredWidth,INITIAL_HEIGHT);
        float widthExtension = preferredWidth - INITIAL_MID_WIDTH + 100;
        midEdge.rectTransform.sizeDelta = new Vector2(Mathf.Max(INITIAL_MID_WIDTH + widthExtension , 0f), INITIAL_HEIGHT);
        float Y = midEdge.rectTransform.sizeDelta.y;
        float RightX = midEdge.rectTransform.sizeDelta.x / 2; 
        float LeftX = -midEdge.rectTransform.sizeDelta.x / 2;

        leftEdge.rectTransform.localPosition = new Vector3(LeftX, leftEdge.rectTransform.localPosition.y, 0);
        rightEdge.rectTransform.localPosition = new Vector3(RightX, rightEdge.rectTransform.localPosition.y, 0);
    }
}