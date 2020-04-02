using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Util
{

    public static void GetAllFiles(string path, out List<string> filePath)
    {
        filePath = new List<string>();
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta") || files[i].Name.EndsWith(".cs"))
                {
                    continue;
                }
                //Debug.Log("Name:" + files[i].Name);
                //Debug.Log("FullName:" + files[i].FullName);
                //Debug.Log("DirectoryName:" + files[i].DirectoryName);
                filePath.Add(files[i].FullName);
            }
        }
    }
    public static string GenerateMD5Path(string path)
    {
        if (File.Exists(path))
        {
            return GenerateMD5(File.ReadAllText(path));
        }
        return string.Empty;
    }

    public static string GenerateMD5(string txt)
    {
        using (MD5 mi = MD5.Create())
        {
            byte[] buffer = Encoding.Default.GetBytes(txt);
            //开始加密
            byte[] newBuffer = mi.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < newBuffer.Length; i++)
            {
                sb.Append(newBuffer[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
