using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("CustomConditional")]
public class DetectionCheck : Conditional
{
	public SharedDetectTarget DetectTarget;
	public SharedTransform Target;

	public override TaskStatus OnUpdate()
	{
		if (DetectTarget.Value.TargetCount > 0)
		{
			Target.Value = DetectTarget.Value.Target;
			return TaskStatus.Success;
		}
		
		Target.Value = null;
		return TaskStatus.Failure;
	}
}