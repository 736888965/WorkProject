using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using MokaData;

namespace MokaDataEditor
{
    public class DataBaseHelper : EditorWindow
    {

        #region   面板
        DataBaseHelper()
        {
            this.titleContent = new GUIContent("自动生成C#");
        }
        Rect wr;
        string loadPath;
        string savePath;
        static string namepace = "MokeDataBase";
        [MenuItem("Tool/自动生成C#脚本")]
        static void Laod()
        {
            namepace = string.IsNullOrEmpty(PlayerPrefs.GetString("namepace")) ?
               "MokeDataBase" : PlayerPrefs.GetString("namepace");
            Rect wr = new Rect(0, 0, 500, 500);
            DataBaseHelper window = (DataBaseHelper)EditorWindow.GetWindowWithRect(typeof(DataBaseHelper), wr, false);
            window.Show();
        }


        private void OnGUI()
        {
            GUILayout.BeginVertical();
            //绘制标题
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Helper C#");
            //绘制文本
            GUILayout.Space(50);
            GUILayout.EndVertical();
            GUI.skin.label.fontSize = 15;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            //GUILayout.Label("load path 加载路径 :");
            loadPath = EditorGUILayout.TextField("Load Path", loadPath);
            GUILayout.Space(10);
            if (GUILayout.Button("Browse", GUILayout.Width(100), GUILayout.Height(25)))
            {
                string path = Application.dataPath.Replace("/Assets", "");
                path = EditorUtility.SaveFolderPanel("加载Data数据", path, Application.dataPath);
                loadPath = path;
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.TextField("Save Path", savePath);
            if (GUILayout.Button("Browse", GUILayout.Width(70), GUILayout.Height(25)))
            {
                string path = Application.dataPath.Replace("/Assets", "");
                path = EditorUtility.SaveFolderPanel("保存Data数据", path, Application.dataPath);
                savePath = path;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            namepace = EditorGUILayout.TextField("namespace", namepace);

            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Space(50);
            if (GUILayout.Button("生成C#", GUILayout.Width(100), GUILayout.Height(25)))
            {
                PlayerPrefs.SetString("namepace", namepace);
                List<string> list = new List<string>();
                Utils.GetFiles(loadPath, ref list);
                for (int i = 0; i < list.Count; i++)
                    Write(list[i]);
                AssetDatabase.Refresh();
                Close();
            }
            GUILayout.EndHorizontal();
        }

        #endregion

        #region 文件操作
        void Write(string path)
        {
            string ext = Path.GetExtension(path);
            switch (ext)
            {
                case ExtType.csv:
                    CSVHelper.LoadData(path, savePath, namepace);
                    break;
                case ExtType.excel:
                    ExcelHelper.LoadData(path, savePath, namepace);
                    break;
                default:
                    Debug.LogError("获取文件后缀错误" + path);
                    break;
            }
        }

       
        #endregion

        #region  Clear
        [MenuItem("Tool/清除命名空间")]
        static void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
        #endregion


    }

}


