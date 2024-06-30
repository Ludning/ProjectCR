using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Animal : MonoBehaviour, IDamageable
{
    private MonsterStats _stats;
    
    public DetectTarget DetectTarget;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _stats = new MonsterStats();
    }

    public void OnDamage(float damageValue)
    {

    }
}
