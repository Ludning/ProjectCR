using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public abstract bool CheakCondition(Player owner, WeaponHandler handler);
}
