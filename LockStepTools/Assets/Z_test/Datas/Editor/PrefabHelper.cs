using MokaData;
using MokeDataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace MokaDataEditor
{
    public class PrefabHelper : EditorWindow
    {
        PrefabHelper()
        {
            this.titleContent = new GUIContent("生成peraber");
        }
        string loadPath;
        bool saveParfab = false;
        [MenuItem("Tool/生成perfab")]
        static void SavePerfab()
        {
            Rect wr = new Rect(0, 0, 500, 500);
            PrefabHelper window = (PrefabHelper)EditorWindow.GetWindowWithRect(typeof(PrefabHelper), wr, false);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            //绘制标题
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Prefab  Helper");
            //绘制文本
            GUILayout.EndVertical();
            GUILayout.Space(50);

            GUI.skin.label.fontSize = 15;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.TextField("选择数据文件夹", loadPath);
            GUILayout.Space(10);
            if (GUILayout.Button("Browse", GUILayout.Width(100), GUILayout.Height(25)))
            {
                string path = Application.dataPath.Replace("/Assets", "");
                path = EditorUtility.SaveFolderPanel("保存Data数据", path, Application.dataPath);
                //if (Path.GetExtension(path) != )
                //{
                //    Debug.LogError("选择数据文件错误" + path);
                //    loadPath = null;
                //    return;
                //}
                loadPath = path;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            saveParfab = GUILayout.Toggle(saveParfab, "保存Prefab");
            GUILayout.EndHorizontal();
            GUILayout.Space(30);
            if (GUILayout.Button("生成Perfab", GUILayout.Width(100), GUILayout.Height(25)))
            {
                if (string.IsNullOrEmpty(loadPath))
                    Debug.LogError("加载路径错误");
                List<string> fileList = new List<string>();
                Utils.GetFiles(loadPath, ref fileList);
                string className = string.Empty;
                for (int i = 0; i < fileList.Count; i++)
                {
                    className = Path.GetFileNameWithoutExtension(fileList[i]);
                    string typeName =PlayerPrefs.GetString("namepace")+"." +Utils.ToUppor(className) + ScriptType.data;

                    Assembly assembly = Assembly.Load("Assembly-CSharp");
                    // 获取Type
                    Type[] types = assembly.GetTypes();
                    Type t = assembly.GetType(typeName);
                    List<object> list = Loader.LoadData(fileList[i], PlayerPrefs.GetString("namepace") + "." + Utils.ToUppor(className));
                    Debug.Log("className :" + className + list.Count);
                    CreateAsset(t, Utils.ToUppor(className),list);
                }
                Close();
            }

        }

        void CreatePrefab(Type type ,string goName ,List<object> list)
        {
            GameObject obj = new GameObject(goName);
            obj.AddComponent(type);
            Component com = obj.GetComponent(type);

            PropertyInfo[] pro = com.GetType().GetProperties(BindingFlags.Public);
            MethodInfo[] infos = com.GetType().GetMethods();
            MethodInfo member = com.GetType().GetMethod("Set");
            if (member == null)
            {
                Debug.LogError("获取setvalue 错误" + goName);
                GameObject.DestroyImmediate(obj);
                return;
            }
            object temp = Activator.CreateInstance(com.GetType());
            //member.Invoke(temp, new object[] { list });

           

            string path = $"Assets/Z_test/{goName}.prefab";

            //参数1 创建路径，参数2 需要创建的对象， 如果路径下已经存在该名字的prefab，则覆盖
            //PrefabUtility.CreatePrefab(path, obj);
            PrefabUtility.SaveAsPrefabAsset(obj, path);
            GameObject.DestroyImmediate(obj);
            AssetDatabase.Refresh();


           
        }

        void CreateAsset(Type type, string goName, List<object> list)
        {
            ScriptableObject scripts = ScriptableObject.CreateInstance(type.ToString());
            DataBase data = (DataBase)scripts;
            data.Set(list);
            AssetDatabase.CreateAsset(data, $"Assets/Z_test/{goName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        static void Creat()
        {
           // ScriptableObject scripts = ScriptableObject.CreateInstance<DataBase>();
        }


    }

}
