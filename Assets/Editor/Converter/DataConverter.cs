using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using ExcelDataReader;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

public class DataConverter
{
    public static void LoadExcel<T>(string xlsxPath, string assetPath) where T : ScriptableObject
    {
        Debug.Log("ReadExcel");

        //파일 존재 체크
        if (IsFileExists(xlsxPath) == false)
            return;

        //엑셀파일로 부터 테이블 데이터 로드
        var tables = GetTableFromXlsx(xlsxPath);
        var asset = GetScriptableAsset<T>(assetPath);

        //데이터를 스크립터블로 파싱
        ParseTableToScriptable<T>(asset, tables);
        
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        //var assetType = typeof(T);
        //var loadedAsset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

        /*foreach (var fieldInfo in assetType.GetFields())
        {
            //제네릭타입 알아내기
            Type type = fieldInfo.FieldType.GetGenericArguments()[0];
            //함수 리플렉션 호출
            var method = typeof(DataConverter)
                .GetMethod(nameof(ReadDataFromTable), BindingFlags.Static | BindingFlags.Public)
                ?.MakeGenericMethod(type);
            if (method == null) continue;

            var data = method.Invoke(null, new object[] { fieldInfo.Name, tables });

            if (loadedAsset == null)
            {
                loadedAsset = ScriptableObject.CreateInstance<T>();

                fieldInfo.SetValue(loadedAsset, data);
                AssetDatabase.CreateAsset(loadedAsset, assetPath);
            }
            else
            {
                fieldInfo.SetValue(loadedAsset, data);
            }
        }*/
    }

    //엑셀파일로 부터 테이블 데이터 로드
    private static DataTableCollection GetTableFromXlsx(string xlsxPath)
    {
        using var stream = new FileStream(xlsxPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        // FileStream을 사용한 코드 작성
        using var reader = ExcelReaderFactory.CreateReader(stream);
        // 모든 시트 로드
        return reader.AsDataSet().Tables;
    }
    
    private static T GetScriptableAsset<T>(string assetPath) where T : ScriptableObject
    {
        T loadedAsset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        if (loadedAsset == null)
        {
            loadedAsset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(loadedAsset, assetPath);
        }
        return loadedAsset;
    }

    //테이블 데이터를 스크립터블 오브젝트에 저장
    private static void ParseTableToScriptable<T>(T loadedAsset, DataTableCollection tables) where T : ScriptableObject
    {
        var assetType = typeof(T);
        var fieldInfos = assetType.GetFields();
        foreach (var fieldInfo in fieldInfos)
        {
            //제네릭타입 알아내기
            Type type = fieldInfo.FieldType.GetGenericArguments()[0];
            //함수 리플렉션 호출
            var method = typeof(DataConverter).GetMethod(nameof(ReadDataFromTable), BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(type);
            if (method == null) continue;

            var data = method.Invoke(null, new object[] { fieldInfo.Name, tables });
            
            fieldInfo.SetValue(loadedAsset, data);
        }
    }

    public static List<T> ReadDataFromTable<T>(string sheetName, DataTableCollection tables) where T : class, new()
    {
        if (tables.Contains(sheetName) == false)
        {
            Debug.LogError($"Xlsx 파일에 Sheet이름 : {sheetName} 이 존재하지 않습니다");
            return null;
        }

        DataTable sheet = tables[sheetName];
        var dataType = typeof(T);
        
        FieldInfo[] fieldInfos = dataType.GetFields();
        List<T> ret = new List<T>();


        
        Dictionary<int, string> columnTypeDic = new Dictionary<int, string>();

        //0행의 데이터를 가져온다, 0행의 데이터는 자료형을 결정하기 떄문
        for (int fieldColumn = 0; fieldColumn < dataType.GetFields().Length; fieldColumn++)
        {
            string val = (string)(sheet.Rows[0].ItemArray[fieldColumn]);
            if (string.IsNullOrWhiteSpace(val))
                break;
            columnTypeDic.Add(fieldColumn, val);
        }

        for (var rowIndex = 1; rowIndex < sheet.Rows.Count; rowIndex++)
        {
            // 행 가져오기
            var dataRow = sheet.Rows[rowIndex];
            T data = new T();
            
            for (var columnIndex = 0; columnIndex < dataRow.ItemArray.Length; columnIndex++)
            {
                var item = dataRow.ItemArray[columnIndex];
                

                if (!columnTypeDic.TryGetValue(columnIndex, out string value)) 
                    continue;
                
                FieldInfo fieldInfo = Array.Find(fieldInfos, field => field.Name == value);
                
                if(fieldInfo == null)
                    continue;
                
                
                Type type = fieldInfo.FieldType;

                Debug.Log($"Type : {type}, Data : {item.ToString()}");
                if (type.IsEnum)
                    fieldInfo.SetValue(data, Enum.Parse(type, item.ToString()));
                else if (type == typeof(string))
                    fieldInfo.SetValue(data, item.ToString());
                else if (type.IsPrimitive)
                    fieldInfo.SetValue(data, Convert.ChangeType(item, type));
                
            }
            ret.Add(data);
        }
        return ret;
        
        // 열 가져오기
        //Debug.Log($"slot[{rowIndex}][{columnIndex}] : {item}");
        /*Type type = fi.FieldType;

        if (type.IsEnum)
            fi.SetValue(data, Enum.Parse(type, val));
        else if (type == typeof(string))
            fi.SetValue(data, val);
        else if (type.IsPrimitive)
            fi.SetValue(data, Convert.ChangeType(val, type));
        else if (type.GetInterface("IParsable") != null)
        {
        }*/
        
        //시트 이름 필터링 가능
        //Debug.Log($"Sheet[{sheetIndex}] Name: {sheet.TableName}");
    }

    //파일이 있는지 확인
    private static bool IsFileExists(string path)
    {

        Debug.Log("Path : " + path);
        var isExist = File.Exists(path);
        if (isExist == false)
            Debug.LogError("Xlsx 파일이 존재하지 않습니다.");

        return isExist;
    }
    
    
    /*public static void ReadCsv(string csvPath)
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
    }*/
    /*public static void ReadJson(string jsonPath)
    {
        //xlsx파일 주소
        //string xlsxPath = "Assets/Resource/Xlsx/data.json";

        Debug.Log("ReadJson");

        if (IsFileExists(jsonPath) == false)
            return;

        using (var stream = File.Open(jsonPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            /*var data = await JsonSerializer.DeserializeAsync<YourDataType>(stream);
            Debug.Log($"Name: {data.Name}, Age: {data.Age}");#1#
        }
    }*/
}
