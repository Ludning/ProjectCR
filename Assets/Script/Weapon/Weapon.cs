using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    [SerializeField, ReadOnly] 
    private Player Owner;
    private WeaponHandler Handler;
    
    [SerializeField, ReadOnly]
    private Archetype _archetype;
    [SerializeField, ReadOnly]
    private Specificity _specificity;
    
    private bool _isInitialized = false;

    public void OnUpdate()
    {
        if(_isInitialized == false)
            return;
        
        if(_archetype != null)
            _archetype.OnUpdate();
        if(_specificity != null)
            _specificity.OnUpdate();
    }


    public void InitWeapon(Player owner, WeaponHandler handler, Item item)
    {
        Owner = owner;
        Handler = handler;

        GameData data = DataManager.Instance.GetGameData();
        if(data.ArchetypeData.TryGetValue(item.archetypeID, out ArchetypeData archetypeData))
        {
            _archetype = new Archetype();
            _archetype.InitData(archetypeData, owner);
        }
        if(data.SpecificityData.TryGetValue(item.specificityID, out SpecificityData specificityData))
        {
            _specificity = new Specificity();
            _specificity.InitData(specificityData, owner);
        }

        _isInitialized = true;
    }

    public void UnInstallWeapon()
    {
        _isInitialized = false;
        
        if (_archetype != null)
        {
            Archetype tempArchetype = _archetype;
            _archetype = null;
            tempArchetype.UnInstall();
        }
        if (_specificity != null)
        {
            Specificity tempSpecificity = _specificity;
            _specificity = null;
            tempSpecificity.UnInstall();
        }
    }

    public void ReceptionHandlerEvent(Trigger trigger)
    {
        if(_isInitialized == false)
            return;
        
        _archetype.CheakTrigger(trigger);
        _specificity.CheakTrigger(trigger);
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
