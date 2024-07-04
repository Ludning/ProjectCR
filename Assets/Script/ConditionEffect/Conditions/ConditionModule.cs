using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionModule
{
    public abstract void InitData(string conditionData);
    public abstract bool CheakCondition();
}
