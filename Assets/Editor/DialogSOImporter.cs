using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

public class DialogSOImporter : EditorWindow
{
    private DialogSO targetSO;
    private string csvPath = "";

    [MenuItem("Tools/Import DialogSO from CSV")]
    public static void ShowWindow()
    {
        GetWindow<DialogSOImporter>("DialogSO Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        targetSO = (DialogSO)EditorGUILayout.ObjectField("Target DialogSO", targetSO, typeof(DialogSO), false);

        EditorGUILayout.Space();
        GUILayout.Label("CSV File", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Path", csvPath);
        if (GUILayout.Button("Select CSV File"))
        {
            string path = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
            if (!string.IsNullOrEmpty(path))
                csvPath = path;
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Import"))
        {
            if (targetSO == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a target DialogSO asset.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(csvPath) || !File.Exists(csvPath))
            {
                EditorUtility.DisplayDialog("Error", "Please select a valid CSV file.", "OK");
                return;
            }

            ImportFromCSV(csvPath, targetSO);
        }
    }

    private void ImportFromCSV(string filePath, DialogSO so)
    {
        // 获取当前激活的场景
        // Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        // if (!currentScene.IsValid())
        // {
        //     EditorUtility.DisplayDialog("Error", "No active scene found. Please open a scene first.", "OK");
        //     return;
        // }

        // 收集当前场景中的所有 GameObject（包括未激活的）
        // GameObject[] allObjects = currentScene.GetRootGameObjects();
        // Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();
        // foreach (var root in allObjects)
        // {
        //     AddAllChildren(root, nameToObject);
        // }

        // 读取并解析 CSV（支持引号包裹的多行字段）
        List<string[]> rows = ReadCSVWithMultiline(filePath);
        if (rows.Count == 0)
        {
            Debug.LogWarning("CSV file is empty or could not be parsed.");
            return;
        }

        // 检查是否有标题行（第一行包含“场景”或“对话”关键字）
        bool hasHeader = rows[0].Length >= 3 &&
            (rows[0][0].Contains("角色") || rows[0][1].Contains("场景") || rows[0][2].Contains("对话"));
        int startIndex = hasHeader ? 1 : 0;

        List<DialogList> dialogList = new List<DialogList>();

        for (int i = startIndex; i < rows.Count; i++)
        {
            string[] cols = rows[i];
            if (cols.Length < 3)
            {
                Debug.LogWarning($"Row {i + 1}: skipped (less than 3 columns).");
                continue;
            }

            string sceneName = cols[1].Trim();      // 场景列
            string dialogText = cols[2].Trim();     // 对话列

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning($"Row {i + 1}: skipped (empty scene name).");
                continue;
            }

            // 按换行符分割对话文本
            string[] texts = dialogText.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < texts.Length; j++)
                texts[j] = texts[j].Trim();

            if (texts.Length == 0)
            {
                Debug.LogWarning($"Row {i + 1}: skipped (no valid text after splitting).");
                continue;
            }

            // 查找场景中的同名 GameObject
            string _sceneName=sceneName ;
            // if (nameToObject.TryGetValue(sceneName, out GameObject found))
            //     sceneObj = found;
            // else
            //     Debug.LogWarning($"Row {i + 1}: GameObject '{sceneName}' not found in current scene.");

            // DialogList dl = new DialogList
            // {
            //     texts = texts,
            //     scene = sceneObj
            // };
            DialogList dl = new DialogList(texts, sceneName);
            dialogList.Add(dl);
        }

        // 更新 ScriptableObject
        so.dialogs = dialogList.ToArray();
        EditorUtility.SetDirty(so);
        AssetDatabase.SaveAssets();
        Debug.Log($"Import completed. {dialogList.Count} entries added to {so.name}");
    }

    // 递归收集 GameObject 及其所有子物体
    private void AddAllChildren(GameObject obj, Dictionary<string, GameObject> dict)
    {
        if (!dict.ContainsKey(obj.name))
            dict.Add(obj.name, obj);
        foreach (Transform child in obj.transform)
            AddAllChildren(child.gameObject, dict);
    }

    // 读取 CSV 并正确解析引号包裹的多行字段
    private List<string[]> ReadCSVWithMultiline(string filePath)
    {
        List<string[]> rows = new List<string[]>();
        string allText = File.ReadAllText(filePath, Encoding.UTF8);

        List<string> currentRow = new List<string>();
        StringBuilder currentField = new StringBuilder();
        bool inQuotes = false;
        int i = 0;

        while (i < allText.Length)
        {
            char c = allText[i];

            if (c == '"')
            {
                // 处理双引号转义（"" 表示一个引号字符）
                if (i + 1 < allText.Length && allText[i + 1] == '"')
                {
                    currentField.Append('"');
                    i += 2;
                    continue;
                }
                inQuotes = !inQuotes;
                i++;
                continue;
            }

            if (c == ',' && !inQuotes)
            {
                currentRow.Add(currentField.ToString());
                currentField.Clear();
                i++;
                continue;
            }

            if ((c == '\n' || c == '\r') && !inQuotes)
            {
                if (c == '\r' && i + 1 < allText.Length && allText[i + 1] == '\n')
                    i++;
                currentRow.Add(currentField.ToString());
                currentField.Clear();
                rows.Add(currentRow.ToArray());
                currentRow.Clear();
                i++;
                continue;
            }

            currentField.Append(c);
            i++;
        }

        // 最后一行
        if (currentField.Length > 0 || currentRow.Count > 0)
        {
            currentRow.Add(currentField.ToString());
            rows.Add(currentRow.ToArray());
        }

        return rows;
    }
}