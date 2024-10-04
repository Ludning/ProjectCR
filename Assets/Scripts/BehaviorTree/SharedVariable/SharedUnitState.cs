using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;


[System.Serializable]
public class SharedUnitState : SharedVariable<UnitState>
{
    public static implicit operator SharedUnitState(UnitState value) { return new SharedUnitState { Value = value }; }
}
