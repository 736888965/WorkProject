using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelp 
{
    #region  获取子物体
    public static Transform GetName(this Transform tf,string name)
    {
        if (tf.name == name)
            return tf;
        for (int i = 0; i < tf.childCount; i++)
        {
           Transform temp = tf.GetChild(i).GetName(name);
            if (temp != null)
                return temp;
        }
        return null;
    }

    public static Transform GetName(this GameObject obj, string name)
    {
       return obj.transform.GetName(name);
    }

    public static T GetOrAddComponent<T>(this GameObject obj ,string name = null) where T : Component 
    {
        T t = default;
        if(name == null)
        {
            t = obj.GetComponent<T>();
            if(t==null)
                t = obj.AddComponent<T>();
        }
        else
        {
            Transform tf = obj.GetName(name);
            if(tf!=null)
            {
                t = tf.GetComponent<T>();
                if (t == null)
                    t = tf.gameObject.AddComponent<T>();
            }
        }
        return t;
    }
    public static T GetOrAddComponent<T>(this Transform obj, string name = null) where T : Component
    {
        T t = default;
        if (name == null)
        {
            t = obj.GetComponent<T>();
            if (t == null)
                t = obj.gameObject.AddComponent<T>();
        }
        else
        {
            Transform tf = obj.GetName(name);
            if (tf != null)
            {
                t = tf.GetComponent<T>();
                if (t == null)
                    t = tf.gameObject.AddComponent<T>();
            }
        }
        return t;
    }
    #endregion

    
}
