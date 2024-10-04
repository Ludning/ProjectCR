using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomConditional")]
public class CheakState : Conditional
{
    public SharedUnitState UnitState;
    public UnitState cheakState;
    

    public override TaskStatus OnUpdate()
    {
        return (UnitState.Value == cheakState) 
            ? TaskStatus.Success 
            : TaskStatus.Failure;
    }
}
