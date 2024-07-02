using System;
using System.Collections.Generic;

public class TestWeaponData
{
    List<TestWeaponEffect> conditionEffects = new List<TestWeaponEffect>();
}

public class TestWeaponEffect
{
    
}
public class WeaponEffectParser
{
    public void ParseData(string data)
    {
        //WeaponData weaponData = new WeaponData();
        /*string[] parts = data.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
                continue;

            WeaponEffect effect = new WeaponEffect();
            string[] keyValues = part.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string keyValue in keyValues)
            {
                string[] kvPair = keyValue.Split(':');

                if (kvPair.Length == 2)
                {
                    if (kvPair[0].Trim().StartsWith("Condition"))
                    {
                        effect.Conditions.Add(kvPair[0].Trim() + ":" + kvPair[1].Trim());
                    }
                    else
                    {
                        effect.Effects.Add(kvPair[0].Trim(), kvPair[1].Trim());
                    }
                }
                else if (kvPair.Length == 1 && kvPair[0].Trim().StartsWith("Condition"))
                {
                    effect.Conditions.Add(kvPair[0].Trim());
                }
            }

            weaponData.Effects.Add(effect);
        }*/

        //return weaponData;
    }
}