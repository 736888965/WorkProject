using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.Hotfix]
public static class ResourceLoad
{
    public static T Load<T>(this string name) where T : UnityEngine.Object
    {
        return Resources.Load<T>(name);
    }

    public static T LoadComponent<T>(this string name)where T :Component
    {
        GameObject obj = Resources.Load<GameObject>(name);
        if (obj != null)
            return obj.GetComponent<T>();
        return null;
    }

    public static void LoadAB<T>(string name ,Action<T> call= null)where T :UnityEngine.Object 
    {
        Log.LogColor("name ；" + name);
        if(PathUtil.GetAllPath(name ,out string path))
        {
            AssetBundleLoad.Instance.LoadAsynSingle<T>(name, path, call);
        }
    }
    public static void LoadLua(string text)
    {
        Log.LogColor("lua 加载 文件 " + text);
        XLua.LuaEnv lua = new XLua.LuaEnv();
        lua.DoString(text);
    }
   
}
