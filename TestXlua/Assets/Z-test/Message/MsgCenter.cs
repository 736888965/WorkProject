using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgCenter : MonoBase 
{ 
    public static MsgCenter instance;

    private void Awake()
    {
        instance = this;
        gameObject.AddComponent<UIManager>();
        DontDestroyOnLoad(gameObject);
    }
    public void Dispath(int areaCode, int eventCode, object message)
    {

    }
}
