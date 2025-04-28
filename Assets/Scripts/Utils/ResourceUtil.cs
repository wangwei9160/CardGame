using UnityEngine;

public class ResourceUtil
{
    public static GameObject GetCard()
    {
        return Resources.Load<GameObject>("Arts/Card/UI/Card");
    }

    public static GameObject GetEnemy(string name)
    {
        return Resources.Load<GameObject>("Prefabs/Character/Character_" + name);
    }

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
    public static Sprite GetCardByID(int id)
    {
        Card card = CardFactory.GetCard(id);
        Test0Class cls = card.cardCfg;
        return Resources.Load<Sprite>("Arts/Card/icon/" + cls.Icon);
    }  
    public static Sprite GetCardByName(string name)
    {
        return Resources.Load<Sprite>("Arts/Card/icon/" + name);
    } 
}
