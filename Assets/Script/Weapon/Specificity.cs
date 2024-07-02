using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Specificity
{
    public static Specificity SpecificityFactory(SpecificityType specificityType)
    {
        Specificity archetypeInstance;
        switch (specificityType)
        {
            case SpecificityType.Discord:
                archetypeInstance = new Specificity_Discord();
                break;
            case SpecificityType.AvoidAccess:
                archetypeInstance = new Specificity_AvoidAccess();
                break;
            case SpecificityType.Instability:
                archetypeInstance = new Specificity_Instability();
                break;
            case SpecificityType.PhysicalVibration:
                archetypeInstance = new Specificity_PhysicalVibration();
                break;
            case SpecificityType.PermanentMovement:
                archetypeInstance = new Specificity_PermanentMovement();
                break;
            case SpecificityType.CloseRangeExecution:
                archetypeInstance = new Specificity_CloseRangeExecution();
                break;
            case SpecificityType.Tombstone:
                archetypeInstance = new Specificity_Tombstone();
                break;
            case SpecificityType.Ensemble:
                archetypeInstance = new Specificity_Ensemble();
                break;
            case SpecificityType.InfiniteSadness:
                archetypeInstance = new Specificity_InfiniteSadness();
                break;
            case SpecificityType.ChainReaction:
                archetypeInstance = new Specificity_ChainReaction();
                break;
            case SpecificityType.OneForAll:
                archetypeInstance = new Specificity_OneForAll();
                break;
            case SpecificityType.Fluctuation:
                archetypeInstance = new Specificity_Fluctuation();
                break;
            case SpecificityType.OsmoticPower:
                archetypeInstance = new Specificity_OsmoticPower();
                break;
            case SpecificityType.Meganeura:
                archetypeInstance = new Specificity_Meganeura();
                break;
            case SpecificityType.Onslaught:
                archetypeInstance = new Specificity_Onslaught();
                break;
            case SpecificityType.DegradableDestructionDevice:
                archetypeInstance = new Specificity_DegradableDestructionDevice();
                break;
            case SpecificityType.RangeSecuringDevice:
                archetypeInstance = new Specificity_RangeSecuringDevice();
                break;
            case SpecificityType.Preparation:
                archetypeInstance = new Specificity_Preparation();
                break;
            case SpecificityType.Pressure:
                archetypeInstance = new Specificity_Pressure();
                break;
            case SpecificityType.Wildcard:
                archetypeInstance = new Specificity_Wildcard();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(specificityType), specificityType, null);
        }
        return archetypeInstance;
    }
}
