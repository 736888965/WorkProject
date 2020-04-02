using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class DClass
{
    public int f1;
    public int f2;
}
[CSharpCallLua]
public interface ItfD
{
    int f1 { get; set; }
    int f2 { get; set; }
    int add(int a, int b);
}
[CSharpCallLua]
public delegate int FDelegate(int a, string b, out DClass c);
[CSharpCallLua]
public delegate Action GetE();



/// <summary>
/// 把lua 映射到 C#的类中
/// </summary>
public class CallLua : MonoBehaviour
{
    LuaEnv luaEnv;
    public TextAsset script;
    private void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString(script.text);
        print("_G.a = " + luaEnv.Global.Get<int>("a"));
        print("_G.b = " + luaEnv.Global.Get<string>("b"));
        print("_G.c = " + luaEnv.Global.Get<bool>("c"));

        DClass d = luaEnv.Global.Get<DClass>("d");
        print("_G.d = {f1 =" + d.f1 + " ,f2=" + d.f2 + "}");

        Dictionary<string, double> d1 = luaEnv.Global.Get<Dictionary<string, double>>("d");
        print("_G.d = {f1 =" + d1["f1"] + " ,f2=" + d1["f2"] + "  ,count " + d1.Count + " }");

        List<double> d2 = luaEnv.Global.Get<List<double>>("d");
        print("_G.d.len " + d2.Count);

        ItfD d3 = luaEnv.Global.Get<ItfD>("d");
        d3.f2 = 1000;
        print("_G.d = {f1 =" + d3.f1 + " ,f2=" + d3.f2 + "}");
        print("_G.d : add " + d3.add(d3.f1, d3.f2));

        LuaTable table = luaEnv.Global.Get<LuaTable>("d");
        Debug.Log("_G.d = {f1=" + table.Get<int>("f1") + ", f2=" + table.Get<int>("f2") + "}");

        Action e = luaEnv.Global.Get<Action>("e");//映射到一个delgate，要求delegate加到生成列表，否则返回null，建议用法
        e();

        FDelegate f = luaEnv.Global.Get<FDelegate>("f");
        DClass d_ret;
        // 多值返回，可用out接收多余的参数
        // 输出 a 100 b John
        int f_ret = f(100, "John", out d_ret);//lua的多返回值映射：从左往右映射到c#的输出参数，输出参数包括返回值，out参数，ref参数
                                              // table只含有常量f1,所以f2赋值为0
        Debug.Log("ret.d = {f1=" + d_ret.f1 + ", f2=" + d_ret.f2 + "}, ret=" + f_ret);

        GetE ret_e = luaEnv.Global.Get<GetE>("ret_e");//delegate可以返回更复杂的类型，甚至是另外一个delegate
        e = ret_e();
        e();


        // 映射到 LuaFunction
        LuaFunction d_e = luaEnv.Global.Get<LuaFunction>("e");
        // Call 函数可以传任意类型，任意个数的参数
        d_e.Call();

    }

    private void OnDisable()
    {
        luaEnv.Dispose();
    }
}

