using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomConditional")]
public class TargetDistanceCheak : Conditional
{
    public SharedTransform Target;
    public float _attackRange = 2f;
    
    public override void OnAwake()
    {
    }

    public override TaskStatus OnUpdate()
    {
        if(Target == null)
            return TaskStatus.Failure;
        
        float distance = Vector3.Distance(Owner.transform.position, Target.Value.position);
        
        return distance >= _attackRange ? TaskStatus.Success : TaskStatus.Failure;
    }
}
