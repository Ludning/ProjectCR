using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, ReadOnly] 
    private Player Owner;
    private WeaponHandler Handler;
    
    private List<WeaponLogic> _archetypeConditionEffects;
    private Dictionary<string, WeaponRecord> _archetypeRecord;
    private List<WeaponLogic> _specificityConditionEffects;
    private Dictionary<string, WeaponRecord> _specificityRecord;

    public void InitWeapon(Player owner, WeaponHandler handler)
    {
        Owner = owner;
        Handler = handler;
    }

    public void ReceptionWeaponHandlerEvent(WeaponTrigger type)
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
    
    
    
    
    #region Temp
    [SerializeField, ReadOnly]
    private Archetype _archetype;
    [SerializeField, ReadOnly]
    private Specificity _specificity;
    public void InitWeaponData(ArchetypeType archetypeType, SpecificityType specificityType)
    {
        _archetype = Archetype.ArchetypeFactory(archetypeType);
        _specificity = Specificity.SpecificityFactory(specificityType);
    }
    #endregion
    #region Parser
    /*public List<WeaponEffect> ParseData(string data)
    {
        List<WeaponEffect> weaponEffects = new List<WeaponEffect>();
        string[] entries = data.Split('\n'); // 데이터가 줄바꿈으로 구분된 경우

        foreach (var entry in entries)
        {
            if (string.IsNullOrWhiteSpace(entry))
                continue;

            WeaponEffect effect = new WeaponEffect
            {
                Conditions = new List<string>(),
                Effects = new Dictionary<string, string>()
            };

            // 조건과 효과를 분리
            var parts = entry.Split(new[] { '}' }, StringSplitOptions.RemoveEmptyEntries);

            // 조건 파싱
            string conditionPart = parts[0].Trim('{');
            foreach (var condition in conditionPart.Split(','))
            {
                effect.Conditions.Add(condition.Trim());
            }

            // 효과 파싱
            if (parts.Length > 1)
            {
                string effectPart = parts[1].Trim('{', '}');
                foreach (var eff in effectPart.Split(','))
                {
                    var keyValue = eff.Split(':');
                    if (keyValue.Length == 2)
                    {
                        effect.Effects.Add(keyValue[0].Trim(), keyValue[1].Trim());
                    }
                }
            }

            weaponEffects.Add(effect);
        }
        return weaponEffects;
    }*/
    #endregion
}
