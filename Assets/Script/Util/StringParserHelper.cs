using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringParserHelper
{
    #region SignProcessing
    /// <summary>
    /// { } 파싱
    /// </summary>
    public static List<string> BracesParser(string parserableData)
    {
        List<string> resultList = new List<string>();

        // 정규표현식을 사용하여 { } 안의 문자열을 추출
        Regex regex = new Regex(@"\{(.*?)\}");
        MatchCollection matches = regex.Matches(parserableData);

        foreach (Match match in matches)
        {
            string content = match.Groups[1].Value; // { }를 제외한 내용
            resultList.Add(content);
        }

        return resultList;
    }
    /// <summary>
    /// 콤마 파싱
    /// </summary>
    public static List<string> CommaParser(string parserableData)
    {
        List<string> resultList = new List<string>();

        string[] arr = parserableData.Split(',');
        string[] trimmedArr = arr.Select(s => s.Trim()).ToArray();
        
        return trimmedArr.ToList();
    }
    /// <summary>
    /// : 파싱
    /// </summary>
    public static KeyValuePair<string, string> FirstColonParser(string parserableData)
    {
        // 첫 번째 ':'의 위치를 찾음
        int colonIndex = parserableData.IndexOf(':');
        
        // ':'이 없는 경우 예외 처리
        if (colonIndex == -1)
        {
            throw new ArgumentException("':' 부호가 존재하지 않습니다.");
        }

        // ':' 다음에 '('가 있는지 확인
        int parenthesisIndex = parserableData.IndexOf('(', colonIndex + 1);

        if (parenthesisIndex != -1 && parenthesisIndex < colonIndex)
        {
            throw new ArgumentException("':' 부호가 '(' 부호보다 앞에 있지 않습니다.");
        }

        // 키와 값을 분리
        string key = parserableData.Substring(0, colonIndex).Trim();
        string value = parserableData.Substring(colonIndex + 1).Trim();
        
        // KeyValuePair 반환
        return new KeyValuePair<string, string>(key, value);
    }
    /// <summary>
    /// 괄호 파싱
    /// </summary>
    public static string ParenthesesParser(string parserableData)
    {
        // 문자열이 null 또는 비어 있는지 검사
        if (string.IsNullOrEmpty(parserableData))
        {
            return parserableData; // null 또는 빈 문자열인 경우 그대로 반환
        }

        // 좌우 양끝에 있는 괄호 제거
        if (parserableData.StartsWith("(") && parserableData.EndsWith(")"))
        {
            return parserableData.Substring(1, parserableData.Length - 2);
        }

        // 괄호가 없는 경우 그대로 반환
        return parserableData;
    }
    /// <summary>
    /// | 파싱
    /// </summary>
    public static List<string> PipeParser(string parserableData)
    {
        List<string> resultList = new List<string>();

        string[] arr = parserableData.Split('|');
        string[] trimmedArr = arr.Select(s => s.Trim()).ToArray();
        
        return trimmedArr.ToList();
    }
    #endregion

    #region DataProcessing
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data)
    {
        MethodInfo methodInfo = typeof(T).GetMethod("SetParserData", BindingFlags.Public | BindingFlags.Static);
        Type[] genericArguments = { typeof(T) };
        MethodInfo constructedMethod = methodInfo.MakeGenericMethod(genericArguments);
        if (constructedMethod != null)
        {
            object[] parameters = new object[] { columnTypeDic, dataRow, data };
            constructedMethod.Invoke(null, parameters);
        }
    }

    public static void BuiltInTypeParser<T>(object instance, Type fieldType, string fieldName, object cellData)
    {
        var fieldInfo = typeof(T).GetField(fieldName);
        //열거형 처리
        if (fieldType.IsEnum)
        {
            fieldInfo.SetValue(instance, Enum.Parse(fieldType, cellData.ToString()));
        }
        //String 처리
        else if (fieldType == typeof(string))
        {
            fieldInfo.SetValue(instance, cellData.ToString());
        }
        //값 형식 처리
        else if (fieldType.IsPrimitive)
        {
            Debug.Log($"{cellData}, {fieldType.Name}");
            /*if("all".Equals(cellData))
                fieldInfo.SetValue(instance, Convert.ChangeType("-1", fieldType));
            else*/
                fieldInfo.SetValue(instance, Convert.ChangeType(cellData, fieldType));
        }
    }

    public static void SetValueToEnum<TData, TEnum>(object genericInstance, string fieldName, string parserableData) where TEnum : struct, Enum
    {
        if (Enum.TryParse<TEnum>(parserableData, out TEnum enumValue))
        {
            FieldInfo enumFieldInfo = typeof(TData).GetField(fieldName);
            enumFieldInfo.SetValue(genericInstance, enumValue);
        }
        else
        {
            throw new ArgumentException($"Failed to parse '{parserableData}' to enum type {typeof(TEnum)}.");
        }
    }
    public static void SetValueToEnumFlag<TData, TEnum>(object genericInstance, string fieldName, string parserableData) where TEnum : struct, Enum
    {
        var triggers = StringParserHelper.PipeParser(parserableData);
        TEnum enumFlag = default(TEnum); // 초기화

        foreach (var trigger in triggers)
        {
            if (Enum.TryParse<TEnum>(trigger, out TEnum parsedTrigger))
            {
                enumFlag = (TEnum)Enum.ToObject(typeof(TEnum), Convert.ToInt32(enumFlag) | Convert.ToInt32(parsedTrigger));
            }
        }
        
        FieldInfo enumFieldInfo = typeof(TData).GetField(fieldName);
        enumFieldInfo.SetValue(genericInstance, enumFlag);
    }
    public static void SetValueToList<T>(object instance, Type genericType, string fieldName, string data)
    {
        FieldInfo fieldInfo = typeof(T).GetField(fieldName);
        var listInstance = fieldInfo.GetValue(instance);
        var listGenericType = fieldInfo.FieldType.GetGenericArguments()[0];
        if (listInstance == null)
        {
            listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(listGenericType));
            fieldInfo.SetValue(instance, listInstance);
        }
        
        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        
        object valueToAdd = Convert.ChangeType(data, listGenericType);
        addMethod.Invoke(listInstance, new object[] { valueToAdd });
    }
    public static void SetValueToKeyValue<T>(object instance, string fieldName, string keyString, string valueString)
    {
        // 인스턴스의 필드 정보 가져오기
        FieldInfo fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo == null)
        {
            throw new ArgumentException($"Field '{fieldName}' not found in type '{typeof(T)}'.");
        }

        // 필드의 현재 값 가져오기
        var listInstance = fieldInfo.GetValue(instance);

        // 필드가 null인 경우 초기화
        if (listInstance == null)
        {
            listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(KeyValuePair<string, string>)));
            fieldInfo.SetValue(instance, listInstance);
        }

        // 리스트 타입과 Add 메서드 가져오기
        var listType = listInstance.GetType();
        MethodInfo addMethod = listType.GetMethod("Add");

        if (addMethod == null)
        {
            throw new InvalidOperationException("Add method not found on list type.");
        }

        // KeyValuePair<string, string> 생성
        var keyValuePairType = typeof(KeyValuePair<string, string>);
        var keyValuePair = Activator.CreateInstance(keyValuePairType, keyString, valueString);

        // 리스트에 KeyValuePair 추가
        addMethod.Invoke(listInstance, new object[] { keyValuePair });
    }
    public static void SetValueToDictionary<T>(object instance, string fieldName, string keyString, string valueString)
    {
        // 인스턴스의 필드 정보 가져오기
        FieldInfo fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo == null)
        {
            throw new ArgumentException($"Field '{fieldName}' not found in type '{typeof(T)}'.");
        }

        // 필드의 현재 값 가져오기
        var dictionaryInstance = fieldInfo.GetValue(instance);

        // 필드가 null인 경우 초기화
        if (dictionaryInstance == null)
        {
            dictionaryInstance = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(typeof(string), typeof(string)));
            fieldInfo.SetValue(instance, dictionaryInstance);
        }

        // Dictionary 타입 가져오기
        var dictionaryType = dictionaryInstance.GetType();
        MethodInfo addMethod = dictionaryType.GetMethod("Add");

        if (addMethod == null)
        {
            throw new InvalidOperationException("Add method not found on dictionary type.");
        }

        // Dictionary에 키-값 추가
        addMethod.Invoke(dictionaryInstance, new object[] { keyString, valueString });
    }
    public static void SetValueToKeyValueData<T>(object instance, string fieldName, string keyString, string valueString)
    {
        // 인스턴스의 필드 정보 가져오기
        FieldInfo fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo == null)
        {
            throw new ArgumentException($"Field '{fieldName}' not found in type '{typeof(T)}'.");
        }

        // 필드의 현재 값 가져오기
        var listInstance = fieldInfo.GetValue(instance);

        // 필드가 null인 경우 초기화
        if (listInstance == null)
        {
            listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(KeyValueData<string, string>)));
            fieldInfo.SetValue(instance, listInstance);
        }

        // 리스트 타입과 Add 메서드 가져오기
        var listType = listInstance.GetType();
        MethodInfo addMethod = listType.GetMethod("Add");

        if (addMethod == null)
        {
            throw new InvalidOperationException("Add method not found on list type.");
        }

        // KeyValueData<string, string> 생성
        var keyValuePairType = typeof(KeyValueData<,>).MakeGenericType(typeof(string), typeof(string));
        var keyValuePair = Activator.CreateInstance(keyValuePairType, keyString, valueString);

        // 리스트에 KeyValueData 추가
        addMethod.Invoke(listInstance, new object[] { keyValuePair });
    }
    #endregion
}