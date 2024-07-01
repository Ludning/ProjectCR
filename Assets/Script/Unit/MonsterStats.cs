using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterStats : UnitStats
{
    public void LoadMonsterStatsData(MonsterData data)
    {
        Level = data.level;
        MaxHp = data.maxHp;
        Damage = data.damage;
        Defense = data.defense;
        Speed = 0;
    }
}
