using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UiConfig
{
    public string Name;         // name
    public UILAYER layer;       // 层次
    public string UiPath;       // ui prefab 路径
    public string BindScene;    // 绑定的场景 -- prefab
    public UiConfig() { }

    public UiConfig(string name,UILAYER lay,string uipath , string bindScene)
    {
        Name = name;
        layer = lay;
        UiPath = uipath;
        BindScene = bindScene;
    }
}

public class UIConfigManager
{
    public static Dictionary<string, UiConfig> Configs = new Dictionary<string, UiConfig>
    {
        {"BattleUI" , new UiConfig("BattleUI" , UILAYER.M_NORMAL_LAYER ,"UI/BattleUI" ,"Prefabs/EchoEvent/FightEvent")},
        {"PlayerTurnTip" , new UiConfig("PlayerTurnTip" , UILAYER.M_BATTLE_LAYER , "UI/PlayerTurnTip" , "")},
        {"TopInfo" , new UiConfig("TopInfo" , UILAYER.M_BATTLE_LAYER , "UI/TopInfo" , "") },
        { "RewardPanelUI", new UiConfig("RewardPanelUI", UILAYER.M_BATTLE_LAYER, "UI/RewardPanelUI", "") },
        { "MergePanel", new UiConfig("MergePanel", UILAYER.M_BATTLE_LAYER, "UI/MergePanel", "") },
        {"HpUI" ,new UiConfig("HpUI" , UILAYER.M_BATTLE_LAYER ,"UI/HpUI" , "") }

    };
}