using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ExcelDataReader;
using UnityEditor;
using UnityEngine;

public class DataConverter
{
    [MenuItem("DataConverter/ReadExcel")]
    public static void ReadExcel(string xlsxPath)
    {
        Debug.Log("ReadExcel");

        //파일 존재 체크
        if (IsFileExists(xlsxPath) == false)
            return;

        using var stream = new FileStream(xlsxPath, FileMode.Open, FileAccess.Read);
        // FileStream을 사용한 코드 작성
        using var reader = ExcelReaderFactory.CreateReader(stream);
        // 모든 시트 로드
        var tables = reader.AsDataSet().Tables;
        
        var assetPath = "Assets/Resource/Data/GameData.asset";
        var gameDataType = typeof(GameData);
        var loadedAsset = AssetDatabase.LoadAssetAtPath<GameData>(assetPath);

        foreach (var fieldInfo in gameDataType.GetFields())
        {
            //제네릭타입 알아내기
            Type type = fieldInfo.FieldType.GetGenericArguments()[0];
            //함수 리플렉션 호출
            var method = typeof(DataConverter).GetMethod(nameof(ReadDataFromXlsx), BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(type);
            if (method == null) continue;
            
            var data = method.Invoke(null, new object[] { fieldInfo.Name, tables });
                
            if (loadedAsset == null)
            {
                loadedAsset = ScriptableObject.CreateInstance<GameData>();

                fieldInfo.SetValue(loadedAsset, data);
                AssetDatabase.CreateAsset(loadedAsset, assetPath);
            }
            else
            {
                fieldInfo.SetValue(loadedAsset, data);
            }
        }
    }

    public static List<T> ReadDataFromXlsx<T>(string sheetName, DataTableCollection tables) where T : class, new()
    {
        if (tables.Contains(sheetName) == false)
        {
            Debug.LogError($"Xlsx 파일에 Sheet이름 : {sheetName} 이 존재하지 않습니다");
            return null;
        }
        
        DataTable sheet = tables[sheetName];
        var dataType = typeof(T);
        List<T> ret = new List<T>();
        
        Dictionary<int, string> columnTypeDic = new Dictionary<int, string>();
        
        //0행의 데이터를 가져온다, 0행의 데이터는 자료형을 결정하기 떄문
        DataRow dataRow = sheet.Rows[0];
        for (int fieldColumn = 1; fieldColumn <= dataType.GetFields().Length; fieldColumn++)
        {
            //dataRow
            //string val = sheet.Cell(row, fieldColumn).GetValue<String>();
            //if (string.IsNullOrWhiteSpace(val))
            //    break;
            //columnTypeDic.Add(fieldColumn, val);
        }

        
        //시트 이름 필터링 가능
        //Debug.Log($"Sheet[{sheetIndex}] Name: {sheet.TableName}");

        for (var rowIndex = 0; rowIndex < sheet.Rows.Count; rowIndex++)
        {
            // 행 가져오기
            var slot = sheet.Rows[rowIndex];
            for (var columnIndex = 0; columnIndex < slot.ItemArray.Length; columnIndex++)
            {
                var item = slot.ItemArray[columnIndex];
                // 열 가져오기
                Debug.Log($"slot[{rowIndex}][{columnIndex}] : {item}");
            }
        }

        return ret;
    }




    public static void ReadCsv(string csvPath)
    {
        Debug.Log("ReadExcel");

        if (IsFileExists(csvPath) == false)
            return;

        // 한글 깨짐 현상 해결 가능
        var config = new ExcelReaderConfiguration();
        config.FallbackEncoding = Encoding.GetEncoding("ks_c_5601-1987");

        var stream = new FileStream(csvPath, FileMode.Open, FileAccess.Read);
        using (var reader = ExcelReaderFactory.CreateCsvReader(stream, config))
        {
            // 항상 하나의 시트만 관리된다.
            var sheet = reader.AsDataSet().Tables[0];
            // 시트 이름
            Debug.Log($"Sheet Name: {sheet.TableName}");
            for (var rowIndex = 0; rowIndex < sheet.Rows.Count; rowIndex++)
            {
                // 행 가져오기
                var slot = sheet.Rows[rowIndex];
                for (var columnIndex = 0; columnIndex < slot.ItemArray.Length; columnIndex++)
                {
                    // 열 가져오기
                    var item = slot.ItemArray[columnIndex];
                    Debug.Log($"slot[{rowIndex}][{columnIndex}] : {item}");
                }
            }

            reader.Dispose();
            reader.Close();
        }
    }

    public static void ReadJson(string jsonPath)
    {
        //xlsx파일 주소
        //string xlsxPath = "Assets/Resource/Xlsx/data.json";
        
        Debug.Log("ReadJson");

        if (IsFileExists(jsonPath) == false)
            return;

        using (var stream = File.Open(jsonPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            /*var data = await JsonSerializer.DeserializeAsync<YourDataType>(stream);
            Debug.Log($"Name: {data.Name}, Age: {data.Age}");*/
        }
    }

    //파일이 있는지 확인
    private static bool IsFileExists(string path)
    {
        
        Debug.Log("Path : " + path);
        var isExist = File.Exists(path);
        if (isExist == false)
            Debug.LogError("파일이 존재하지 않습니다.");
        
        return isExist;
    }
}
