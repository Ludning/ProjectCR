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
    
    public void InitData(SkillData skillData, Player owner)
    {
        Owner = owner;
        
        foreach (var recordData in skillData.recordDatas)
        {
            RecordModule recordModule = new RecordModule();
            recordModule.InitData(recordData);
            _recordModules.Add(recordData.RecordName, recordModule);
        }

        foreach (var referenceData in skillData.referenceDatas)
        {
            ReferenceModule referenceModule = new ReferenceModule();
            referenceModule.InitData(referenceData);
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
