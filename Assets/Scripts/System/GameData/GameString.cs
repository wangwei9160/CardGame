using UnityEngine;

public class GameString
{
    // UI
    public static string BATTLEUI = "BattleUI";
    public static string MERGEUI = "MergePanel";
    public static string SELECTCARDREWARD = "SelectCardRewardPanel";

    // Notice

    public static string STAGEINFO = "还剩{0}步走出{1}";
    public static string MAGICEINFO = "{0}/{1}";
    public static string TURNINFO = "第{0}回合";


    // Color
    //public static Color UNUSECOLOR = new Color(0.282353f, 0.282353f, 0.282353f);    // 不能使用的颜色
    public static Color NUNUMBERCOLOR = ColorUtil.HexToColor("413781");
    public static Color ONCLICKNUMBERCOLOR = ColorUtil.HexToColor("53074e");    // 点击后数字的颜色
    public static Color ONCLICKTEXTCOLOR = ColorUtil.HexToColor("081d40 ");    // 点击后数字的颜色

}

