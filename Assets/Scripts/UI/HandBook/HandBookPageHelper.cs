using System.Collections.Generic;

public class HandBookPageHelper 
{
    public List<UiConfig> Configs;
    public HandBookPageHelper()
    {
        Configs = new List<UiConfig>();
        Configs.Add(new UiConfig("CardPage" , "UI/HandBook/CardPage"));
        Configs.Add(new UiConfig("ComponentPage", "UI/HandBook/ComponentPage"));
        Configs.Add(new UiConfig("TreasurePage", "UI/HandBook/TreasurePage"));
        Configs.Add(new UiConfig("EnemyPage", "UI/HandBook/EnemyPage"));
        Configs.Add(new UiConfig("GuidePage", "UI/HandBook/GuidePage"));
    }

    public UiConfig getCfg(int id)
    {
        return Configs[id];
    }
    public UiConfig getCfg(string name)
    {
        for(int i = 0; i < Configs.Count; i++)
        {
            if (Configs[i].Name == name)
            {
                return Configs[i];
            }
        }
        return null;
    }
}
