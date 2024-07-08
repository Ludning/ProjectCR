using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Mediator
{
    [SerializeField, ReadOnly]
    private List<ConditionEffectModule> _conditionEffectModules;
    [SerializeField, ReadOnly]
    private Dictionary<string, RecordModule> _recordModules;
    [SerializeField, ReadOnly]
    private Dictionary<string, ReferenceModule> _referenceModules;
    
    public void InitData(SkillData skillData)
    {
        foreach (var recordData in skillData.recordDatas)
        {
            RecordModule recordModule = new RecordModule();
            recordModule.InitData(recordData, this);
            _recordModules.Add(recordData.RecordName, recordModule);
        }

        foreach (var referenceData in skillData.referenceDatas)
        {
            ReferenceModule referenceModule = new ReferenceModule();
            referenceModule.InitData(referenceData, this);
            _referenceModules.Add(referenceData.ReferenceName, referenceModule);
        }
        
        foreach (var conditionEffectData in skillData.conditionEffectDatas)
        {
            ConditionEffectModule conditionEffectModule = new ConditionEffectModule();
            conditionEffectModule.InitData(conditionEffectData, this);
            _conditionEffectModules.Add(conditionEffectModule);
        }
    }
    public void OnUpdate()
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
            conditionEffectModule.OnChangedRecordData();
    }
    public void SetTrigger(Trigger trigger)
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
