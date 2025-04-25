using System;
using UnityEngine;

public class ColorUtil
{
    static float ByteToFloat(string hex)
    {
        return (float)(Convert.ToUInt32(hex, 16)) / 255f; // 将十六进制字符串转换为UInt32,然后除以255得到0到1之间的浮点数
    }

    public static Color HexToColor(string hexColor)
    {
        // 移除字符串开头的'#'字符（如果有的话）
        hexColor = hexColor.Replace("#", "");
        // 将十六进制字符串转换为RGB值,并转换为0到1之间的浮点数
        float red = ByteToFloat(hexColor.Substring(0, 2));
        float green = ByteToFloat(hexColor.Substring(2, 2));
        float blue = ByteToFloat(hexColor.Substring(4, 2));
        return new Color(red, green, blue);
    }
}