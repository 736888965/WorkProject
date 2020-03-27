using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathUtil
{
    public static bool GetAllPath(string file, out string outPath)
    {
        outPath = GetRuntimePath() + file;
        if (File.Exists(outPath))
            return true;
        else
        {
            if(Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.OSXEditor)
            {
                outPath = Application.streamingAssetsPath + "/" + file;
                if (File.Exists(outPath))
                    return true;
            }
        }
        return false;
    }

    public static string GetRuntimePath()
    {
        string path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor | RuntimePlatform.OSXEditor:
                path = Application.streamingAssetsPath + "/";
                break;
            case RuntimePlatform.Android:
                path = Application.persistentDataPath + "/";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.persistentDataPath + "/";
                break;
        }
        return path;
    }

    public static void PlatformPath(ref string outPath)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            outPath = "File://" + outPath;
        }
    }

   
}
