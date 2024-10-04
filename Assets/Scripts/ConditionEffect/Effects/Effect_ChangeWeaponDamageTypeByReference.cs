using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_ChangeWeaponDamageTypeByReference : EffectModule
{
    private Mediator _mediator;

    [SerializeField, ReadOnly]
    private SkillSlotType _skillType;

    public override void InitData(string effectData, Mediator mediator)
    {
        _mediator = mediator;
        _skillType = Enum.Parse<SkillSlotType>(effectData);
    }

    public override void InvokeEffect()
    {
        
    }

    public override void CancelEffect()
    {
        
    }
}