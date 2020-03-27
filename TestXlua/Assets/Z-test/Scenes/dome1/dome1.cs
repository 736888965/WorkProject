using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class dome1 : MonoBehaviour
{
    public Transform cube;
    public Transform sphere;

    public Button bt1;

    public Button bt2;

    public TextAsset text;
    public static LuaEnv luaenv { get; set; }
    private void Start()
    {
        bt1.onClick.AddListener(OnClickOne);
        bt2.onClick.AddListener(OnClickTwo);
        luaenv = new LuaEnv();
        //StartCoroutine(Load());
        //luaenv.DoString(text.text);
        //luaenv.Dispose();
        
    }


    private void OnGUI()
    {
        if(GUILayout.Button("",GUILayout.Height(50),GUILayout.Width(100)))
        {
            ResourceLoad.LoadAB<TextAsset>("gloab.lua.unity3d", (text) =>
             {
                 Log.LogColor("加载文件" + text.ToString(),"red");
                 luaenv.DoString(text.text);
             });
        }
        if (GUILayout.Button("", GUILayout.Height(50), GUILayout.Width(100)))
        {
            ResourceLoad.LoadAB<TextAsset>("assetbundload.lua.unity3d", (text) =>
            {
                Log.LogColor("加载文件" + text.ToString(), "red");
                luaenv.DoString(text.text);
            });
        }
    }



    IEnumerator Load()
    {
        //WWW www = new WWW("file:///" + Application.dataPath + "/AssetBundle/ABs/load.lua.unity3d");
        //yield return www;
        //AssetBundle ab = www.assetBundle;
        //text = ab.LoadAsset<TextAsset>("load.lua");
        //luaenv.DoString(text.text);
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(Application.dataPath + "/AssetBundle/ABs/load.lua.unity3d");
        yield return req.SendWebRequest();
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError(req.error);
        }
        AssetBundle ab = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        text = ab.LoadAsset<TextAsset>("load.lua");
        luaenv.DoString(text.text);

    }
    [LuaCallCSharp]
    private void OnClickOne()
    {
        print("C# on click one");
    }

    private void OnClickTwo()
    {
        print("C# on click two");
        //ResourceLoad.LoadAB<TextAsset>("testhotfix.lua.unity3d", (text) =>
        //{
        //    Log.LogColor(text.ToString());
        //    luaenv.DoString(text.text);
        //});
    }
}
