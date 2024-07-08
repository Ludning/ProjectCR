using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class EffectModule
{
    public bool IsActive { get; set; }
    public abstract void InitData(string effectData, Mediator mediator);
    public abstract void InvokeEffect();
    public abstract void CancelEffect();
}
