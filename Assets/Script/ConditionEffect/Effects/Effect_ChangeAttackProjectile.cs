using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_ChangeAttackProjectile : EffectModule
{
    private Mediator _mediator;
    
    [SerializeField, ReadOnly]
    private string _projectileName;
    
    public override void InitData(string effectData, Mediator mediator)
    {
        _mediator = mediator;

        _projectileName = effectData;
    }

    public override void InvokeEffect()
    {
        PlayerManager.Instance.Player.WeaponHandler.SetOverrideProjectilePrefab(_projectileName);
    }

    public override void CancelEffect()
    {
        PlayerManager.Instance.Player.WeaponHandler.SetOverrideProjectilePrefab();
    }
}