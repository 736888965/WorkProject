using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ProtocolHelp 
{
    public static void Serializer<T>(this T t, string path) where T : IMessage
    {
        using (Stream input = File.OpenWrite(path))
        {
            t.WriteTo(input);
        }
    }

    public static T Deserializer<T>(string path) where T : IMessage, new()
    {
        using (Stream output = File.OpenRead(path))
        {
            T t = new T();
            t.MergeFrom(output);
            return t;
        }
    }
}
