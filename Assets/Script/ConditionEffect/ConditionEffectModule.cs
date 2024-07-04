using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEffectModule
{
    //트리거 조건
    private Trigger _trigger;
    //컨디션 조건
    private List<ConditionModule> _conditionModules;
    //효과
    private List<EffectModule> _effectModules;

    public void CheakTrigger(Trigger trigger)
    {
        if (_trigger == trigger && CheakCondition())
        {
            InvokeEffect();
        }
        else
        {
            CancelEffect();
        }
    }
    public bool CheakCondition()
    {
        if (_trigger == Trigger.None)
            return false;
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
