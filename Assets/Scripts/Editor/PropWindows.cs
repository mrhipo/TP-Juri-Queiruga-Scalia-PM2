using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class PropsWindow : EditorWindow
{

    PropType type;
    int maxWidth;

    [MenuItem("LevelEditor/Props")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PropsWindow));
    }


    void OnGUI()
    {
        type = (PropType)EditorGUILayout.EnumPopup("Filter:", type);
        var path = "Assets/Resources/" + type.ToString() + "/";

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

        List<GameObject> allGOs = new List<GameObject>();

        foreach (FileInfo fileInfo in fileInf)
        {
            string fullPath = fileInfo.FullName.Replace(@"\", "/");
            string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
            GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
            allGOs.Add(prefab);
        }


        if (allGOs.Any())
        {
            int i = 0;
            int rowNum;

            foreach (var prop in allGOs)
            {
                EditorGUILayout.BeginHorizontal();
                var preview = AssetPreview.GetAssetPreview(prop);
                if (GUILayout.Button(preview, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    Selection.activeObject = GameObject.Instantiate(prop);
                }
            }
        }
    }
}
