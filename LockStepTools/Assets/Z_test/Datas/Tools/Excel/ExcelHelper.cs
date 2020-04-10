using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace MokaData
{
    public class ExcelHelper 
    {
        public static void LoadData(string path ,string save ,string namepace)
        {
            if (Path.GetExtension(path) != ".xlsx"||!File.Exists(path))
            {
                Debug.LogError("传入的文件后缀错误");
                return;
            }
            List<List<string>> lists = Read(path);
            Utils.Write(Path.GetFileNameWithoutExtension(path), save, namepace, lists);
        }

       public  static List<List<string>> Read(string path)
        {
            FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            DataSet result = excelDataReader.AsDataSet();
            // 获取表格有多少列 
            int columns = result.Tables[0].Columns.Count;
            // 获取表格有多少行 
            int rows = result.Tables[0].Rows.Count;
            // 根据行列依次打印表格中的每个数据 
            List<List<string>> excelDta = new List<List<string>>();

            for (int i = 0; i < rows; i++)
            {
                List<string> list = new List<string>();
                for (int k = 0; k < columns; k++)
                {
                   string file =  result.Tables[0].Rows[i][k].ToString();
                    list.Add(file);
                }
                excelDta.Add(list);
            }
            return excelDta;
        }
    }
}
