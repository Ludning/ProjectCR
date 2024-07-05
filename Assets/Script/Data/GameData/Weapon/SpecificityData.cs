using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;

[Serializable]
public class SpecificityData : IParserable
{
    [TableColumnWidth(40, false)]
    public int index;
    public SpecificityType specificityType;
    public string name;
    public string description;
    public List<ConditionEffectData> conditionEffectDatas;
    public List<RecordData> recordDatas;
    public List<ReferenceData> referenceDatas;
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T dataInstance) where T : IParserable
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
                        ConditionEffectData.SetParserData(listInstance, genericType, conditionEffect);
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
                        RecordData.SetParserData(listInstance, genericType, conditionEffect);
                    }
                    break;
                case "referenceDatas":
                    listInstance = fieldInfo.GetValue(dataInstance);
                    genericType = fieldType.GetGenericArguments()[0];
                    if (listInstance == null)
                    {
                        listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
                        fieldInfo.SetValue(dataInstance, listInstance);
                    }
                    string referenceCellData = dataRow[keyValuePair.Value].ToString();
                    var referenceDatas = StringParserHelper.BracesParser(referenceCellData);
                    foreach (var referenceData in referenceDatas)
                    {
                        ReferenceData.SetParserData(listInstance, genericType, referenceData);
                    }
                    break;
                default:
                    StringParserHelper.BuiltInTypeParser<T>(dataInstance, fieldType, fieldInfo.Name, dataRow[keyValuePair.Value]);
                    break;
            }
        }
    }
}