using UnityEngine;

public class ResourceUtil
{
    public static string GetCardTypeName(int tp)
    {
        return EnumHelper.GetTypeName((CARD_TYPE)tp);
    }

    public static Sprite GetCardAttributeTypeImage(int tp)
    {
        string name = EnumHelper.GetAttributeName((CARD_ATTRIBUTE_TYPE)tp);
        return Resources.Load<Sprite>($"Arts/Card/Type/{name}");
    }

    public static Sprite GetWhiteCostImage(int cost)
    {
        return Resources.Load<Sprite>($"Arts/Cost/White/cost{cost}");
    }

    public static GameObject GetCard()
    {
        return Resources.Load<GameObject>("Arts/Card/UI/Card");
    }

    public static GameObject GetArtShow(string _name)
    {
        return Resources.Load<GameObject>("Arts/Show/" + _name);
    }

    public static GameObject GetUnitByPath(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public static GameObject GetCharacterByName(string name)
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

    public static Sprite GetCardByName(string name)
    {
        return Resources.Load<Sprite>("Arts/Card/icon/" + name);
    } 
}
