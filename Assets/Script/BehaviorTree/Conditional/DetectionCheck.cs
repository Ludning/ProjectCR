using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("CustomConditional")]
public class DetectionCheck : Conditional
{
	private DetectTarget DetectTarget;
	public override void OnAwake()
	{
		DetectTarget = gameObject.GetComponent<Animal>().DetectTarget;
	}


	public override TaskStatus OnUpdate()
	{
		if (DetectTarget.TargetCount > 0)
		{
			Debug.Log("타겟 존재");
			return TaskStatus.Success;
		}
		
		Debug.Log("타겟 없음");
		return TaskStatus.Failure;
	}
}