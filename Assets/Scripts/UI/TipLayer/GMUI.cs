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

    private Action pendingAction;

    protected override void Start()
    {
        base.Start();
        
        if (okBtn != null) okBtn.onClick.AddListener(SendGM);
        if (treasureBtn != null) treasureBtn.onClick.AddListener(SetTreasureCommand);
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
            default:
                Debug.LogWarning($"Unknown GM command: {parts[0]}");
                break;
        }
    }
}