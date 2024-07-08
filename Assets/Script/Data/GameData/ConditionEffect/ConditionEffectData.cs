using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public class ConditionEffectData
{
    public Trigger trigger;
    [ShowInInspector]
    public List<KeyValueData<string, string>> conditionDatas;
    [ShowInInspector]
    public List<KeyValueData<string, string>> effectDatas;
    
    
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
                        StringParserHelper.SetValueToEnumFlag<ConditionEffectData, Trigger>(genericInstance, "trigger", conditionAndEffects.Value);
                        break;
                    case ConditionType.RequestRecordValue:
                        //StringParserHelper.SetValueToList<ConditionEffectData>(genericInstance, typeof(string), "conditionDatas", effectUnit);
                        StringParserHelper.SetValueToKeyValueData<ConditionEffectData>(genericInstance, "conditionDatas", conditionAndEffects.Key, conditionAndEffects.Value);
                        break;
                }
            }
            else if (Enum.TryParse<EffectType>(conditionAndEffects.Key, out EffectType effectResult))
            {
                StringParserHelper.SetValueToKeyValueData<ConditionEffectData>(genericInstance, "effectDatas", conditionAndEffects.Key, conditionAndEffects.Value);
            }
        }
        
        Type listType = typeof(List<>).MakeGenericType(genericType);
        MethodInfo addMethod = listType.GetMethod("Add");
        addMethod.Invoke(listInstance, new object[] { genericInstance });
    }
}