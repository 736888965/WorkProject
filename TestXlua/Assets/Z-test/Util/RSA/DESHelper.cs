using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DESHelper : MonoBehaviour
{
    /// <summary>
    /// DES加密/解密
    /// </summary>
    /// <param name="data">加密/解密数据</param>
    /// <param name="key">秘钥</param>
    /// <param name="keyIV">向量</param>
    /// <param name="isEncrypt">true加密，false解密</param>
    /// <returns></returns>
    public static byte[] EncryptOrDecrypt(byte[] data, byte[] key, byte[] keyIV, bool isEncrypt)
    {
        DESCryptoServiceProvider desP = new DESCryptoServiceProvider();
        if (isEncrypt)// 加密
        {
            desP.Key = key;
            desP.IV = keyIV;
            ICryptoTransform desencrypt = desP.CreateEncryptor(key, keyIV);
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return result;
        }
        else // 解密
        {
            desP.Key = key;
            desP.IV = keyIV;
            ICryptoTransform desencrypt = desP.CreateDecryptor(key, keyIV);
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return result;
        }
    }

    /// <summary>
    /// 创建随机秘钥
    /// </summary>
    /// <returns></returns>
    public static byte[] CreateKey()
    {
        DESCryptoServiceProvider desP = new DESCryptoServiceProvider();
        desP.GenerateKey();
        return desP.Key;
    }

    /// <summary>
    /// 创建随机向量
    /// </summary>
    /// <returns></returns>
    public static byte[] CreateIV()
    {
        DESCryptoServiceProvider desP = new DESCryptoServiceProvider();
        desP.GenerateIV();
        return desP.IV;
    }
}
