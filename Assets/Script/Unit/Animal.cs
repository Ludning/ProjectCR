using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Animal : MonoBehaviour
{
    public DetectTarget DetectTarget;

    [SerializeField] private Animator _animator;
    private static readonly int IsWalk = Animator.StringToHash("IsWalk");
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsRoll = Animator.StringToHash("IsRoll");
    
    private static readonly int IsSpin = Animator.StringToHash("IsSpin");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsStun = Animator.StringToHash("IsStun");
    private static readonly int IsDamaged = Animator.StringToHash("IsDamaged");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    /*private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetDestination(Vector3.zero);
    }
    private bool SetDestination(Vector3 destination)
    {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
            navMeshAgent.Resume();
#else
        agent.isStopped = false;
#endif
        return agent.SetDestination(destination);
    }*/


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetBool(IsWalk, true);
        }
        else if (context.canceled)
        {
            _animator.SetBool(IsWalk, false);
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetBool(IsRun, true);
        }
        else if (context.canceled)
        {
            _animator.SetBool(IsRun, false);
        }
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetBool(IsRoll, true);
        }
        else if (context.canceled)
        {
            _animator.SetBool(IsRoll, false);
        }
    }
    public void OnSpin(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetBool(IsSpin, true);
        }
        else if (context.canceled)
        {
            _animator.SetBool(IsSpin, false);
        }
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(IsAttack);
    }
    public void OnStun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetBool(IsStun, true);
        }
        else if (context.canceled)
        {
            _animator.SetBool(IsStun, false);
        }
    }
    public void OnDamaged(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(IsDamaged);
    }
    public void OnDie(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(IsDie);
    }
    

    public void OnDamage(float damage)
    {

    }
}
