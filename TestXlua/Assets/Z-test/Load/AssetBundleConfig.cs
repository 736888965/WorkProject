using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleConfig 
{
    
}


public class ABConfig
{
    public string name { get; set; }
    public AssetBundle assetBundle { get; set; }
    public int index { get; set; }

    public ABConfig() { }
    public ABConfig (string name,AssetBundle assetBundle)
    {
        this.name = name;
        this.assetBundle = assetBundle;
        this.index = 1;
    }
}

[SerializeField]
public class ABBundle
{
    public string name { get; set; }

    public string path { get; set; }

    public string md5 { get; set; }

    public string type { get; set; }
}