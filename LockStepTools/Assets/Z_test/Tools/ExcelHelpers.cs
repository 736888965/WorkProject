using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class ExcelHelpers
{
    /// <summary>
    ///返回数据的集合 
    ///数据的格式为 每一行为一条数据
    ///例：赵一|党员|1年|赵一.png| 
    /// </summary>
    /// <returns></returns>
    public static List<string> LoadData(string path)
    {
        // StreamingAssets目录下的  党员信息.xlsx文件的路径：Application.streamingAssetsPath + "/党员信息.xlsx" 
        //FileStream fileStream = File.Open(Application.streamingAssetsPath + "/党员信息.xlsx", FileMode.Open, FileAccess.Read);
        FileStream fileStream = File.Open(Application.dataPath + "/Z_test/1.xlsx", FileMode.Open, FileAccess.Read);

        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
        // 表格数据全部读取到result里(引入：DataSet（
       // using System.Data;） 需引入 System.Data.dll到项目中去)
        DataSet result = excelDataReader.AsDataSet();

        // 获取表格有多少列 
        int columns = result.Tables[0].Columns.Count;
        // 获取表格有多少行 
        int rows = result.Tables[0].Rows.Count;
        // 根据行列依次打印表格中的每个数据 

        List<string> excelDta = new List<string>();

        //第一行为表头，不读取
        for (int i = 1; i < rows; i++)
        {
            string value = null;
            string all = null;
            for (int j = 0; j < columns; j++)
            {
                // 获取表格中指定行指定列的数据 
                value = result.Tables[0].Rows[i][j].ToString();
                if (value == "")
                {
                    continue;
                }
                all = all + value + "|";
            }
            if (all != null)
            {
                Debug.Log(all);
                excelDta.Add(all);
            }
        }
        return excelDta;
    }

}
