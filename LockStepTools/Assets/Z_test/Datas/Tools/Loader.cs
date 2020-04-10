using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.IO;
using System;

namespace MokaData
{
    /// <summary>
    /// 加载数据
    /// </summary>
    public class Loader 
    {
        public static List<object> LoadData(string path,string typeName)
        {
            string ext = Path.GetExtension(path);
            List<List<string>> values = new List<List<string>>();
            switch (ext)
            {
                case ExtType.csv:
                    values = CSVHelper.Read(path);
                    break;
                case ExtType.excel:
                    values = ExcelHelper.Read(path);
                    break;
                default:
                    Debug.LogError("获取文件后缀错误" + path);
                    break;
            }
            if (values.Count == 0)
                return null;
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            // 获取Type
            Type[] types = assembly.GetTypes();
            Type t = assembly.GetType(typeName);
            PropertyInfo[] infos = t.GetProperties();


            List<string> infoList = new List<string>();
            string name = string.Empty;
            string type = string.Empty;
            List<Properties> proList = new List<Properties>();
            for (int i = 0; i < infos.Length; i++)
            {
                name = infos[i].Name;
                for (int k = 0; k < values[0].Count; k++)
                {
                    type = values[0][k];
                    if (string.Equals(type, name) || string.Equals(Utils.ToUppor(type), name))
                    {
                        Properties pro = new Properties(i, k);
                        proList.Add(pro);
                        continue;
                    }
                }
            }
            List<object> result = new List<object>();
            for (int i = 1; i < values.Count; i++)
            {
                List<string> list = values[i];
                object temp = Activator.CreateInstance(t);
                for (int k = 0; k < list.Count; k++)
                {
                    int res = Utils.GetProperties(k, proList);
                    if (res != -1)
                    {
                        //属性的名称
                        string infoName = infos[res].Name;
                        //MethodInfo[] member = t.GetMethods();
                       
                        MethodInfo meth = t.GetMethod("set_"+infoName);
                        if (meth == null)
                            continue;
                        PropertyInfo pro = t.GetProperty(infoName);
                        string infotype = pro.PropertyType.ToString();
                        bool infoB;
                        int infoI;
                        float infoF;
                        if (infotype.Contains("Boolean"))
                        {
                            bool.TryParse(list[k], out infoB);
                            meth.Invoke(temp, new object[] { infoB });
                        }
                        else if (infotype.Contains("Int32"))
                        {
                            int.TryParse(list[k], out infoI);
                            meth.Invoke(temp, new object[] { infoI });
                        }
                        else if (infotype.Contains("Single"))
                        {
                            float.TryParse(list[k], out infoF);
                            meth.Invoke(temp, new object[] { infoF });
                        }
                        else
                            meth.Invoke(temp, new object[] { list[k] });
                    }
                }
                result.Add(temp);
            }
            return result;

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<T> LoadData<T>(string path) where T :class,new()
        {
            string ext = Path.GetExtension(path);
            List<List<string>> values = new List<List<string>>();
            switch (ext)
            {
                case ExtType.csv:
                    values = CSVHelper.Read(path);
                    break;
                case ExtType.excel:
                    values = ExcelHelper.Read(path);
                    break;
                default:
                    Debug.LogError("获取文件后缀错误" + path);
                    break;
            }

            if (values.Count == 0)
                return null;
            PropertyInfo[] infos = typeof(T).GetProperties();
            List<string> infoList = new List<string>();
            string name = string.Empty;
            string type = string.Empty;
            List<Properties> proList = new List<Properties>();
            for (int i = 0; i < infos.Length; i++)
            {
                name = infos[i].Name;
                for (int k = 0; k < values[0].Count; k++)
                {
                    type = values[0][k];
                    if(string.Equals(type,name) || string.Equals(Utils.ToUppor(type),name))
                    {
                        Properties pro = new Properties(i, k);
                        proList.Add(pro);
                        continue;
                    }
                }
            }
            List<T> result = new List<T>();
            for (int i = 1; i < values.Count; i++)
            {
                List<string> list = values[i];
                T temp = new T();
                for (int k = 0; k < list.Count; k++)
                {
                    int res = Utils.GetProperties(k, proList);
                    if (res != -1)
                    {
                        //属性的名称
                        string infoName = infos[res].Name;
                        PropertyInfo pro = temp.GetType().GetProperty(infoName);
                        string infotype = pro.PropertyType.ToString();
                        bool infoB;
                        int infoI;
                        float infoF;
                        if (infotype.Contains("Boolean"))
                        {
                            bool.TryParse(list[k], out infoB);
                            pro.SetValue(temp, infoB);
                        }
                        else if (infotype.Contains("Int32"))
                        {
                            int.TryParse(list[k], out infoI);
                            pro.SetValue(temp, infoI);
                        }
                        else if (infotype.Contains("Single"))
                        {
                            float.TryParse(list[k], out infoF);
                            pro.SetValue(temp, infoF);
                        }
                        else
                            pro.SetValue(temp, list[k]);
                    }
                }
                result.Add(temp);
            }
            return result;
        }

    }
}
