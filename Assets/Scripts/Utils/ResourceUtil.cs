using UnityEngine;

// 美术资源获取通用工具
public class ResourceUtil
{
    public static Sprite GetTreasureByID(int id)
    {
        TreasureBase treasure = TreasureFactory.GetTreasure(id);
        TreasureClass cls = treasure.treasureCfg;
        return Resources.Load<Sprite>("Arts/Treasures/" + cls.Icon);
    }
    public static Sprite GetTreasureByName(string name)
    {
        return Resources.Load<Sprite>("Arts/Treasures/" + name);
    }
}
