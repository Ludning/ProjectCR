using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerFinalStats : UnitFinalStats
{
    #region Field & Property
    [SerializeField, ReadOnly]
    private int _maxMp;
    [SerializeField, ReadOnly]
    private float _criticalChance;
    [SerializeField, ReadOnly]
    private float _criticalMultiplier;
    [SerializeField, ReadOnly]
    private float _hpRegen;
    [SerializeField, ReadOnly]
    private float _mpRegen;
    [SerializeField, ReadOnly]
    private int _hp;
    [SerializeField, ReadOnly]
    private int _mp;


    public int MaxMp
    {
        get => _maxMp;
        set => _maxMp = value;
    }

    public float CriticalChance
    {
        get => _criticalChance;
        set => _criticalChance = value;
    }

    public float CriticalMultiplier
    {
        get => _criticalMultiplier;
        set => _criticalMultiplier = value;
    }

    public float HpRegen
    {
        get => _hpRegen;
        set => _hpRegen = value;
    }

    public float MpRegen
    {
        get => _mpRegen;
        set => _mpRegen = value;
    }

    public int Hp
    {
        get => _hp;
        set => _hp = value;
    }

    public int Mp
    {
        get => _mp;
        set => _mp = value;
    }
    #endregion

    public void LoadData(PlayerStats statsData, EquipmentData equipmentData)
    {
        MaxHp = statsData.MaxHp;
        MaxMp = statsData.MaxMp;
        Damage = statsData.Damage;
        Defense = statsData.Defense;
        Speed = statsData.Speed;
        CriticalChance = statsData.CriticalChance;
        CriticalMultiplier = statsData.CriticalMultiplier;
        HpRegen = statsData.HpRegen;
        MpRegen = statsData.MpRegen;
    }
    public void InitPlayerStat()
    {
        Hp = MaxHp;
        Mp = MaxMp;
    }
}
