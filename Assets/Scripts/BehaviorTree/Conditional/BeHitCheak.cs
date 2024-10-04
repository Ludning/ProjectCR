using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomConditional")]
public class BeHitCheak : Conditional
{
    public SharedBool isDamaged = false;
    public override void OnAwake()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        return (isDamaged.Value == true) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
