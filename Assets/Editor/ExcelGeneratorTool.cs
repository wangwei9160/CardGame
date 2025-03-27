using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using ExcelDataReader;
using System.Collections.Generic;
using System;
using System.Globalization;
using Unity.VisualScripting;

public class ExcelToClassGenerator
{
    [MenuItem("Tools/Excel/生成策划表")]
    public static void GenerateClassFromExcel()
    {
        // 选择 Excel 文件
        string excelPath = EditorUtility.OpenFilePanel("Select Excel File", "", "xlsx,xls");
        if (string.IsNullOrEmpty(excelPath))
        {
            Debug.LogWarning("No file selected.");
            return;
        }

        // 读取 Excel 文件
        using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // 使用 AsDataSet 方法
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true // 使用第一行作为表头
                    }
                });

                string name = Path.GetFileNameWithoutExtension(excelPath);
                var table = result.Tables[0]; // 读取第一个表格

                // 获取字段名（第一行）
                var fieldNames = new List<string>();
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    fieldNames.Add(table.Rows[0][col].ToString());
                }

                // 获取字段类型（第二行）
                var fieldTypes = new List<string>();
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    fieldTypes.Add(table.Rows[1][col].ToString());
                }

                // 找到以 $ 开头的行
                int startRow = -1;
                for (int row = 2; row < table.Rows.Count; row++)
                {
                    if (table.Rows[row][0].ToString().StartsWith("$"))
                    {
                        startRow = row + 1; // 从下一行开始读取数据
                        break;
                    }
                }

                if (startRow == -1)
                {
                    Debug.LogWarning("No data row starting with '$' found.");
                    return;
                }

                // 读取数据
                var dataList = new List<Dictionary<string, string>>();
                for (int row = startRow; row < table.Rows.Count; row++)
                {
                    var data = new Dictionary<string, string>();
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        var fieldName = fieldNames[col];
                        var fieldType = fieldTypes[col];
                        var fieldValue = table.Rows[row][col].ToString();
                        data[fieldName] = fieldValue;
                    }
                    dataList.Add(data);
                }

                // 生成 C# 文件
                name = ToCamelCaseClassName(name);
                string className = name + "Class";
                string managerName = name + "Manager";
                string scriptPath = Path.Combine(Application.dataPath, $"Scripts/Design/{name}.cs").Replace("\\", "/");
                string code = GenerateClassCode(className, fieldNames, fieldTypes, dataList, managerName);
                File.WriteAllText(scriptPath, code);
                Debug.Log($"Class generated at: {scriptPath} ");

                AssetDatabase.Refresh(); // 刷新 Unity 资源数据库
            }
        }
    }

    private static string GenerateClassCode(string className, List<string> fieldNames, List<string> fieldTypes, List<Dictionary<string, string>> dataList, string managerName)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine("[Serializable]");
        sb.AppendLine($"public class {className}");
        sb.AppendLine("{");

        // 生成字段
        for (int i = 0; i < fieldNames.Count; i++)
        {
            if (fieldTypes[i] == "list") // 如果字段类型是数组
            {
                sb.AppendLine($"    public List<int> {fieldNames[i]};");
            }
            else
            {
                sb.AppendLine($"    public {fieldTypes[i]} {fieldNames[i]};");
            }
        }

        sb.AppendLine("}");
        sb.AppendLine();

        // 生成 Manager 类
        sb.AppendLine($"public class {managerName}");
        sb.AppendLine("{");
        sb.AppendLine($"    public static Dictionary<{fieldTypes[0]}, {className}> m_Dic;");
        sb.AppendLine();
        sb.AppendLine($"    static {managerName}()");
        sb.AppendLine("    {");
        sb.AppendLine($"        m_Dic = new Dictionary<int, {className}>()");
        sb.AppendLine("        {");

        // 生成数据
        foreach (var data in dataList)
        {
            sb.Append("            {");
            sb.Append($"{data[fieldNames[0]]}, new {className}() {{ ");
            for (int i = 0; i < fieldNames.Count; i++)
            {
                var fieldName = fieldNames[i];
                var value = data[fieldName];
                if (fieldTypes[i] == "list")
                {
                    sb.Append($"{fieldName} = new List<int>() {{ {value} }}");
                }
                else if (fieldTypes[i] == "string")
                {
                    sb.Append($"{fieldName} = \"{value}\"");
                }
                else
                {
                    sb.Append($"{fieldName} = {value}");
                }
                if (i < fieldNames.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.AppendLine(" } },");
        }
        sb.AppendLine("        };");
        sb.AppendLine("    }");

        sb.AppendLine($"    public static {className} Get{className}ByKey({fieldTypes[0]} key)"); // fieldTypes[0] -> Type
        sb.AppendLine("    {");
        sb.AppendLine("        if(m_Dic.ContainsKey(key)) return m_Dic[key];");
        sb.AppendLine("        return null;");
        sb.AppendLine("    }");

        sb.AppendLine("}");

        return sb.ToString();
    }
    /// <summary>
    /// 将下划线分隔的字符串转换为驼峰命名并首字母大写
    /// </summary>
    /// <param name="input">输入的下划线分隔字符串</param>
    /// <returns>转换后的驼峰命名字符串</returns>
    public static string ToCamelCaseClassName(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // 将字符串转换为小写并分割为单词
        string[] words = input.Split('_');
        if (words.Length == 0)
            return input;

        // 首个单词首字母大写，其余单词首字母大写
        string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[0].ToLower());
        for (int i = 1; i < words.Length; i++)
        {
            result += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i].ToLower());
        }

        return result;
    }
}
