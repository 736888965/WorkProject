﻿using LockStep.Serialization;
using System;
using System.Text;

namespace LockStep.Game
{
    public interface IBackup : IDumpStr
    {
        void WriteBackup(Serializer writer);
        void ReadBackup(Deserializer reader);
    }

    public interface IHashCode
    {
        int GetHash(ref int idx);
    }

    public interface IDumpStr
    {
        void DumpStr(StringBuilder sb, string prefix);
    }

    public interface IAfterBackup
    {
        void OnAfterDeserialize(); //反序列化后重构
    }
}

