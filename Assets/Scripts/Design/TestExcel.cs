using System;
using System.Collections.Generic;

[Serializable]
public class TestExcelClass
{
    public int id;
    public string str;
    public List<int> arrayInt;
    public List<string> arrayStr;
    public List<List<int>> arrArrInt;
    public List<List<string>> arrArrString;
}

public class TestExcelConfig
{
    public static Dictionary<int, TestExcelClass> m_Dic;

    static TestExcelConfig()
    {
        m_Dic = new Dictionary<int, TestExcelClass>()
        {
            {1, new TestExcelClass() { id = 1, str = "abc", arrayInt = new List<int>() { 1, 2, 3 }, arrayStr = new List<string>() { "a", "b", "c" }, arrArrInt = new List<List<int>>() { new List<int>() { 1 }, new List<int>() { 2, 2 }, new List<int>() { 3, 3, 3 }, new List<int>() { 0 }, new List<int>() { 4 } }, arrArrString = new List<List<string>>() { new List<string>() { "a", "b", "c" }, new List<string>() { "d", "e" }, new List<string>() { "ff" } } } },
        };
    }
    public static TestExcelClass GetTestExcelClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<TestExcelClass> GetAll()
    {
        List<TestExcelClass> ret = new List<TestExcelClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
