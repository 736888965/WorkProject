using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace MokaData
{
    public class ExtType
    {
        public const string csv = ".csv";
        public const string excel = ".xlsx";

    }

    public class ScriptType
    {
        /// <summary>
        /// 源文件
        /// </summary>
        public const string source = "Source";
        /// <summary>
        /// 
        /// </summary>
        public const string sourceData = "SourceData";


        public const string data = "Data";

        /// <summary>
        /// 继承的父类
        /// </summary>
        public const string inMono = "DataBase";
    }
    

    public class ScriptFunName
    {
        public const string Get = "Get";
        public const string Set = "Set";
    }

    public class Properties
    {
        public int netID { get; set; }
        public int nativeID { get; set; }
        public Properties() { }
        public Properties(int net,int native)
        {
            this.netID = net;
            this.nativeID = native;
        }
    }


    public class Utils
    {
        /// <summary>
        /// 自动创建C#脚本
        /// </summary>
        /// <param name="className">脚本名</param>
        /// <param name="save">保存路径</param>
        /// <param name="namepace">命名空间</param>
        /// <param name="lists">内容</param>
        public static void Write(string className, string save,string namepace, List<List<string>> lists)
        {
            if (lists.Count <= 2)
            {
                Debug.LogError("读取错误");
                return;
            }
            #region  源文件
            List<string> typeList = lists[1];
            StringBuilder builder = new StringBuilder();
            builder.Append($"using System;\nusing UnityEngine;\n");
            builder.Append($"namespace {namepace} \n{{\n");
            builder.Append($"\t[Serializable]\n");
            builder.Append($"\tpublic class {ToUppor(className)}\n\t{{\n");

            for (int i = 0; i < typeList.Count; i++)
            {
                string typeName = GetType(typeList[i]);
                builder.Append($"\t\t[SerializeField]\n");
                builder.Append($"\t\tprivate {typeName} {(lists[0][i]).ToLower()};\n");
                builder.Append($"\t\tpublic {typeName} {ToUppor(lists[0][i])}");
                builder.Append($" {{ get {{ return  {(lists[0][i]).ToLower()}; }}  set {{ { (lists[0][i]).ToLower()} = value ; }} }} \n");
            }
            builder.Append("\t}\n}");
            string saveC = $"{save}/{ScriptType.source}/{ToUppor(className)}.cs";
            string dir = Path.GetDirectoryName(saveC);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (File.Exists(saveC))
                File.Delete(saveC);
            File.WriteAllText(saveC, builder.ToString());
            #endregion

            #region  源集合文件
            builder.Clear();
            builder.Append($"using System;\n");
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using UnityEngine;\n");
            builder.Append($"namespace {namepace} \n{{\n");
            builder.Append($"\t[Serializable]\n");
            builder.Append($"\tpublic class {ToUppor(className)}Data  : {ScriptType.inMono}\n\t{{\n");
            builder.Append($"\t\tpublic {ToUppor(className)}Data ()\n");
            builder.Append($"\t\t{{\n");
            builder.Append($"\t\t\t{className.ToLower()}List = new List<{ToUppor(className)}>();\n");
            builder.Append($"\t\t}}\n");
            builder.Append($"\t\t [SerializeField]\n");
            builder.Append($"\t\tprivate List<{ToUppor(className)}> {className.ToLower()}List ;\n");
            builder.Append($"\t\tpublic List<{ToUppor(className)}> {ScriptFunName.Get}() \n\t\t{{\n");
            builder.Append($"\t\t\treturn  {className.ToLower()}List;\n");
            builder.Append($"\t\t}}\n");
            builder.Append($"\t\tpublic override void {ScriptFunName.Set} (List<object> list)\n\t\t{{\n");
            //builder.Append($"\t\t\t{className.ToLower()}List = list.ConvertAll(c => {{ return ({ToUppor(className)})c; }});\n");
            builder.Append($"\t\t\t{className.ToLower()}List.Clear();\n");
            builder.Append($"\t\t\tfor (int i = 0; i < list.Count; i++)\n");
            builder.Append($"\t\t\t\t {className.ToLower()}List.Add(({ToUppor(className)})list[i]);\n");
            builder.Append("\t\t}\n");
            builder.Append("\t}\n}");

            saveC = $"{save}/{ScriptType.sourceData}/{ToUppor(className)}{ScriptType.data}.cs";
            dir = Path.GetDirectoryName(saveC);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (File.Exists(saveC))
                File.Delete(saveC);
            File.WriteAllText(saveC, builder.ToString());
            #endregion
        }

        static string GetType(string typeName)
        {
            if (bool.TryParse(typeName, out bool tempB))
                return "bool";
            else if (int.TryParse(typeName, out int tempI))
                return "int";
            else if (float.TryParse(typeName, out float tempF))
                return "float";
            else
                return "string";
        }

        public static string ToUppor(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            value = value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1).ToLower();
            return value;
        }

        public static int GetProperties(int native, List<Properties> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (native == list[i].nativeID)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listPath"></param>
        /// <param name="isFrist"></param>
        public static void GetFiles(string path, ref List<string> listPath, bool isFrist = true)
        {
            if (isFrist)
                listPath.Clear();
            if (!Directory.Exists(path))
                return;
            string[] dirs = Directory.GetDirectories(path);
            for (int i = 0; i < dirs.Length; i++)
                GetFiles(path, ref listPath, false);
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string exten = Path.GetExtension(files[i]);
                if (!string.Equals(exten, ".meta"))
                    listPath.Add(files[i].Replace("\\", "/"));
            }
        }
    }
}
