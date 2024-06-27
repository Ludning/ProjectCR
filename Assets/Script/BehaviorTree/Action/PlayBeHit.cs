using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("CustomAction")]
public class PlayBeHit : Action
{
    private static readonly int IsDamaged = Animator.StringToHash("IsDamaged");
    private Animator _animator;
    public override void OnAwake()
    {
        _animator = Owner.gameObject.GetComponent<Animator>();
    }
    
    public override TaskStatus OnUpdate()
    {
        _animator.SetTrigger(IsDamaged);
        return TaskStatus.Success;
    }
}
