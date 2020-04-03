using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionHelper 
{
    /// <summary>
    /// 创建对象实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullName">命名空间.类型名</param>
    /// <param name="assemblyName">程序集</param>
    /// <returns></returns>
    public static T CreateInstance<T>(string fullName, string assemblyName)
    {
        string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
        Type o = Type.GetType(path);//加载类型
        object obj = Activator.CreateInstance(o, true);//根据类型创建实例
        return (T)obj;//类型转换并返回
    }
}
