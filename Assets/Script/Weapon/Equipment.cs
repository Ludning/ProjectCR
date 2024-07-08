using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    [SerializeField, ReadOnly]
    private Archetype _archetype;
    [SerializeField, ReadOnly]
    private Specificity _specificity;
    

    public void Install(Item item)
    {
        GameData data = DataManager.Instance.GetGameData();
        if(data.ArchetypeData.TryGetValue(item.archetypeID, out ArchetypeData archetypeData))
        {
            _archetype = new Archetype();
            _archetype.InitData(archetypeData);
        }
        if(data.SpecificityData.TryGetValue(item.specificityID, out SpecificityData specificityData))
        {
            _specificity = new Specificity();
            _specificity.InitData(specificityData);
        }
    }

    public void UnInstall()
    {
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
