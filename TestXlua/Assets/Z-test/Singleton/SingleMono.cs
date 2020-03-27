using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SingleMono<T>:MonoBehaviour where T: SingleMono<T>
{
    private static T _instance;
    private static readonly object syslock = new object();
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syslock)
                {
                    GameObject go = GameObject.Find("SingleMono");
                    if (go == null)
                        go = new GameObject("SingleMono");
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}

public class TSingleton<T> where T : class
{
    public static T _instance;
    public static readonly object syslock = new object();
    public static readonly Type[] EmptyTypes = new Type[0];

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syslock)
                {
                    ConstructorInfo ci = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, EmptyTypes, null);
                    if (ci == null) { throw new InvalidOperationException("class must contain a private constructor"); }
                    _instance = (T)ci.Invoke(null);
                }
            }
            return _instance;
        }
    }
}
