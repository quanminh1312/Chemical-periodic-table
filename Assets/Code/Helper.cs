using Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helper
{
    public static  List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
    public static string[] headers;
    public static List<string> elementsGroup = new List<string>() { "IA", "IIA", "IIIB", "IVB", "VB", "VIB", "VIIB", "VIIIB", "VIIIB", "VIIIB", "IB", "IIB", "IIIA", "IVA", "VA", "VIA", "VIIA", "VIIIA", };
    public static List<string> lNumber = new List<string>() {"s","p", "d", "f" };
    public static void ReadCSVFile()
    {
        TextAsset tableData = Resources.Load("PeriodicTableCSV") as TextAsset;
        string csvContent = tableData.text;

        // Tách các dòng trong file CSV
        string[] lines = csvContent.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);

        // Lấy các tiêu đề cột từ dòng đầu tiên
        headers = lines[0].Split(',');

        // Xử lý các dòng còn lại
        for (int i = 1; i < lines.Length - 1; i++)
        {
            // Tách các giá trị trong dòng
            string[] values = lines[i].Split(',');

            // Tạo một dictionary để lưu trữ các giá trị của dòng hiện tại
            Dictionary<string, string> rowData = new Dictionary<string, string>();

            // Lưu trữ các giá trị vào dictionary
            for (int j = 0; j < headers.Length; j++)
            {
                string value = "";
                if (values[j] != null) value = values[j];
                rowData.Add(headers[j], value);
            }
            // Thêm dictionary vào danh sách data
            data.Add(rowData);
        }
    }
    public static void TurnOnSceneAddictiveMode()
    {
        SceneManager.LoadScene("InfoScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("TableScene", LoadSceneMode.Additive);
    }
    public static void ToggleInfoScene(int number)
    {
        InfosSceneManager.instance.ToggleInfoSceneManager(number);
    }
    public static void ClearAllData()
    {
        data.Clear();
        headers = null;
    }
}
