using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;


[TaskCategory("Initialize")]
public class MonsterBTInitialize : Action
{
    // Component references
    public SharedDetectTarget DetectTarget;
    public SharedNavmeshAgent NavMeshAgent;
    
    public override TaskStatus OnUpdate()
    {
        Animal animal = Owner.GetComponent<Animal>();
        NavMeshAgent agent = Owner.GetComponent<NavMeshAgent>();
        if(animal == null || animal.DetectTarget == null || agent == null)
            return TaskStatus.Failure;

        DetectTarget.Value = animal.DetectTarget;
        NavMeshAgent.Value = agent;
        return TaskStatus.Success;
    }
}
