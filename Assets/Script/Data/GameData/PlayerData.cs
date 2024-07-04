using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;

[Serializable]
public class PlayerData : IParserable
{
    [TableColumnWidth(40)]
    [VerticalGroup("Player Account"), LabelWidth(70)]
    public string identification;
    [VerticalGroup("Player Account"), LabelWidth(70)]
    public string password;
    [VerticalGroup("Player Account"), LabelWidth(70)]
    public string nickname;
    
    [TableColumnWidth(80)]
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string character;
    [VerticalGroup("Player Data"), LabelWidth(70)]
    public int level;
    [VerticalGroup("Player Data"), LabelWidth(70)]
    public int exp;
    
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string inventory_data;
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string equipment_data;
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string ownedSkill_data;
    [VerticalGroup("Player Data"), LabelWidth(90)]
    public string equipmentSkill_data;
    
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