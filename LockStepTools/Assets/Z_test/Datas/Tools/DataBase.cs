using System.Collections.Generic;
using UnityEngine;

namespace MokeDataBase
{
    public abstract class DataBase : ScriptableObject
    {
        public abstract void Set(List<object> list);
    }
}