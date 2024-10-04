using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class SharedDetectTarget : SharedVariable<DetectTarget>
{
    public static implicit operator SharedDetectTarget(DetectTarget value) { return new SharedDetectTarget { Value = value }; }
}
