using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomConditional")]
public class IsTargetInAttackRange : Conditional
{
    public SharedTransform Target;
    public float _attackRange = 5f;
    private float RangeSquare => Mathf.Pow(_attackRange, 2);
    
    public override void OnAwake()
    {
    }


    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            Debug.Log("타겟 존재 안함");
            return TaskStatus.Failure;
        }
        Vector3 dirVec = transform.position - Target.Value.position;
        float distanceX = Mathf.Pow(dirVec.x, 2);
        float distanceY = Mathf.Pow(dirVec.y, 2);
        float distanceZ = Mathf.Pow(dirVec.z, 2);

        float distance = distanceX + distanceY + distanceZ;
        
        if (distance < RangeSquare)
        {
            return TaskStatus.Success;
        }
		
        return TaskStatus.Failure;
    }
}
