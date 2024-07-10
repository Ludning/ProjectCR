using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomAction")]
public class AttackPattern : Action
{
    [SerializeField] private string attackPattern = "짱쎈 박치기";
    public override void OnAwake()
    {
        
    }

    public override void OnStart()
    {
        
    }
    
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
        return TaskStatus.Running;
    }
}
