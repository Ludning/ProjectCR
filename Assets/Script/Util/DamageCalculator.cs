using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    //totalDMG = weaponDMG * {1 + skillDMG} * {1 + criticalDMG} * {1 + weakDMG} 
    //총 피해량 = 무기 공격력 * {스킬 툴팁 피해} * {극대화 피해} * {취약 피해}
    public static float GetDamage(float weaponDMG, float skillDMG, float criticalDMG, float weakDMG)
    {
        return weaponDMG * (1 + skillDMG) * (1 + criticalDMG) * (1 + weakDMG);
    }
}
