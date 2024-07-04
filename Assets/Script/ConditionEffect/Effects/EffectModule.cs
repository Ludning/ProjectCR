using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class EffectModule
{
    public abstract void InitData(string effectData);
    public abstract void InvokeEffect();
    public abstract void CancelEffect();
}
