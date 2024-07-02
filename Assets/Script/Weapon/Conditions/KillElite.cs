using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillElite : Condition
{
    public override bool CheakCondition(Player owner, WeaponHandler handler)
    {
        return true;
    }
}
