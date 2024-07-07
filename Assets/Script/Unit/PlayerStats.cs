using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PlayerStats : UnitStats
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
    private int _exp;
    [SerializeField, ReadOnly]
    private int _expRequired;

    [SerializeField, ReadOnly]
    private int _stagger;
    [SerializeField, ReadOnly]
    private int _projectileSpeed;
    
    public int MaxMp
    {
        get => _maxMp;
        protected set => _maxMp = value;
    }

    public float CriticalChance
    {
        get => _criticalChance;
        protected set => _criticalChance = value;
    }

    public float CriticalMultiplier
    {
        get => _criticalMultiplier;
        protected set => _criticalMultiplier = value;
    }

    public float HpRegen
    {
        get => _hpRegen;
        protected set => _hpRegen = value;
    }

    public float MpRegen
    {
        get => _mpRegen;
        protected set => _mpRegen = value;
    }

    public int Exp
    {
        get => _exp;
        protected set => _exp = value;
    }
    public int ExpRequired
    {
        get => _expRequired;
        protected set => _expRequired = value;
    }
    public int Stagger
    {
        get => _stagger;
        protected set => _stagger = value;
    }
    public int ProjectileSpeed
    {
        get => _projectileSpeed;
        protected set => _projectileSpeed = value;
    }
    #endregion

    public void LoadPlayerStatsData(PlayerData data, int level)
    {
        var levelStatDic = DataManager.Instance.GetGameData().LevelData;

        if (levelStatDic.TryGetValue(data.level, out LevelData levelData) == false)
        {
            levelData = levelStatDic[level];
        }

        Level = level;
        MaxHp = levelData.maxHp;
        Damage = levelData.damage;
        Defense = levelData.defense;
        ExpRequired = levelData.expRequired;
        
        MaxMp = 100;
        Speed = 100;
        CriticalChance = 30;
        CriticalMultiplier = 150;
        HpRegen = 0;
        MpRegen = 0;
        
        Stagger = 100;
        ProjectileSpeed = 100;
    }
}
