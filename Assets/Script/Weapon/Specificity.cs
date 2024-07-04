using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Specificity
{
    private List<ConditionEffectModule> _conditionEffectModules;
    private Dictionary<string, RecordModule> _recordModules;
    private Dictionary<string, ReferenceModule> _referenceModules;
    
    public void InitData(string specificityData)
    {
        
    }
    public void SetTrigger(Trigger trigger)
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
        {
            conditionEffectModule.CheakTrigger(trigger);
        }
    }
    public void CheakCondition()
    {
        foreach (var conditionEffectModule in _conditionEffectModules)
        {
            
        }
    }
}
