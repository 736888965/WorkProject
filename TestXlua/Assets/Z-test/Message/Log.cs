using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    public static void LogColor(string message, string color = "white")
    {

#if !NO_LOG
        Debug.Log(string.Format(Application.installerName + ": <color={0}> {1} </color>", color, message));
#endif

    }

    public static void LogError(string messaga)
    {
#if !NO_LOG
        Debug.LogError(string.Format("{0} : {1}", Application.installerName, messaga));
#endif
    }
}
