using UnityEngine;
using UnityEditor;
using System.IO;
using ExcelDataReader;
using System.Collections.Generic;
using System.Text;
using System;

public class ExcelToClassGeneratorWindow : EditorWindow
{
    private string excelPath = string.Empty; // Excel 文件路径
    private List<string[]> excelPreviewData = new List<string[]>(); // 存储 Excel 文件的前三行数据

    [MenuItem("Tools/Excel/Excel 工具")]
    public static void ShowWindow()
    {
        // 显示弹窗
        GetWindow<ExcelToClassGeneratorWindow>("Excel 工具");
    }

    private void OnGUI()
    {
        GUILayout.Label("Excel 文件设置", EditorStyles.boldLabel);

        // 显示文件路径
        GUILayout.Label($"当前文件: {excelPath}");

        // 选择文件按钮
        if (GUILayout.Button("选择 Excel 文件"))
        {
            excelPath = EditorUtility.OpenFilePanel("选择 Excel 文件", "", "xlsx,xls");
            if (!string.IsNullOrEmpty(excelPath))
            {
                LoadExcelPreview(excelPath); // 加载 Excel 文件的前三行
            }
        }

        // 显示 Excel 文件的前三行
        if (excelPreviewData.Count > 0)
        {
            GUILayout.Label("Excel 文件前三行预览:", EditorStyles.boldLabel);

            for (int i = 0; i < excelPreviewData.Count; i++)
            {
                GUILayout.BeginHorizontal();
                foreach (var cell in excelPreviewData[i])
                {
                    GUILayout.Label(cell, GUILayout.Width(100)); // 每个单元格宽度为 100
                }
                GUILayout.EndHorizontal();
            }
        }

        // 生成按钮
        if (GUILayout.Button("生成 C# 类"))
        {
            if (!string.IsNullOrEmpty(excelPath))
            {
                GenerateClassFromExcel(excelPath);
            }
            else
            {
                Debug.LogWarning("请先选择 Excel 文件。");
            }
        }
    }

    /// <summary>
    /// 加载 Excel 文件的前三行
    /// </summary>
    /// <param name="path">Excel 文件路径</param>
    private void LoadExcelPreview(string path)
    {
        excelPreviewData.Clear(); // 清空之前的数据

        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false 
                    }
                });

                var table = result.Tables[0]; // 读取第一个表格

                // 读取前三行
                for (int row = 0; row < 3 && row < table.Rows.Count; row++)
                {
                    var rowData = new List<string>();
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        rowData.Add(table.Rows[row][col].ToString());
                    }
                    excelPreviewData.Add(rowData.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// 生成 C# 类
    /// </summary>
    /// <param name="path">Excel 文件路径</param>
    private void GenerateClassFromExcel(string path)
    {
        // 这里可以调用之前的 GenerateClassFromExcel 方法
        Debug.Log($"生成 C# 类: {path}");
        // 你可以将之前的 GenerateClassFromExcel 方法逻辑放到这里
    }
}