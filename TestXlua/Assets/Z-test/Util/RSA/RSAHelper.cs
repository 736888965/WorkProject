using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class RSAHelper : MonoBehaviour
{
    /// <summary>
    /// 读取二进制文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] GetByte(string path)
    {
        FileInfo fi = new FileInfo(path);
        List<byte> buff = new List<byte>();
        using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            BinaryReader br = new BinaryReader(fs);
            try
            {
                while (true)
                {
                    byte i = br.ReadByte();
                    buff.Add(i);
                }
            }
            catch (Exception)
            {
                br.Close();
            }
        }
        return buff.ToArray();
    }

    /// <summary>
    /// 读取文本文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetText(string path)
    {
        FileInfo fi = new FileInfo(path);
        string content;
        using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            StreamReader sr = new StreamReader(fs);
            content = sr.ReadToEnd();
            sr.Close();
        }
        return content;
    }

    /// <summary>
    /// 写入二进制文件
    /// </summary>
    /// <param name="content"></param>
    /// <param name="path"></param>
    public static void WriteByte(byte[] content, string path)
    {
        FileInfo fi = new FileInfo(path);
        using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            BinaryWriter br = new BinaryWriter(fs);
            br.Write(content);
            br.Flush();
            br.Close();
        }
    }

    /// <summary>
    /// 写入文本文件
    /// </summary>
    /// <param name="content"></param>
    /// <param name="path"></param>
    public static void WriteText(string content, string path)
    {
        FileInfo fi = new FileInfo(path);
        using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Flush();
            sw.Close();
        }
    }

    /// <summary>
    /// RSA加密/解密
    /// </summary>
    /// <param name="data">加密/解密数据</param>
    /// <param name="key">公钥/私钥</param>
    /// <param name="isEncrypt">ture加密，false解密</param>
    /// <returns></returns>
    public static byte[] EncryptOrDecrypt(byte[] data, string key, bool isEncrypt)
    {
        RSACryptoServiceProvider rsaP = new RSACryptoServiceProvider();
        rsaP.FromXmlString(key);
        if (isEncrypt)// 加密
        {
            byte[] buff = rsaP.Encrypt(data, true);
            return buff;
        }
        else // 解密
        {
            byte[] buff = rsaP.Decrypt(data, true);
            return buff;
        }

    }
}
