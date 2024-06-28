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

    public void OnDamage(float damage)
    {

    }
}
