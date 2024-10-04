using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterFinalStats : UnitFinalStats
{
    #region Field & Property
    [SerializeField]
    private int _hp;

    public int Hp
    {
        get => _hp;
        set => _hp = value;
    }
    #endregion
    
    public void LoadData(MonsterStats statsData)
    {
        MaxHp = statsData.MaxHp;
        Damage = statsData.Damage;
        Defense = statsData.Defense;
        Speed = statsData.Speed;
    }
    public void InitMonsterStat()
    {
        Hp = MaxHp;
    }
}
