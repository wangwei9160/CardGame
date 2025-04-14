using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Permissions;

// Excel文件信息类
public class ExcelFileInfo
{
    public string filePath;
    public string fileName;
    public bool generated;
};
public class ExcelFolderGeneratWindow : EditorWindow
{
    

    private string selectedFolderPath = "";
    private List<ExcelFileInfo> excelFiles = new List<ExcelFileInfo>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Excel/全量导入策划表")]
    public static void GenerateAllExcel()
    {
        GetWindow<ExcelFolderGeneratWindow>("策划表文件夹").minSize = new Vector2(300, 200);;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("策划表自动化生成" , EditorStyles.boldLabel);

        // EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("当前选中文件夹", selectedFolderPath);
        
        if (GUILayout.Button("选择文件夹", GUILayout.Width(100)))
        {
            // 打开文件夹选择对话框
            string path = EditorUtility.OpenFolderPanel("请选择策划表目录", "", "");
            if (!string.IsNullOrEmpty(path))
            {
                selectedFolderPath = path;
                ScanExcelFiles();
            }
        }
        
        // EditorGUILayout.EndHorizontal();
        
        // 文件列表部分
        if (excelFiles.Count > 0)
        {
            GUILayout.Space(10);
            GUILayout.Label("Excel 文件:", EditorStyles.boldLabel);
            
            // 表头
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("文件名", EditorStyles.boldLabel, GUILayout.Width(200));
            GUILayout.Label("单文件生成", EditorStyles.boldLabel, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
            
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            // 文件列表
            foreach (var fileInfo in excelFiles)
            {
                EditorGUILayout.BeginHorizontal();
                
                // 文件名
                EditorGUILayout.LabelField(fileInfo.fileName, GUILayout.Width(200));
                
                // 生成按钮
                if (GUILayout.Button(fileInfo.generated ? "重新生成" : "生成", GUILayout.Width(100)))
                {
                    ExcelToClassGenerator.ReadExcelFile(fileInfo.filePath);
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndScrollView();
            
            // 批量生成按钮
            if (GUILayout.Button("全量生成"))
            {
                ReadAllExcelFilesInFolder(selectedFolderPath);
            }
        }
        else if (!string.IsNullOrEmpty(selectedFolderPath))
        {
            EditorGUILayout.HelpBox("当前目录下没有Excel文件", MessageType.Info);
        }
        EditorGUILayout.EndVertical();
    }

    private void ScanExcelFiles()
    {
        excelFiles.Clear();
        
        if (string.IsNullOrEmpty(selectedFolderPath)) return;
        
        string[] files = Directory.GetFiles(selectedFolderPath)
            .Where(file => file.EndsWith(".xlsx") || file.EndsWith(".xls"))
            .ToArray();
        
        foreach (string filePath in files)
        {
            string name = Path.GetFileName(filePath).Replace(".xlsx" , "");
            excelFiles.Add(new ExcelFileInfo
            {
                filePath = filePath,
                fileName = Path.GetFileName(filePath),
                generated = CheckFileExist(name),
            });
        }
    }

    private bool CheckFileExist(string fileName)
    {
        string name = ExcelToClassGenerator.ToCamelCaseClassName(fileName);
        string currentDirectory = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Design\\";
        
        return FileExistsWithoutExtension(currentDirectory , name);
    }

    private bool FileExistsWithoutExtension(string directoryPath, string name)
    {
        var files = Directory.GetFiles(directoryPath);

        return files.Any(file => 
            Path.GetFileNameWithoutExtension(file).Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }

    private void ReadAllExcelFilesInFolder(string folderPath)
    {
        // 获取文件夹下所有.xlsx和.xls文件
        string[] excelFiles = Directory.GetFiles(folderPath)
            .Where(file => file.EndsWith(".xlsx") || file.EndsWith(".xls"))
            .ToArray();
        
        if (excelFiles.Length == 0)
        {
            Debug.LogWarning("No Excel files found in the selected folder.");
            return;
        }
        
        // 处理每个Excel文件
        foreach (string filePath in excelFiles)
        {
            try
            {
                Debug.Log($"Reading Excel file: {filePath}");
                ExcelToClassGenerator.ReadExcelFile(filePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to read Excel file {filePath}: {e.Message}");
            }
        }
        
        Debug.Log($"Finished reading {excelFiles.Length} Excel files.");
    }

}
