using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class LevelData : IParserable
{
    [TableColumnWidth(40, false)]
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
    public int expRequired;
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T dataInstance) where T : IParserable
    {
        foreach (KeyValuePair<string, int> keyValuePair in columnTypeDic)
        {
            FieldInfo fieldInfo = typeof(T).GetField(keyValuePair.Key, BindingFlags.Public | BindingFlags.Instance);
            Type fieldType = fieldInfo.FieldType;

            if (dataRow[keyValuePair.Value] == DBNull.Value)
                continue;

            StringParserHelper.BuiltInTypeParser<T>(dataInstance, fieldType, fieldInfo.Name, dataRow[keyValuePair.Value]);
        }
    }
}
