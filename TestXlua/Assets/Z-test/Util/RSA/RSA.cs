using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class RSA : MonoBehaviour
{

    public string data;

    private void OnGUI()
    {
        if(GUILayout.Button("",GUILayout.Width(100),GUILayout.Height(50)))
        {
            Save();
        }
    }
    private void Save()
    {
        string rootPath = Application.streamingAssetsPath+"/RSA/";
        string RSAPath = Path.Combine(rootPath, "RSA");
        string encryptFilePath = Path.Combine(RSAPath, "加密文件.dll");
        string decryptFilePath = Path.Combine(RSAPath, "解密文件.txt");
        string publicKeyPath = Path.Combine(RSAPath, "RSA公钥.xml");
        string privateKeyPath = Path.Combine(RSAPath, "RSA私钥.xml");
        string DESKeyPath = Path.Combine(RSAPath, "经过RSA加密的DES秘钥.dll");
        string DESIVPath = Path.Combine(RSAPath, "经过RSA加密的DES向量.dll");
        if (Directory.Exists(RSAPath))
        {
            Directory.Delete(RSAPath, true);
        }
        Directory.CreateDirectory(RSAPath);

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string RSAPublic = rsa.ToXmlString(false);// RSA公钥
        string RSAPrivate = rsa.ToXmlString(true);// RSA私钥
        byte[] DESKey = DESHelper.CreateKey(); // DES秘钥
        byte[] DESIV = DESHelper.CreateIV(); // DES向量

        // DES加密输入内容
        byte[] enData = DESHelper.EncryptOrDecrypt(Encoding.Unicode.GetBytes(data), DESKey, DESIV, true);
        // 写入加密文件
        RSAHelper.WriteByte(enData, encryptFilePath);
        // 写入RSA公钥
        RSAHelper.WriteText(RSAPublic, publicKeyPath);
        // 写入RSA私钥
        RSAHelper.WriteText(RSAPrivate, privateKeyPath);
        // 写入经过RSA加密的DES秘钥
        RSAHelper.WriteByte(RSAHelper.EncryptOrDecrypt(DESKey, RSAPublic, true), DESKeyPath);
        // 写入经过RSA加密的DES向量
        RSAHelper.WriteByte(RSAHelper.EncryptOrDecrypt(DESIV, RSAPublic, true), DESIVPath);

        // 读取RSA私钥
        string privateKey = RSAHelper.GetText(privateKeyPath);
        // 读取DES秘钥并解密
        byte[] realDESKey = RSAHelper.EncryptOrDecrypt(RSAHelper.GetByte(DESKeyPath), privateKey, false);
        // 读取DES向量并解密
        byte[] realDESIV = RSAHelper.EncryptOrDecrypt(RSAHelper.GetByte(DESIVPath), privateKey, false);
        // 读取加密文件
        byte[] enData2 = RSAHelper.GetByte(encryptFilePath);
        // 解密文件
        byte[] deData = DESHelper.EncryptOrDecrypt(enData2, realDESKey, realDESIV, false);
        // 写入解密文件
        RSAHelper.WriteText(Encoding.Unicode.GetString(deData), decryptFilePath);


    }


}
