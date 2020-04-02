using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class BuildAssetBundle
{
    [MenuItem("XLua/PackageAb")]
    static void PackageAb()
    {
        UnityEngine.Object obj = Selection.activeObject;
        if(obj!=null)
        {
            string file = AssetDatabase.GetAssetOrScenePath(obj);
            Debug.Log(file);
            if(file.EndsWith(".lua.txt"))
            {
                AssetImporter asset = AssetImporter.GetAtPath(file);
                asset.assetBundleName = obj.name; //设置Bundle文件的名称    
                asset.assetBundleVariant = "unity3d";//设置Bundle文件的扩展名    
                asset.SaveAndReimport();
                AssetDatabase.Refresh();
                BuildPipeline.BuildAssetBundles("Assets/AssetBundle/ABs", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
            }
        }
    }

    [MenuItem("XLua/PackageLua")]
    static  void PackageLua()
    {
        UnityEngine.Object obj = Selection.activeObject;
        if(obj!=null)
        {
            List<ABBundle> list = new List<ABBundle>();
            string objPath = AssetDatabase.GetAssetOrScenePath(obj);
            //string[] files =Directory.GetDirectories
            Util.GetAllFiles(objPath ,out List<string> filePath);
            string file = string.Empty;
            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
            for (int i = 0; i < filePath.Count; i++)
            {
                file = filePath[i];
                //if(file.EndsWith(".lua"))
                //{
                string name = Path.GetFileNameWithoutExtension(file);
                Debug.Log("name " + name);
                AssetBundleBuild ab = new AssetBundleBuild();
                ab.assetBundleName = name;
                ab.assetBundleVariant = "unity3d";
                string namePath = file.Replace("\\", "/").Replace(Application.dataPath, "Assets");
                Debug.Log("namePath : " + namePath);
                ab.assetNames = new string[] { namePath };
                builds.Add(ab);
                ABBundle bundle = new ABBundle();
                bundle.name = name;
                bundle.path = namePath;
                bundle.type = "TextAsset";
                bundle.md5 = Util.GenerateMD5Path(file.Replace("\\", "/"));
                list.Add(bundle);
                //}
            }
            Debug.LogError("namePath : " + builds.Count);
            if (Directory.Exists("Assets/AssetBundle/ABlua"))
                Directory.Delete("Assets/AssetBundle/ABlua",true);
            Directory.CreateDirectory("Assets/AssetBundle/ABlua");
            AssetDatabase.Refresh();
            BuildPipeline.BuildAssetBundles("Assets/AssetBundle/ABlua", builds.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();

            //生成配置文件
            string config = LitJson.JsonMapper.ToJson(list);
            Log.LogColor(config);
            string configPath = "Assets/AssetBundle/ABlua/config.md";
            File.WriteAllText(configPath, config);
            AssetDatabase.Refresh();

        }
    }


    public static string key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCFXFc8SpwejFHANFfjOzBy63eh"
                                + "ZV7bLoPKG2xkzSXqk8Sgj4GkWph5oF8cKbjXLQEOAf8v+anv1U6melrx/igRi+jS"
                                + "kKFk3qwtJUOKd6uCCx3S5HQjgeUcs8lftNIonbXRf721qHrbRq9SrscHtjEiymH/"
                                + "7q482g9MvkJc/tHRIQIDAQAB";
    public static string key1 = "<RSAKeyValue><Modulus>2VZheWHGp463NfKiYrZ3ohkqeR9vdFm+Ezakwg5iKTXKjb7kEcvIfidnSyLpGTUoRvEMkA6AStqO0k9cjreecDieZXnYCpN6IiY3495wf/OFnuW6ZrBdevvGQHsVB0Erlc7Dz8W2SCZIkthNUtjUw2lIhPGfs+GbkTm2ib/9p8E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    [MenuItem("XLua/加密")]
    public static void JiaMi()
    {
        Util.GetAllFiles("Assets/AssetBundle/ABlua", out List<string> list);
        for (int i = 0; i < list.Count; i++)
        {
            byte[] bys = Encoding.UTF8.GetBytes(File.ReadAllText(list[i]));
            RSACryptoServiceProvider oRSA1 = new RSACryptoServiceProvider();
            oRSA1.FromXmlString(key1);
            byte[] AOutput = oRSA1.Encrypt(bys, false);
            File.WriteAllBytes(Application.streamingAssetsPath + list[i].Replace(Application.dataPath + "/Assets/AssetBundle/ABlua", ""), AOutput);
        }
        AssetDatabase.Refresh();

    }


}
