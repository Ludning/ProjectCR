using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

public class InitData
{
    [ShowInInspector]
    public List<KeyValueData<string, string>> initDataList;
    
    public static void SetParserData<T>(T instance, string parserableData)
    {
        FieldInfo fieldInfo = typeof(InitData).GetField("initDataList", BindingFlags.Public | BindingFlags.Instance);
        /*object initDataListInstance = fieldInfo.GetValue(instance);
        
        if (initDataListInstance == null)
        {
            initDataListInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(KeyValueData<string, string>)));
            fieldInfo.SetValue(instance, initDataListInstance);
        }*/

        var initDataStringList = StringParserHelper.BracesParser(parserableData);
        foreach (var initDataString in initDataStringList)
        {
            var dataStrings = StringParserHelper.CommaParser(initDataString);
            foreach (var dataString in dataStrings)
            {
                var keyValueData = StringParserHelper.FirstColonParser(dataString);
                StringParserHelper.SetValueToKeyValueData<InitData>(instance, "initDataList", keyValueData.Key, keyValueData.Value);
            }
        }
    }
}
