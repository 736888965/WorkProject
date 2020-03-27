using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AssetBundleLoad : SingleMono<AssetBundleLoad> 
{

    AssetBundleManifest mainfest = null;


    private List<ABConfig> dependenList = new List<ABConfig>();

    private List<ABConfig> mainbodyList = new List<ABConfig>();


    private void LoadDepend(string file)
    {
        string[] paths = mainfest.GetAllDependencies(file);
    }

    public void LoadAsynSingle<T>(string name, string path, Action<T> call = null) where T : UnityEngine.Object
    {
        StartCoroutine(LoadAsyn<T>(name, path, call));
    }
    /// <summary>
    /// 加载本体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="path"></param>
    /// <param name="call"></param>
    /// <returns></returns>
    private IEnumerator LoadAsyn<T>(string name, string path ,Action<T> call = null) where T : UnityEngine.Object
    {
        Log.LogColor("laodasyn ", "blue");
        ABConfig temp = null;
        for (int i = 0; i < mainbodyList.Count; i++)
        {

        }

        PathUtil.PlatformPath(ref path);
        //TODO 加载依赖
        #region 加载解密
        /*
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return req.SendWebRequest();
        if (req.isNetworkError || req.isHttpError)
        {
            Log.LogError("下载" +  req.error);
            yield break;
        }
        byte[] byts = req.downloadHandler.data;*/

        WWW www = new WWW(path);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Log.LogColor("出错 ; " + www.error);
            yield break;
        }
        byte[] byts = www.bytes;

        byte byt;
        byt = byts[byts.Length - 2];
        byts[byts.Length - 2] = byts[0];
        byts[0] = byt;
        byt = byts[byts.Length - 1];
        byts[byts.Length - 1] = byts[1];
        byts[1] = byt;
        #endregion

        AssetBundle ab = AssetBundle.LoadFromMemory(byts);
        //AssetBundle ab = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        T t = ab.LoadAsset<T>(name.Replace(".unity3d",""));
        call?.Invoke(t);
    }

    private IEnumerable LaodDependencies(string file)
    {
        ABConfig temp = null;
        for (int i = 0; i < dependenList.Count; i++)
        {
            temp = dependenList[i];
            if(string.Equals(temp.name,file))
            {
                dependenList.Remove(temp);
                temp.index++;
                dependenList.Add(temp);
                yield break;
            }
        }

       if(PathUtil.GetAllPath(file,out string path))
        {
            PathUtil.PlatformPath(ref path);
            UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(path);
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError)
            {
                Log.LogError(req.error);
                yield break;
            }
            AssetBundle ab = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            temp = new ABConfig(file, ab);
            dependenList.Add(temp);
        }

    }


}
