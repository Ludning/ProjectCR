using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Specificity : Mediator
{
    private List<ConditionEffectModule> _conditionEffectModules = new List<ConditionEffectModule>();
    private Dictionary<string, RecordModule> _recordModules = new Dictionary<string, RecordModule>();
    private Dictionary<string, ReferenceModule> _referenceModules = new Dictionary<string, ReferenceModule>();
    
    public void InitData(SpecificityData archetypeData, Player owner)
    {
        Owner = owner;
        
        if (archetypeData.recordDatas != null)
        {
            foreach (var recordData in archetypeData.recordDatas)
            {
                RecordModule recordModule = new RecordModule();
                recordModule.InitData(recordData);
                _recordModules.Add(recordData.RecordName, recordModule);
            }
        }

        if (archetypeData.referenceDatas != null)
        {
            foreach (var referenceData in archetypeData.referenceDatas)
            {
                ReferenceModule referenceModule = new ReferenceModule();
                referenceModule.InitData(referenceData);
                _referenceModules.Add(referenceData.ReferenceName, referenceModule);
            }
        }

        if (archetypeData.conditionEffectDatas != null)
        {
            foreach (var conditionEffectData in archetypeData.conditionEffectDatas)
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
        _referenceModules.Clear();
    }
    public void OnUpdate()
    {
        foreach (var recordModule in _recordModules.Values)
            recordModule.OnUpdate();
        foreach (var conditionEffectModule in _conditionEffectModules)
            conditionEffectModule.OnUpdate();
    }
    public void CheakTrigger(Trigger trigger)
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
        {
            conditionEffectModule.CheakTrigger(trigger);
        }
    }

    public override int GetData(string name, DataModuleType type)
    {
        switch (type)
        {
            case DataModuleType.Record:
                if (_recordModules.TryGetValue(name, out RecordModule recordModule))
                    return recordModule.GetValue();
                break;
            case DataModuleType.Reference:
                if (_referenceModules.TryGetValue(name, out ReferenceModule referenceModule))
                    return referenceModule.GetValue();
                break;
        }
        return int.MaxValue;
    }
    public override void AddRecordData(string name, int value, RecordDataType type)
    {
        if (_recordModules.TryGetValue(name, out RecordModule recordModule))
        {
            switch (type)
            {
                case RecordDataType.Value:
                    recordModule.AddValue(value);
                    break;
                case RecordDataType.Duration:
                    recordModule.AddDuration(value);
                    break;
            }
        }
    }
    public override void SetRecordData(string name, int value, RecordDataType type)
    {
        if (_recordModules.TryGetValue(name, out RecordModule recordModule))
        {
            switch (type)
            {
                case RecordDataType.Value:
                    recordModule.SetValue(value);
                    break;
                case RecordDataType.Duration:
                    recordModule.SetDuration(value);
                    break;
            }
        }
    }
}
