using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Archetype
{
    public static Archetype ArchetypeFactory(ArchetypeType archetype)
    {
        Archetype archetypeInstance;
        switch (archetype)
        {
            case ArchetypeType.Adaptive:
                archetypeInstance = new Archetype_Adaptive();
                break;
            case ArchetypeType.Aggressive:
                archetypeInstance = new Archetype_Aggressive();
                break;
            case ArchetypeType.LightWeight:
                archetypeInstance = new Archetype_LightWeight();
                break;
            case ArchetypeType.Precision:
                archetypeInstance = new Archetype_Precision();
                break;
            case ArchetypeType.Heavy:
                archetypeInstance = new Archetype_Heavy();
                break;
            case ArchetypeType.Shadow:
                archetypeInstance = new Archetype_Shadow();
                break;
            case ArchetypeType.Predation:
                archetypeInstance = new Archetype_Predation();
                break;
            case ArchetypeType.Hunter:
                archetypeInstance = new Archetype_Hunter();
                break;
            case ArchetypeType.Lasso:
                archetypeInstance = new Archetype_Lasso();
                break;
            case ArchetypeType.Cleavage:
                archetypeInstance = new Archetype_Cleavage();
                break;
            case ArchetypeType.Conversion:
                archetypeInstance = new Archetype_Conversion();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(archetype), archetype, null);
        }
        return archetypeInstance;
    }
    
}
