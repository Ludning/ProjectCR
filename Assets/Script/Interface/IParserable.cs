using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UnityEngine;

public class IParserable
{
    public static void BuiltInTypeParser<T>(Type fieldType, FieldInfo fieldInfo, object cellData, T data) where T : IParserable
    {
        //열거형 처리
        if (fieldType.IsEnum)
        {
            fieldInfo.SetValue(data, Enum.Parse(fieldType, cellData.ToString()));
        }
        //String 처리
        else if (fieldType == typeof(string))
        {
            fieldInfo.SetValue(data, cellData.ToString());
        }
        //값 형식 처리
        else if (fieldType.IsPrimitive)
        {
            fieldInfo.SetValue(data, Convert.ChangeType(cellData, fieldType));
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
        
        //제네릭 인스턴스 필드들 가져옴
        //FieldInfo genericField;
        /*object genericInstance = Activator.CreateInstance(genericType);
        genericField.SetValue(genericInstance, data.ToString());

        if (genericType == typeof(string))
        {
            fi.SetValue(genericInstance, dataRow[index].ToString());
        }*/
        /*
        // 데이터 추가하는 부분
        MethodInfo addMethod = requestRecordListInstance.GetType().GetMethod("Add");
        object parsedData = ParseData(genericType, data); // 데이터를 적절히 파싱해서 추가하는 것을 가정
        addMethod.Invoke(requestRecordListInstance, new object[] { parsedData });*/
        
        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        //addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}
