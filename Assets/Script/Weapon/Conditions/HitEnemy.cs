using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : Condition
{
    public override bool CheakCondition(Player owner, WeaponHandler handler)
    {
        return true;
    }
}
