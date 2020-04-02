using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestGUI : MonoBehaviour
{

    public CanvasScaler canvas;
    public string date;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.AddComponent<UIManager>();
        // canvas.referenceResolution = new Vector2(Screen.width, Screen.height);
        //GetComponent<Text>().font = 
    }


    private void OnGUI()
    {
        if (GUILayout.Button("", GUILayout.Height(50), GUILayout.Width(100)))
        {
            ResourceLoad.LoadAB<TextAsset>("load.lua.unity3d", (text) =>
            {
                Log.LogColor(text.ToString());
            });
        }
        if (GUILayout.Button("", GUILayout.Height(50), GUILayout.Width(100)))
        {
            //ABBundle[] list = LitJson.JsonMapper.ToObject<ABBundle[]>(File.ReadAllText("Assets/AssetBundle/ABlua/config.md"));
            //Log.LogColor("数量 ： "+ list.Length);
            //Log.LogColor(list[0].md5);
            //GetKey();

            //byte[] byts=  Encryption(date);
            //File.WriteAllBytes("byt.byte", byts);
            Log.LogColor(Decrypt(File.ReadAllBytes("byt.byte")));
        }


    }
    void GetKey()
    {
        string PublicKey = string.Empty;
        string PrivateKey = string.Empty;
        RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
        PublicKey = rSACryptoServiceProvider.ToXmlString(false); // 获取公匙，用于加密
        PrivateKey = rSACryptoServiceProvider.ToXmlString(true); // 获取公匙和私匙，用于解密
        Log.LogColor("publickey ； " + PublicKey, "red");
        Log.LogColor("PrivateKey ； " + PublicKey, "red");
        using (StreamWriter streamWriter = new StreamWriter("PublicKey.xml"))
        {
            streamWriter.Write(rSACryptoServiceProvider.ToXmlString(false));// 将公匙保存到运行目录下的PublicKey
        }
        using (StreamWriter streamWriter = new StreamWriter("PrivateKey.xml"))
        {
            streamWriter.Write(rSACryptoServiceProvider.ToXmlString(true)); // 将公匙&私匙保存到运行目录下的PrivateKey
        }

    }
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="str">需要加密的明文</param>
    /// <returns></returns>
    private static byte[] Encryption(string str)
    {
        RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
        using (StreamReader streamReader = new StreamReader("PublicKey.xml")) // 读取运行目录下的PublicKey.xml
        {
            rSACryptoServiceProvider.FromXmlString(streamReader.ReadToEnd()); // 将公匙载入进RSA实例中
        }
        byte[] buffer = Encoding.UTF8.GetBytes(str); // 将明文转换为byte[]

        // 加密后的数据就是一个byte[] 数组,可以以 文件的形式保存 或 别的形式(网上很多教程,使用Base64进行编码化保存)
        byte[] EncryptBuffer = rSACryptoServiceProvider.Encrypt(buffer, false); // 进行加密

        //string EncryptBase64 = Convert.ToBase64String(EncryptBuffer); // 如果使用base64进行明文化，在解密时 需要再次将base64 转换为byte[]
        //Console.WriteLine(EncryptBase64);
        return EncryptBuffer;
    }

    private static string Decrypt(byte[] buffer)
    {
        RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
        using (StreamReader streamReader = new StreamReader("PrivateKey.xml")) // 读取运行目录下的PrivateKey.xml
        {
            rSACryptoServiceProvider.FromXmlString(streamReader.ReadToEnd()); // 将私匙载入进RSA实例中
        }
        // 解密后得到一个byte[] 数组
        byte[] DecryptBuffer = rSACryptoServiceProvider.Decrypt(buffer, false); // 进行解密
        string str = Encoding.UTF8.GetString(DecryptBuffer); // 将byte[]转换为明文

        return str;
    }

}
