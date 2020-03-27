using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class RSAByte 
{
    [MenuItem("XLua/测试位移")]
    public static void RSA()
    {
        string path = Application.streamingAssetsPath + "/AB/";
        Util.GetAllFiles(path, out List<string> list);
        string name = string.Empty;
        byte byt;
        for (int i = 0; i < list.Count; i++)
        {
           name = Path.GetFileName(list[i]);
            Log.LogError("name ；" + name);
            byte[] byts =  File.ReadAllBytes(list[i]);
            byt = byts[byts.Length - 2];
            byts[byts.Length - 2] = byts[0];
            byts[0] = byt;
            byt = byts[byts.Length - 1];
            byts[byts.Length - 1] = byts[1];
            byts[1] = byt;
            File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, name), byts);
        }
        AssetDatabase.Refresh();
    }
}
