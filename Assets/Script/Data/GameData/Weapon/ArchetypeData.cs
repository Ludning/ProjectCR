using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class ArchetypeData : IParserable
{
    [TableColumnWidth(40, false)]
    public int index;
    public ArchetypeType archetypeType;
    public string name;
    public string description;
    public WeaponType weaponType;
    
    private List<WeaponConditionEffectData> conditionEffectDatas;
    private List<WeaponRecordData> recordDatas;
    
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T data) where T : IParserable
    {
        // 변수의 이름을 가져오기 위해 변수가 있는 클래스의 타입을 알아야 합니다.
        
        foreach (KeyValuePair<string,int> keyValuePair in columnTypeDic)
        {
            FieldInfo fieldInfo = typeof(T).GetField(keyValuePair.Key, BindingFlags.Public | BindingFlags.Instance);
            Type fieldType = fieldInfo.FieldType;
            
            if(dataRow[keyValuePair.Value] == DBNull.Value)
                continue;

            object listInstance;
            Type genericType;
            
            switch (keyValuePair.Key)
            {
                case "conditionEffectDatas":
                    listInstance = fieldInfo.GetValue(data);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(data, listInstance);
                    }

                    string conditionEffectCellData = dataRow[keyValuePair.Value].ToString();
                    var conditionEffects = StringParserHelper.BracesParser(conditionEffectCellData);
                    foreach (var conditionEffect in conditionEffects)
                    {
                        WeaponConditionEffectData.SetParserData(listInstance, genericType, conditionEffect);
                    }
                    //fieldInfo.SetValue(data, Enum.Parse(fieldType, dataRow[keyValuePair.Value].ToString()));
                    break;
                case "recordDatas":
                    listInstance = fieldInfo.GetValue(data);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(data, listInstance);
                    }
                    WeaponRecordData.SetParserData(data, genericType, dataRow[keyValuePair.Value].ToString());
                    //fieldInfo.SetValue(data, Enum.Parse(fieldType, dataRow[keyValuePair.Value].ToString()));
                    break;
                default:
                {
                    BuiltInTypeParser<T>(fieldType, fieldInfo, dataRow[keyValuePair.Value], data);
                    break;
                }
            }
        }
    }

    /*public static void ParserWeaponConditionEffectData<T>(T listInstance, Type genericType, string parserableData)
    {
        FieldInfo[] genericFields = genericType.GetFields();
        object genericInstance = Activator.CreateInstance(genericType);
        
        switch (keyValuePair.Key)
        {
            case "conditionEffectDatas":
                listInstance = fieldInfo.GetValue(data);
                genericType = fieldType.GetGenericArguments()[0];
                if (listInstance == null)
                {
                    listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                    fieldInfo.SetValue(data, listInstance);
                }
                ParserWeaponConditionEffectData(listInstance, genericType, dataRow[keyValuePair.Value].ToString());
                //fieldInfo.SetValue(data, Enum.Parse(fieldType, dataRow[keyValuePair.Value].ToString()));
                break;
            case "recordDatas":
                listInstance = fieldInfo.GetValue(data);
                genericType = fieldType.GetGenericArguments()[0];
                if (listInstance == null)
                {
                    listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                    fieldInfo.SetValue(data, listInstance);
                }
                ParserWeaponRecordData(data, genericType, dataRow[keyValuePair.Value].ToString());
                //fieldInfo.SetValue(data, Enum.Parse(fieldType, dataRow[keyValuePair.Value].ToString()));
                break;
            default:
            {
                BuiltInTypeParser<T>(fieldType, fieldInfo, dataRow[keyValuePair.Value], data);
                break;
            }
        }
        
        /*foreach (FieldInfo fi in genericFields)
        {
            Type fiType = fi.FieldType;
            if (fiType.IsEnum)
            {
                if (columnTypeDic.TryGetValue(fi.Name, out int index))
                    if(dataRow[index] != DBNull.Value)
                        fi.SetValue(genericInstance, Enum.Parse(fiType, dataRow[index].ToString()));
            }
            else if (fiType == typeof(string))
            {
                if (columnTypeDic.TryGetValue(fi.Name, out int index))
                    if(dataRow[index] != DBNull.Value)
                        fi.SetValue(genericInstance, dataRow[index].ToString());
            }
            else if (fiType.IsPrimitive)
            {
                if (columnTypeDic.TryGetValue(fi.Name, out int index))
                    if(dataRow[index] != DBNull.Value)
                        fi.SetValue(genericInstance, Convert.ChangeType(dataRow[index], fiType));
            }
        }#1#
        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
    public static void ParserWeaponRecordData<T>(T listData, Type genericType, string parserableData)
    {
        FieldInfo[] genericFields = genericType.GetFields();
        object genericInstance = Activator.CreateInstance(genericType);
    }*/
}
[Serializable]
public class WeaponConditionEffectData : IParserable
{
    public WeaponTrigger trigger;
    public List<string> conditionDatas;
    public List<string> effectDatas;
    
    
    public static void SetParserData<T>(T listInstance, Type genericType, string parserableData)
    {
        //FieldInfo[] genericFields = genericType.GetFields();
        object genericInstance = Activator.CreateInstance(genericType);
        
        //FieldInfo fieldInfo = typeof(T).GetField(keyValuePair.Key, BindingFlags.Public | BindingFlags.Instance);
        //Type fieldType = fieldInfo.FieldType;
        
        /*FieldInfo[] fieldInfos = genericInstance.GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if(string.IsNullOrWhiteSpace(parserableData))
                continue;
            
            switch (fieldInfo.Name)
            {
                case "trigger":
                    
                    break;
                case "conditionDatas":
                    break;
                case "effectDatas":
                    break;
            }
        }*/
        
        var effectUnits = StringParserHelper.CommaParser(parserableData);
        foreach (var effectUnit in effectUnits)
        {
            var conditionAndEffects = StringParserHelper.FirstColonParser(effectUnit);
            Debug.Log(conditionAndEffects.Key);
            Debug.Log(conditionAndEffects.Value);

            if (Enum.TryParse<ConditionType>(conditionAndEffects.Key, out ConditionType conditionResult))
            {
                switch (conditionResult)
                {
                    case ConditionType.Trigger:
                        SetValueToEnumFlag<WeaponConditionEffectData, WeaponTrigger>(genericInstance, "trigger", conditionAndEffects.Value);
                        break;
                    case ConditionType.RequestRecordValue:
                        SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "conditionDatas", conditionAndEffects.Value);
                        break;
                }
            }
            else if (Enum.TryParse<EffectType>(conditionAndEffects.Key, out EffectType effectResult))
            {
                switch (effectResult)
                {
                    case EffectType.SetRecordValues:
                        SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "effectDatas", conditionAndEffects.Value);
                        break;
                    case EffectType.IncreasedStat:
                        SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "effectDatas", conditionAndEffects.Value);
                        break;
                    case EffectType.SpawnObject:
                        SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "effectDatas", conditionAndEffects.Value);
                        break;
                }
            }
            
            switch (conditionAndEffects.Key)
            {
                case "Trigger":
                    var triggers = StringParserHelper.PipeParser(conditionAndEffects.Value);
                    WeaponTrigger combinedTriggers = WeaponTrigger.None;

                    foreach (var trigger in triggers)
                    {
                        if (Enum.TryParse<WeaponTrigger>(trigger, out var parsedTrigger))
                            combinedTriggers |= parsedTrigger; // 비트 플래그 설정
                        else
                            throw new ArgumentException($"Invalid trigger value: {trigger}");
                    }
                    FieldInfo enumFieldInfo = typeof(WeaponConditionEffectData).GetField("trigger");
                    enumFieldInfo.SetValue(genericInstance, combinedTriggers);
                    break;
                case "RequestRecordValue":
                    string noParenthesesConditionAndEffects = StringParserHelper.ParenthesesParser(conditionAndEffects.Value);
                    
                    break;
                case "effectDatas":
                    
                    break;
            }
        }
        /*object exIistInstance;
        Type exGenericType;
        
        switch (keyValuePair.Key)
        {
            case "conditionDatas":
                listInstance = fieldInfo.GetValue(data);
                genericType = fieldType.GetGenericArguments()[0];
                if (listInstance == null)
                {
                    listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                    fieldInfo.SetValue(data, listInstance);
                }
                ParserWeaponConditionEffectData(listInstance, genericType, dataRow[keyValuePair.Value].ToString());
                //fieldInfo.SetValue(data, Enum.Parse(fieldType, dataRow[keyValuePair.Value].ToString()));
                break;
            case "effectDatas":
                listInstance = fieldInfo.GetValue(data);
                genericType = fieldType.GetGenericArguments()[0];
                if (listInstance == null)
                {
                    listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                    fieldInfo.SetValue(data, listInstance);
                }
                ParserWeaponRecordData(data, genericType, dataRow[keyValuePair.Value].ToString());
                //fieldInfo.SetValue(data, Enum.Parse(fieldType, dataRow[keyValuePair.Value].ToString()));
                break;
            default:
            {
                BuiltInTypeParser<T>(fieldType, fieldInfo, dataRow[keyValuePair.Value], data);
                break;
            }
        }*/
        
        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}
[Serializable]
public class WeaponRecordData : IParserable
{
    public string recordName;
    public RecordType recordType;
    public float recordLimit;
    public float duration;
    public int recordResetValue;
    
    public static void SetParserData<T>(T listData, Type genericType, string parserableData)
    {
        FieldInfo[] genericFields = genericType.GetFields();
        object genericInstance = Activator.CreateInstance(genericType);
    }
}