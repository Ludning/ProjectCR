using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("CustomAction")]
public class SetPatrolPosition : Action
{
    public SharedVector3 PatrolPosition; // 순찰 범위
    public SharedFloat PatrolRadius; // 순찰 범위
    public SharedFloat PatrolDelay;   // 다음 위치로 이동하기 전 대기 시간


    public override TaskStatus OnUpdate()
    {
        PatrolPosition.Value = GetRandomNavMeshPoint(transform.position, PatrolRadius.Value);
        return TaskStatus.Success;
        return TaskStatus.Failure;
    }
    
    /// <summary>
    /// 중심에서 일정 반경 내에서 NavMesh 위의 무작위 위치를 반환합니다.
    /// </summary>
    /// <param name="center">중심 위치</param>
    /// <param name="radius">반경</param>
    /// <returns>NavMesh 위의 무작위 위치</returns>
    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;

        // NavMesh에서 무작위로 위치를 선택합니다.
        if (NavMesh.SamplePosition(RandomNavMeshLocation(center, radius), out hit, radius, NavMesh.AllAreas))
        {
            randomPoint = hit.position;
        }
        return randomPoint;
    }

    /// <summary>
    /// NavMesh 내의 무작위 위치를 반환합니다.
    /// </summary>
    /// <param name="center">중심 위치</param>
    /// <param name="radius">반경</param>
    /// <returns>무작위 위치</returns>
    private Vector3 RandomNavMeshLocation(Vector3 center, float radius)
    {
        NavMeshHit navHit;
        // NavMesh 위에서 무작위 방향으로 확장된 영역을 설정합니다.
        Vector3 randomDirection = Random.insideUnitSphere * radius + center;

        // 지정된 반경 내에서 무작위 위치를 찾습니다.
        if (NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas))
        {
            return navHit.position;
        }
        return Vector3.zero;
    }
}
