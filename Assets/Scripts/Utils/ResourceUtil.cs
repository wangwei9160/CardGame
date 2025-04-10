using UnityEngine;

// ������Դ��ȡͨ�ù���
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
