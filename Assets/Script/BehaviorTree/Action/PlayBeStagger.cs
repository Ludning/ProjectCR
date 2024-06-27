using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomAction")]
public class PlayBeStagger : Action
{
    private static readonly int IsStun = Animator.StringToHash("IsStun");
    private Animator _animator;
    public override void OnAwake()
    {
        _animator = Owner.gameObject.GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        _animator.SetBool(IsStun, true);
        return TaskStatus.Success;
    }
}
