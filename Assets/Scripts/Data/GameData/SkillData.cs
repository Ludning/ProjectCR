using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;

[Serializable]
public class SkillData : IParserable
{
    [TableColumnWidth(40, false)]
    public int index;
    public string skillName;
    public string description;
    public SkillType skillType;
    public SkillSlotType skillSlotType;
    public InitData initDatas;
    public List<ConditionEffectData> conditionEffectDatas;
    public List<RecordData> recordDatas;
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
                case "initDatas":
                    object instance = fieldInfo.GetValue(dataInstance);
                    if (instance == null)
                    {
                        instance = Activator.CreateInstance(typeof(InitData));
                        fieldInfo.SetValue(dataInstance, instance);
                    }
                    string initCellData = dataRow[keyValuePair.Value].ToString();
                    InitData.SetParserData(instance, initCellData);
                    break;
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
                default:
                    StringParserHelper.BuiltInTypeParser<T>(dataInstance, fieldType, fieldInfo.Name, dataRow[keyValuePair.Value]);
                    break;
            }
        }
    }
}
