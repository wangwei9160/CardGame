public class UiInfo
{
    public string Name;
    public UILAYER layer;
    public string UiPath;

    public UiInfo(string name, string uiPath)
    {
        Name = name;
        UiPath = uiPath;
    }

    public UiInfo(string name, UILAYER layer, string uiPath)
    {
        Name = name;
        this.layer = layer;
        UiPath = uiPath;
    }
}