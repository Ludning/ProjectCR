using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, ReadOnly] 
    private Player Owner;
    private WeaponHandler Handler;
    
    [SerializeField, ReadOnly]
    private Archetype _archetype;
    [SerializeField, ReadOnly]
    private Specificity _specificity;

    private void FixedUpdate()
    {
        
    }


    public void InitWeapon(Player owner, WeaponHandler handler)
    {
        Owner = owner;
        Handler = handler;
    }

    public void ReceptionWeaponHandlerEvent(Trigger type)
    {
        //_archetypeEffects.ReceptionArchetypeEvent(type);
    }

    #region WeaponLogic
    public void OnAttack()
    {
        
    }
    public void OnSubAttack()
    {
        
    }
    public void OnAim()
    {
        
    }
    #endregion
}
