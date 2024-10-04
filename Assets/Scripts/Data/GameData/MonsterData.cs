using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

[Serializable]
public class MonsterData : IParserable
{
    public string monsterName;
    public MonsterType monsterType;
    public int level;
    public int maxHp;
    public int damage;
    public int defense;
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
