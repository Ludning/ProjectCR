using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("CustomConditional")]
public class DetectionCheck : Conditional
{
	private DetectTarget DetectTarget;
	public SharedTransform Target;
	public override void OnAwake()
	{
		DetectTarget = gameObject.GetComponent<Animal>().DetectTarget;
	}


	public override TaskStatus OnUpdate()
	{
		if (DetectTarget.TargetCount > 0)
		{
			Target.Value = DetectTarget.Target;
			return TaskStatus.Success;
		}
		
		Target.Value = null;
		return TaskStatus.Failure;
	}
}