using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : UIViewBase
{
    public EventClass eventClass;
    public DialogClass dialogClass;

    public Text dialogueText;
    public Button skipBtn;
    public GameObject[] effectBtns;

    #region 数据
    public bool isTyping;
    public string fullText;
    #endregion

    public override void OnAddlistening()
    {
        base.OnAddlistening();
        EventCenter.AddListener(EventDefine.OnMergePanelShow , CloseMe);
    }

    public override void OnRemovelistening()
    {
        base.OnRemovelistening();
        EventCenter.RemoveListener(EventDefine.OnMergePanelShow, CloseMe);
    }

    public void CloseMe()
    {
        UIManager.Instance.Close(Name);
    }

    protected override void Awake()
    {
        base.Awake();
        dialogueText = transform.Find("Dialogue/dialogueText").GetComponent<Text>();
        skipBtn = transform.Find("Dialogue/skipBtn").GetComponent<Button>();
        skipBtn.onClick.AddListener(ShowFullText);
        Transform tf = transform.Find("Dialogue/Buttons");
        effectBtns = new GameObject[tf.childCount];
        for (int i = 0; i < tf.childCount; i++)
        {
            effectBtns[i] = tf.GetChild(i).gameObject;
        }
    }

    public override void Init(string str, string data)
    {
        base.Init(str, data);
        eventClass = JsonUtility.FromJson<EventClass>(data);
        var dia = DialogConfig.GetDialogClassByKey(eventClass.id);
        SetData(JsonUtility.ToJson(dia));
    }

    public void SetData(string data)
    {
        dialogClass = JsonUtility.FromJson<DialogClass>(data);
        fullText = dialogClass.content;
        for (int i = 0; i < dialogClass.op_ids.Count; i++)
        {
            var effectClass = EffectConfig.GetEffectClassByKey(dialogClass.op_ids[i]);
            effectBtns[i].GetComponent<EffectSelectRender>().SetData(effectClass);
        }
        StartCoroutine(TypeText());
    }

    public float charsPerSecond = 20f;
    IEnumerator TypeText()
    {
        isTyping = true;
        for (int i = 0; i < fullText.Length; i++)
        {
            if (!isTyping) break;

            dialogueText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(1f / charsPerSecond);
        }
        isTyping = false;
    }
    
    public void ShowFullText()
    {
        if (isTyping)
        {
            StopCoroutine(TypeText());
            dialogueText.text = fullText;
            isTyping = false;
        }
    }
}
