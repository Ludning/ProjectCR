using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConditionEffectModule
{
    private Mediator _mediator;

    //트리거 조건
    [SerializeField]
    private Trigger _trigger;

    //컨디션 조건
    [SerializeField]
    private List<ConditionModule> _conditionModules = new List<ConditionModule>();

    //효과
    [SerializeField]
    private List<EffectModule> _effectModules = new List<EffectModule>();



    public void InitData(ConditionEffectData conditionEffectData, Mediator mediator)
    {
        //TODO
        _mediator = mediator;

        _trigger = conditionEffectData.trigger;
        if (conditionEffectData.conditionDatas != null)
        {
            foreach (var conditionData in conditionEffectData.conditionDatas)
            {
                ConditionModule condition = conditionData.Key switch
                {
                    "RandomChance" => new Condition_RandomChance(),
                    "RequestRecordValue" => new Condition_RequestRecordValue(),
                    "RequestReference" => new Condition_RequestReference(),
                    _ => null
                };
                if (condition == null)
                    return;
                condition.InitData(conditionData.Value, _mediator);
                _conditionModules.Add(condition);
            }
        }

        if (conditionEffectData.effectDatas != null)
        {
            foreach (var effectDatas in conditionEffectData.effectDatas)
            {
                EffectModule effectModule = effectDatas.Key switch
                {
                    "IncreasedStat" => new Effect_IncreasedStat(),
                    "AddRecordValue" => new Effect_AddRecordValue(),
                    "ChangeAttackProjectile" => new Effect_ChangeAttackProjectile(),
                    "CooldownReduction" => new Effect_CooldownReduction(),
                    "ChangeWeaponDamageTypeByReference" => new Effect_ChangeWeaponDamageTypeByReference(),
                    "PushTarger" => new Effect_PushTarger(),
                    "SetDebuffTarget" => new Effect_SetDebuffTarget(),
                    "SetRecordValue" => new Effect_SetRecordValue(),
                    "SpawnObject" => new Effect_SpawnObject(),
                    _ => null
                };
                if (effectModule == null)
                    return;
                effectModule.InitData(effectDatas.Value, _mediator);
                _effectModules.Add(effectModule);
            }
        }
    }

    public void CheakTrigger(Trigger trigger)
    {
        Debug.Log(trigger);
        Debug.Log(_trigger);
        Debug.Log(trigger & _trigger);
        Debug.Log((trigger & _trigger) == _trigger);
        Debug.Log(CheakCondition());
        if (((trigger & _trigger) == _trigger) && CheakCondition())
            InvokeEffect();
        else
            CancelEffect();
    }

    public void OnChangedRecordData()
    {
        if (_trigger != Trigger.None)
            return;
        if (CheakCondition() == true)
            InvokeEffect();
        else
            CancelEffect();
    }

    private bool CheakCondition()
    {
        foreach (var conditionModule in _conditionModules)
        {
            if (conditionModule.CheakCondition() == false)
                return false;
        }

        return true;
    }

    public void InvokeEffect()
    {
        foreach (var effectModule in _effectModules)
        {
            effectModule.InvokeEffect();
        }
    }

    public void CancelEffect()
    {
        foreach (var effectModule in _effectModules)
        {
            effectModule.CancelEffect();
        }
    }
}
