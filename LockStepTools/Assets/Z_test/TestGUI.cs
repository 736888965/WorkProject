using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using MokaData;
using System.IO;
using System;
using MokeDataBase;
using System.Reflection;

public class TestGUI : MonoBehaviour
{
    [Tooltip("这是速度")]
    public GameObject Y;
    private void OnGUI()
    {
       // if(GUILayout.Button("",GUILayout.Width(100),GUILayout.Height(50)))
        {
            //TextAsset text = Resources.Load<TextAsset>("1");
            //string str = text.text;
            //string[] str =  File.ReadAllLines(Application.dataPath + "/1.csv");
            //for (int i = 0; i < str.Length; i++)
            //{
            //    Debug.Log(str[i]);
            //}
            //List<Fall> list = CSVLoader.LoadData<Fall>("D:/Project/UnityProject/TestXlua/Data/fall.csv");
            //for (int i = 0; i < list.Count; i++)
            //{
            //    print($"第{i}个姓名{list[i].姓名 } 年龄 {list[i].年龄} 性别 {list[i].ToString()}");
            //}
        }
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
           
        }

        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            //Type type = Type.GetType("MokeDataBase.BoxData");
            //print(type.Name);
            //object obj = Activator.CreateInstance(type);
            ////object obj = type.Assembly.CreateInstance("MokeDataBase.BoxData");
            //gameObject.AddComponent((Type)obj);
            //print(typeof(BoxData).FullName);

            Assembly assembly = Assembly.Load("Assembly-CSharp");
            // 获取Type
            Type[] types = assembly.GetTypes();
            Type t = assembly.GetType("MokeDataBase.Box");  
            // 通过Type的FullName
            object obj = Activator.CreateInstance(t);
            print(obj == null);


            // 创建实例
            // var obj = assembly.CreateInstance("MokeDataBase.BoxData");
            //gameObject.AddComponent(t);

        }
    }

     string ToUppor(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        print(value.Length);
        string a = value.Substring(0, 1).ToUpper();
        print(a);
        string b = value.Substring(1, value.Length-1).ToLower();
        print(b);

       // value = value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length).ToLower();
        return value;
    }


    void Log(string err)
    {
        //SocketManager.Instance.errorLog = Log;
        //string data = "实现可能限制同时使用它的应用程序的数量";
        //string data1 = "实现可能限制同时使用它的应用程序的数量";
        //SocketManager.Instance.Set("127.0.0.1", 8885);
        //for (int i = 0; i < 1; i++)
        //{
        //    SocketManager.Instance.AddData(Encoding.UTF8.GetBytes(data));
        //    SocketManager.Instance.AddData(Encoding.UTF8.GetBytes(data1));
        //}
        //SocketManager.Instance.Connect();
        //print(err);
    }
}
