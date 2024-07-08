using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Archetype : Mediator
{
    private List<ConditionEffectModule> _conditionEffectModules = new List<ConditionEffectModule>();
    private Dictionary<string, RecordModule> _recordModules = new Dictionary<string, RecordModule>();
    private Dictionary<string, ReferenceModule> _referenceModules = new Dictionary<string, ReferenceModule>();

    public void InitData(ArchetypeData archetypeData)
    {
        if (archetypeData.recordDatas != null)
        {
            foreach (var recordData in archetypeData.recordDatas)
            {
                RecordModule recordModule = new RecordModule();
                recordModule.InitData(recordData, this);
                _recordModules.Add(recordData.RecordName, recordModule);
            }
        }

        if (archetypeData.referenceDatas != null)
        {
            foreach (var referenceData in archetypeData.referenceDatas)
            {
                ReferenceModule referenceModule = new ReferenceModule();
                referenceModule.InitData(referenceData, this);
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
    /*public void OnUpdate()
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
            conditionEffectModule.OnUpdate();
    }*/
    
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
                    return recordModule.RecordValue;
                break;
            case DataModuleType.Reference:
                if (_referenceModules.TryGetValue(name, out ReferenceModule referenceModule))
                    return referenceModule.GetValue();
                break;
        }
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
