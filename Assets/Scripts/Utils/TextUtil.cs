using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Security.Cryptography;
using Unity.Mathematics;

public static class TextUtil
{
    public static void ProcessTextWrap(Text textCom , float _lineLength = 190f , float _defaultLine = 3f)
    {
        if (textCom == null) return;

        AdjustTextComWidth(textCom , _lineLength , _defaultLine);
        ProcessTextContent(textCom.text , textCom);
    }

    public static void AdjustTextComBySelf(Text textCom , float _maxLineLength , float _maxLineNum)
    {
        float maxCol = _maxLineLength * (textCom.fontSize + 1) ;
        float maxRow = _maxLineNum * (textCom.fontSize + 1);
        textCom.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxCol);
        textCom.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxRow);
    }

    public static void AdjustTextComWidth(Text textCom , float _lineLength = 190f , float _defaultLine = 3f)
    {
        string res = textCom.text;
        if (string.IsNullOrEmpty(res)) return;
        int maxLen = res.Length;
        float maxWidth = maxLen * (textCom.fontSize + 1);
        
        float preferredWidth = _lineLength;
        if (maxWidth >= _lineLength * _defaultLine)
        {
            preferredWidth = maxWidth / _defaultLine + textCom.fontSize - 1;
        }
        textCom.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
        float preferredHeight = Mathf.CeilToInt(maxWidth / preferredWidth) * (textCom.fontSize + 1) + (textCom.fontSize / 2);
        textCom.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
    }

    public static void ProcessTextContent(string input, Text textCom)
    {
        // 使用Unity的TextGenerator获取换行信息
        var generator = new TextGenerator();
        var generationSettings = textCom.GetGenerationSettings(textCom.rectTransform.rect.size);
        generator.Populate(input, generationSettings);

        // 获取所有行信息
        var lines = new System.Collections.Generic.List<string>();
        int currentLineStart = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (generator.lines.Count > lines.Count + 1 &&
                i >= generator.lines[lines.Count + 1].startCharIdx)
            {
                string line = input.Substring(currentLineStart, i - currentLineStart);
                lines.Add(line);
                currentLineStart = i;
            }
        }

        // 添加最后一行
        if (currentLineStart < input.Length)
        {
            lines.Add(input.Substring(currentLineStart));
        }

        // 处理每行的开头字符
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            // 检查行首是否是标点或数字
            while (i > 0 && line.Length > 0 &&
                ((char.IsPunctuation(line[0]) && line[0] != '(' && line[0] != '（') || isNotChinese(line[0])))
            {
                // 将问题字符移到上一行末尾
                lines[i - 1] += line[0];
                line = line.Length > 1 ? line.Substring(1) : "";

                // 如果移除了字符后当前行为空，则跳过
                if (string.IsNullOrEmpty(line)) continue;
            }
            lines[i] = line;
        }
        float _maxLineLength = 0f;
        float _maxLineNum = 0f;
        for(int i = 0 ; i < lines.Count ; i++){
            if(string.IsNullOrEmpty(lines[i])) continue;
            result.AppendLine(lines[i]);
            _maxLineLength = math.max(_maxLineLength , lines[i].Length);
            _maxLineNum += 1;
        }
        textCom.text = result.ToString().TrimEnd();
        AdjustTextComBySelf(textCom , _maxLineLength , _maxLineNum);
    }

    public static bool isNotChinese(char c)
    {
        if(char.IsDigit(c)) return true;
        if(c >= 'a' && c <= 'z') return true;
        if(c >= 'A' && c <= 'Z') return true;
        return false;
    }

}