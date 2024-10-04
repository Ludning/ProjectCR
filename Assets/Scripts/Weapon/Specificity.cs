using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Specificity : Mediator
{
    [SerializeField]
    private List<ConditionEffectModule> _conditionEffectModules = new List<ConditionEffectModule>();
    [TableList] 
    private Dictionary<string, RecordModule> _recordModules = new Dictionary<string, RecordModule>();
    
    public void InitData(SpecificityData specificityData)
    {
        if (specificityData.recordDatas != null)
        {
            foreach (var recordData in specificityData.recordDatas)
            {
                RecordModule recordModule = new RecordModule();
                recordModule.InitData(recordData, this);
                _recordModules.Add(recordData.RecordName, recordModule);
            }
        }
        if (specificityData.conditionEffectDatas != null)
        {
            foreach (var conditionEffectData in specificityData.conditionEffectDatas)
            {
                ConditionEffectModule conditionEffectModule = new ConditionEffectModule();
                conditionEffectModule.InitData(conditionEffectData, this);
                _conditionEffectModules.Add(conditionEffectModule);
            }
        }
        
    }
    public void UnInstall()
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
        {
            conditionEffectModule.CancelEffect();
        }
        _conditionEffectModules.Clear();
        _recordModules.Clear();
    }
    public void CheakTrigger(Trigger trigger)
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
        {
            conditionEffectModule.CheakTrigger(trigger);
        }
    }

    public override int GetRecordData(string name)
    {
        if (_recordModules.TryGetValue(name, out RecordModule recordModule))
            return recordModule.RecordValue;
        return int.MaxValue;
    }
    public override void AddRecordData(string name, int value)
    {
        if (_recordModules.TryGetValue(name, out RecordModule recordModule))
        {
            recordModule.RecordValue += value;
        }
    }
    public override void SetRecordData(string name, int value)
    {
        if (_recordModules.TryGetValue(name, out RecordModule recordModule))
        {
            recordModule.RecordValue = value;
        }
    }
    public override void OnChangedRecordData()
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
            conditionEffectModule.OnChangedRecordData();
    }

}
