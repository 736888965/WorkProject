using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class UIElement
{
    public string Name;
    public string Path;
    public string ComponentName;
    public UIElement(string name, string path, string componentName)
    {
        Name = name;
        Path = path;
        ComponentName = componentName;
    }

    public override string ToString()
    {
        string str = string.Format("Name={0} || Path={1} || ComponentName={2}", Name, Path, ComponentName);
        return str;
    }
}

public class UICodeGenerator
{
    [MenuItem("Assets/CreateCodeDeleteComponent")]
    public static void CreateCodeDeleteComponent()
    {
        GetPath(true);
    }

    [MenuItem("Assets/OnlyCreateCode")]
    public static void OnlyCreateCode()
    {
        GetPath(false);
    }

    public static void GetPath(bool isDeleteComponent)
    {
        var objs =
            Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
        GameObject obj = objs[0] as GameObject;
        elements = new List<UIElement>();
        GetPathAs(obj.transform, isDeleteComponent);

        foreach (var item in elements)
        {
            Debug.Log(item);
        }

        GeneratePane("Assets/" + obj.name + ".cs", obj.name, elements);
        GenerateCtrl("Assets/" + obj.name + "Ctrl.cs", obj.name, elements);

    }

    public static List<UIElement> elements;

    static void GetPathAs(Transform transform, bool isDeleteComponent)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<UIMark>())
            {
                elements.Add(new UIElement(child.name, GetPath(child),
                                               child.gameObject.GetComponent<UIMark>().ComponentName));
                if (isDeleteComponent)
                    GameObject.DestroyImmediate(child.gameObject.GetComponent<UIMark>(), true);
            }

            if (child.childCount != 0)
            {
                GetPathAs(child, isDeleteComponent);
            }
        }
    }


    public static void GeneratePane(string generateFilePath, string behaviourName, List<UIElement> elements)
    {
        var sw = new StreamWriter(generateFilePath, false, Encoding.UTF8);
        var strBuilder = new StringBuilder();

        strBuilder.AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine();
        strBuilder.AppendFormat("public class {0} : BasePanel ", behaviourName);
        strBuilder.AppendLine();
        strBuilder.AppendLine("{");
        foreach (var item in elements)
        {
            strBuilder.AppendLine("\tpublic " + item.ComponentName + " " + item.Name + " { get; private set; }");
        }

        strBuilder.AppendLine();
        strBuilder.AppendLine("\tpublic override void InitData()");
        strBuilder.AppendLine("\t{");
        foreach (var item in elements)
        {
            strBuilder.AppendFormat("\t\t{0} = transform.Find(\"{1}\").GetComponent<{2}>();", item.Name,
                                      item.Path.Replace(behaviourName + "/", ""), item.ComponentName);
            strBuilder.AppendLine();
        }

        strBuilder.AppendLine();
        strBuilder.AppendLine("\t\tuiType.uIFormParentType = UIFormParentType.PopUp;");
        strBuilder.AppendLine("\t\tuiType.uiFormShowMode = UIFormShowMode.Normal;");
        strBuilder.AppendLine("\t\tuiType.uiPanelType = UIPanelType.BoxPanel;");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");
        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void GenerateCtrl(string generateFilePath, string behaviourName, List<UIElement> elements)
    {
        var sw = new StreamWriter(generateFilePath, false, Encoding.UTF8);
        var strBuilder = new StringBuilder();

        List<UIElement> temp = new List<UIElement>();

        foreach (UIElement element in elements)
        {
            if (element.ComponentName.Equals("Button"))
                temp.Add(element);
        }

        strBuilder.AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine();
        strBuilder.AppendFormat("public class {0}Ctrl : BaseCtrl ", behaviourName);
        strBuilder.AppendLine("{");
        strBuilder.AppendLine();
        strBuilder.AppendFormat("\tprivate {0} panel;", behaviourName);
        strBuilder.AppendLine();
        strBuilder.AppendLine();
        strBuilder.AppendLine("\tpublic override void InitPanel()");
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tpanel = GetComponent<{0}>();", behaviourName);
        strBuilder.AppendLine();
        foreach (UIElement element in temp)
        {
            strBuilder.AppendFormat("\t\tpanel.{0}.AddListenerGracefully( {1}Click );", element.Name, element.Name);
            strBuilder.AppendLine();
        }
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine();
        foreach (UIElement element in temp)
        {
            strBuilder.AppendFormat("\tvoid {0}Click()", element.Name);
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t{");
            strBuilder.AppendLine();
            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine();
        }

        strBuilder.AppendLine("}");
        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static string GetPath(Transform transform)
    {
        var sb = new System.Text.StringBuilder();
        var t = transform;
        while (true)
        {
            sb.Insert(0, t.name);
            t = t.parent;
            if (t)
            {
                sb.Insert(0, "/");
            }
            else
            {
                return sb.ToString();
            }
        }
    }
}