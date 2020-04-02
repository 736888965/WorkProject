using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShowLog
{
    private static bool isShow = true;
    private static string showLog = "NO_LOG";

    public static void Judege(bool isShow)
    {
        BuildTargetGroup buildTarget;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
                buildTarget = BuildTargetGroup.Standalone;
                break;
            case RuntimePlatform.Android:
                buildTarget = BuildTargetGroup.Android;
                break;
            case RuntimePlatform.IPhonePlayer:
                buildTarget = BuildTargetGroup.iOS;
                break;
            default:
                buildTarget = BuildTargetGroup.Standalone;
                break;
        }
        string values = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
        Log.LogColor("before : " + values, "red");
        if (isShow)
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, values + ";" + showLog);
        else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, values.Replace(showLog, ""));

        Log.LogColor("after  :" + PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget), "red");
    }

#if NO_LOG
    [MenuItem("Tools/IsShowLog/CloseLog(正式)")]
    public static void Close()
    {
        Judege(false);
    }
#else

    [MenuItem("Tools/IsShowLog/OpenLog(测试)")]
    public static void Open()
    {
        Judege(true);
    }
#endif
}
