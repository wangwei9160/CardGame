using System;
using UnityEngine;
using UnityEngine.UI;

// GM
public class GMUI : UIViewBase
{
    public override UILAYER Layer => UILAYER.M_TIP_LAYER;
    public InputField inputText;
    public Button okBtn;
    public Button treasureBtn;
    public Button cardBtn;
    public Button delCardBtn;

    private Action pendingAction;

    protected override void Start()
    {
        base.Start();
        
        if (okBtn != null) okBtn.onClick.AddListener(SendGM);
        if (treasureBtn != null) treasureBtn.onClick.AddListener(SetTreasureCommand);
        if (cardBtn != null) cardBtn.onClick.AddListener(SetCardCommand);
        if (delCardBtn != null) delCardBtn.onClick.AddListener(SetDelCardCommand);
    }

    void Update()
    {
        pendingAction?.Invoke();
        pendingAction = null; // Clear after invocation
    }

    private void SetTreasureCommand()
    {
        pendingAction = () => inputText.text = "GetTreasure 1001";
    }

    private void SetCardCommand()
    {
        pendingAction = () => inputText.text = "GetCard 100001";
    }

    private void SetDelCardCommand()
    {
        pendingAction = () => inputText.text = "DeleteOneCard (移除一张手牌)";
    }

    public void SendGM()
    {
        if (string.IsNullOrEmpty(inputText.text))
        {
            Debug.LogWarning("GM command is empty!");
            return;
        }

        string command = inputText.text.Trim();
        ProcessGMCommand(command);
    }

    private void ProcessGMCommand(string command)
    {
        string[] parts = command.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 0) return;

        switch (parts[0])
        {
            case "GetTreasure":
                if (parts.Length > 1 && int.TryParse(parts[1], out int treasureId))
                {
                    // 为角色添加遗物: treasureId
                    EventCenter.Broadcast(EventDefine.ON_GET_TREASURE_BY_ID , treasureId);
                }
                break;
            case "GetCard" :
                if (parts.Length > 1 && int.TryParse(parts[1], out int cardId))
                {
                    // 为角色添加一张卡牌
                    BattleManager.Instance.GetHandCard();
                }
                break;
            case "DeleteOneCard":
                BattleManager.Instance.RemoveOneHandCard();
                break;
            default:
                Debug.LogWarning($"Unknown GM command: {parts[0]}");
                break;
        }
    }
}