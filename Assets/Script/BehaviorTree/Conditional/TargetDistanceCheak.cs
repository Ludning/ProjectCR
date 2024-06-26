using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomConditional")]
public class TargetDistanceCheak : Conditional
{
    private DetectTarget DetectTarget;
    public float _attackRange = 2f;
    
    public override void OnAwake()
    {
        DetectTarget = gameObject.GetComponent<Animal>().DetectTarget;
    }


    public override TaskStatus OnUpdate()
    {
        if(DetectTarget.Target == null)
            return TaskStatus.Failure;
        
        float distance = Vector3.Distance(Owner.transform.position, DetectTarget.Target.position);
        
        return distance >= _attackRange ? TaskStatus.Success : TaskStatus.Failure;
    }
}
