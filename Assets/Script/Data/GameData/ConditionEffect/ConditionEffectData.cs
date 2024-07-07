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
                /*switch (effectResult)
                {
                    case EffectType.SetRecordValue:
                        StringParserHelper.SetValueToKeyValueData<ConditionEffectData>(genericInstance, "effectDatas", conditionAndEffects.Key, conditionAndEffects.Value);
                        break;
                    case EffectType.IncreasedStat:
                        StringParserHelper.SetValueToKeyValueData<ConditionEffectData>(genericInstance, "effectDatas", conditionAndEffects.Key, conditionAndEffects.Value);
                        break;
                    case EffectType.SpawnObject:
                        StringParserHelper.SetValueToKeyValueData<ConditionEffectData>(genericInstance, "effectDatas", conditionAndEffects.Key, conditionAndEffects.Value);
                        break;
                    /*case EffectType.SetRecordValueByReference,
                    case EffectType.SetRecordDuration,
                    case EffectType.SpawnProjectile,
                    case EffectType.ProjectilePushForce,
                    case EffectType.ProjectilePushRange,
                    case EffectType.ChangeAttackProjectile,
                    case EffectType.ProjectileCount,
                    case EffectType.ChargeMinAngle,
                    case EffectType.ChargeMaxAngle,
                    case EffectType.SetDebuffTarget,
                    case EffectType.ChangeWeaponDamageType,
                    case EffectType.ChangeWeaponDamageTypeByReference,
                    case EffectType.CooldownReduction,#1#
                }*/
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