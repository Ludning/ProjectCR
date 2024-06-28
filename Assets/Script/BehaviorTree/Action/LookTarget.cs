using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


[TaskCategory("CustomAction")]
public class LookTarget : Action
{
    public SharedTransform Target;
    public SharedFloat rotationSpeed;


    // Seek the destination. Return success once the agent has reached the destination.
    // Return running if the agent hasn't reached the destination yet
    public override TaskStatus OnUpdate()
    {
        return SmoothLookAtTargetY(Target.Value.position, rotationSpeed.Value) 
            ? TaskStatus.Success 
            : TaskStatus.Running;
    }

    /// <summary>
    /// 목표를 부드럽게 바라보도록 Y축으로만 회전하고, 목표를 정확히 바라보고 있을 때 true를 반환합니다.
    /// </summary>
    /// <param name="targetPosition">목표 위치</param>
    /// <param name="rotationSpeed">회전 속도</param>
    /// <returns>목표를 정확히 바라보고 있을 때 true</returns>
    bool SmoothLookAtTargetY(Vector3 targetPosition, float rotationSpeed)
    {
        // 현재 위치와 목표 위치의 방향을 구합니다.
        Vector3 direction = targetPosition - transform.position;
        // 방향 벡터의 y 값을 0으로 만들어 평면상에서의 방향만 남깁니다.
        direction.y = 0;

        // 방향 벡터가 0인 경우, (즉, 목표가 바로 위 또는 아래에 있는 경우), 바로 반환
        if (direction == Vector3.zero)
        {
            return false;
        }

        // 현재의 회전 방향에서 목표 회전 방향으로의 보간을 구합니다.
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 목표 회전 방향으로 부드럽게 회전합니다.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 목표를 정확히 바라보고 있는지 확인합니다.
        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        // 목표를 거의 정확히 바라보고 있으면 true를 반환합니다.
        return angle < 0.1f;
    }
}
