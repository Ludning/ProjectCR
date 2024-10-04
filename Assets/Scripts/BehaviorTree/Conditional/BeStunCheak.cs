using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomConditional")]
public class BeStunCheak : Conditional
{
    public SharedBool isBeStun = false;
    public override void OnAwake()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        return (isBeStun.Value == true) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
