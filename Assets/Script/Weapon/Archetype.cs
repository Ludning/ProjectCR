using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Archetype : Mediator
{
    private List<ConditionEffectModule> _conditionEffectModules;
    private Dictionary<string, RecordModule> _recordModules;
    private Dictionary<string, ReferenceModule> _referenceModules;

    public void InitData(ArchetypeData archetypeData)
    {
        foreach (var recordData in archetypeData.recordDatas)
        {
            RecordModule recordModule = new RecordModule();
            recordModule.InitData(recordData);
        }

        foreach (var referenceData in archetypeData.referenceDatas)
        {
            ReferenceModule referenceModule = new ReferenceModule();
            referenceModule.InitData(referenceData);
        }
        
        foreach (var conditionEffectData in archetypeData.conditionEffectDatas)
        {
            ConditionEffectModule cem = new ConditionEffectModule();
            cem.InitData(conditionEffectData, this);
        }
    }
    public void OnUpdate()
    {
        foreach (var recordModule in _recordModules.Values)
            recordModule.OnUpdate();
        foreach (var conditionEffectModule in _conditionEffectModules)
            conditionEffectModule.OnUpdate();
    }
    public void SetTrigger(Trigger trigger)
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
        {
            conditionEffectModule.CheakTrigger(trigger);
        }
    }
    public override int DataTransfer(string name, DataModuleType type)
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
}
