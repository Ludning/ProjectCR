using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Unity/NavMeshAgent")]
[TaskDescription("Apply relative movement to the current position. Returns Success.")]
public class TrakingTargetTwo : Action
{
    [UnityEngine.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
    public SharedGameObject targetGameObject;
    [UnityEngine.Tooltip("The relative movement vector")]
    public SharedVector3 offset;

    // cache the navmeshagent component
    private NavMeshAgent navMeshAgent;
    private GameObject prevGameObject;

    public override void OnStart()
    {
        var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
        if (currentGameObject != prevGameObject) {
            navMeshAgent = currentGameObject.GetComponent<NavMeshAgent>();
            prevGameObject = currentGameObject;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (navMeshAgent == null) {
            Debug.LogWarning("NavMeshAgent is null");
            return TaskStatus.Failure;
        }

        navMeshAgent.Move(offset.Value);

        return TaskStatus.Success;
    }

    public override void OnReset()
    {
        targetGameObject = null;
        offset = Vector3.zero;
    }
}