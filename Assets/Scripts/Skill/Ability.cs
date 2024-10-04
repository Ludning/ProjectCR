using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Mediator
{
    [SerializeField, ReadOnly]
    private List<ConditionEffectModule> _conditionEffectModules;
    [SerializeField, ReadOnly]
    private Dictionary<string, RecordModule> _recordModules;
    
    public void InitData(SkillData skillData)
    {
        foreach (var recordData in skillData.recordDatas)
        {
            RecordModule recordModule = new RecordModule();
            recordModule.InitData(recordData, this);
            _recordModules.Add(recordData.RecordName, recordModule);
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
            Debug.Log("AddRecordData");
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
