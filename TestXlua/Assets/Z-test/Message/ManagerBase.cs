using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase : MonoBase
{
    private Dictionary<int, List<MonoBase>> dics = new Dictionary<int, List<MonoBase>>();
    public override void Exectue(int eventCode, params object[] message)
    {
        if (dics.TryGetValue(eventCode, out List<MonoBase> list))
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Exectue(eventCode, message);
            }
        }
        else
            Log.LogColor("not bind eventcode  is " + eventCode, "red");
    }

    public void Add(int eventCode,MonoBase mono)
    {
        if(!dics.ContainsKey(eventCode))
            dics[eventCode] = new List<MonoBase>();
        dics[eventCode].Add(mono);
    }

    public void Add (int[] eventCodes,MonoBase mono)
    {
        for (int i = 0; i < eventCodes.Length; i++)
            Add(eventCodes[i], mono);
    }

    public void Remove(int eventCode,MonoBase mono)
    {
        if (dics.TryGetValue(eventCode, out List<MonoBase> list))
        {
            if (list.Contains(mono))
                list.Remove(mono);
            if (list.Count == 0)
                dics.Remove(eventCode);
        }
        else
            Log.LogColor("not remove event eventcode :" + eventCode);
    }
    public void Remove(int[] eventCodes,MonoBase mono)
    {
        for (int i = 0; i < eventCodes.Length; i++)
            Remove(eventCodes[i], mono);
    }
}
