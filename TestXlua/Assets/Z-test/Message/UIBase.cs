using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBase
{
    private List<int> msgCode = new List<int>();

    protected void Bind(params int[] eventCodes)
    {
        msgCode.AddRange(eventCodes);
        UIManager.instance.Add(eventCodes, this);
    }
    protected void UnBind()
    {
        UIManager.instance.Remove(msgCode.ToArray(), this);
        msgCode.Clear();
    }
    public virtual void OnDestroy()
    {
        if (msgCode != null)
            UnBind();
    }

    public void Dispath(int areaCode, int eventCode, object message)
    {
        MsgCenter.instance.Dispath(areaCode, eventCode, message);
    }
}
