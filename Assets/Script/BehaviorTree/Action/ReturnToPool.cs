using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ReturnToPool : Action
{
    public override TaskStatus OnUpdate()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
        return TaskStatus.Success;
    }
}
