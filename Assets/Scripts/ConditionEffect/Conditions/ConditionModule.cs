using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ConditionModule
{
    public abstract void InitData(string conditionData, Mediator mediator);
    public abstract bool CheakCondition();
}
