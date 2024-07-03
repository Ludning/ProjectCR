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
    
    public List<WeaponConditionEffectData> conditionEffectDatas;
    public List<WeaponRecordData> recordDatas;
    
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T dataInstance)
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
                    listInstance = fieldInfo.GetValue(dataInstance);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(dataInstance, listInstance);
                    }

                    string conditionEffectCellData = dataRow[keyValuePair.Value].ToString();
                    var conditionEffects = StringParserHelper.BracesParser(conditionEffectCellData);
                    foreach (var conditionEffect in conditionEffects)
                    {
                        WeaponConditionEffectData.SetParserData(listInstance, genericType, conditionEffect);
                    }
                    break;
                case "recordDatas":
                    listInstance = fieldInfo.GetValue(dataInstance);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(dataInstance, listInstance);
                    }
                    
                    string recordEffectCellData = dataRow[keyValuePair.Value].ToString();
                    var recordEffects = StringParserHelper.BracesParser(recordEffectCellData);
                    foreach (var conditionEffect in recordEffects)
                    {
                        WeaponRecordData.SetParserData(listInstance, genericType, conditionEffect);
                    }
                    break;
                default:
                    StringParserHelper.BuiltInTypeParser<T>(dataInstance, fieldType, fieldInfo.Name, dataRow[keyValuePair.Value]);
                    break;
            }
        }
    }
}
[Serializable]
public class WeaponConditionEffectData
{
    public WeaponTrigger trigger;
    public List<string> conditionDatas;
    public List<string> effectDatas;
    
    
    public static void SetParserData<T>(T listInstance, Type genericType, string parserableData)
    {
        object genericInstance = Activator.CreateInstance(genericType);
        
        
        var effectUnits = StringParserHelper.CommaParser(parserableData);
        foreach (var effectUnit in effectUnits)
        {
            var conditionAndEffects = StringParserHelper.FirstColonParser(effectUnit);
            Debug.Log($"{conditionAndEffects.Key}, {conditionAndEffects.Value}");

            if (Enum.TryParse<ConditionType>(conditionAndEffects.Key, out ConditionType conditionResult))
            {
                switch (conditionResult)
                {
                    case ConditionType.Trigger:
                        StringParserHelper.SetValueToEnumFlag<WeaponConditionEffectData, WeaponTrigger>(genericInstance, "trigger", conditionAndEffects.Value);
                        break;
                    case ConditionType.RequestRecordValue:
                        StringParserHelper.SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "conditionDatas", conditionAndEffects.Value);
                        break;
                }
            }
            else if (Enum.TryParse<EffectType>(conditionAndEffects.Key, out EffectType effectResult))
            {
                switch (effectResult)
                {
                    case EffectType.SetRecordValue:
                        StringParserHelper.SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "effectDatas", conditionAndEffects.Value);
                        break;
                    case EffectType.IncreasedStat:
                        StringParserHelper.SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "effectDatas", conditionAndEffects.Value);
                        break;
                    case EffectType.SpawnObject:
                        StringParserHelper.SetValueToList<WeaponConditionEffectData>(genericInstance, typeof(string), "effectDatas", conditionAndEffects.Value);
                        break;
                }
            }
            
            /*switch (conditionAndEffects.Key)
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
            }*/
        }
        
        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}
[Serializable]
public class WeaponRecordData : IParserable
{
    public string RecordName;
    public RecordType RecordType;
    public float RecordLimit;
    public float Duration;
    public int RecordResetValue;

    public static void SetParserData<T>(T listInstance, Type genericType, string parserableData)
    {
        object genericInstance = Activator.CreateInstance(genericType);
        var recordUnits = StringParserHelper.CommaParser(parserableData);
        foreach (var recordUnit in recordUnits)
        {
            var keyValuePair = StringParserHelper.FirstColonParser(recordUnit);
            Type fieldType = typeof(WeaponRecordData).GetField(keyValuePair.Key).FieldType;
            StringParserHelper.BuiltInTypeParser<WeaponRecordData>(genericInstance, fieldType, keyValuePair.Key, keyValuePair.Value);
        }

        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}