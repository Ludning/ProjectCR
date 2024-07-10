using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Tremors : MonoBehaviour
{
    private async void Start()
    {
        await UniTask.Delay(2000);
        PoolManager.Instance.ReturnToPool(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<MonsterStats>();
    }
}
