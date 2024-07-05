using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEffectModule
{
    private Mediator _mediator;
    
    //트리거 조건
    private Trigger _trigger;
    //컨디션 조건
    private List<ConditionModule> _conditionModules;
    //효과
    private List<EffectModule> _effectModules;



    public void InitData(ConditionEffectData conditionEffectData, Mediator mediator)
    {
        //TODO
        _mediator = mediator;

        _trigger = conditionEffectData.trigger;
        /*foreach (var conditionData in conditionEffectData.conditionDatas)
        {
            ConditionModule condition;
            switch (conditionData.Key)
            {
                case "RandomChance":
                    condition = new Condition_RandomChance();
                    condition.InitData(conditionData.Value, _mediator);
                    break;
                case "RequestRecordValue":
                    condition = new Condition_RequestRecordValue<>();
                    condition.InitData(conditionData.Value, _mediator);
                    break;
                case "RequestReference":
                    condition = new Condition_RequestReference<>();
                    condition.InitData(conditionData.Value, _mediator);
                    break;
            }
            if(condition == null)
                return;
            condition.InitData(conditionData.Value, _mediator);
            _conditionModules.Add(condition);
        }*/
        foreach (var effectDatas in conditionEffectData.effectDatas)
        {
            EffectModule effectModule;
            switch (effectDatas.Key)
            {
                
            }

            //EffectModule effectModule = new EffectModule();
            //effectModule.InitData(effectDatas);
            //_effectModules.Add(effectModule);
        }
        
        
        //EffectModule effectModule = new EffectModule();
        //effectModule.InitData(referenceData);
        //_effectModules.Add(effectModule);
    }
    public void CheakTrigger(Trigger trigger)
    {
        if (((trigger & _trigger) == _trigger) && CheakCondition())
            InvokeEffect();
        else
            CancelEffect();
    }
    public void OnUpdate()
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
    private void InvokeEffect()
    {
        foreach (var effectModule in _effectModules)
        {
            effectModule.InvokeEffect();
        }
    }
    private void CancelEffect()
    {
        foreach (var effectModule in _effectModules)
        {
            effectModule.CancelEffect();
        }
    }
}
