using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PlayerStats : UnitStats
{
    #region Field & Property
    [SerializeField]
    private int _maxMp;
    [SerializeField]
    private float _criticalChance;
    [SerializeField]
    private float _criticalMultiplier;
    [SerializeField]
    private float _hpRegen;
    [SerializeField]
    private float _mpRegen;
    [SerializeField]
    private int _exp;
    [SerializeField]
    private int _expRequired;
    
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

    #endregion

    public void LoadPlayerStatsData(PlayerData data)
    {
        var levelStatDic = DataManager.Instance.GetGameData().LevelData;

        if (levelStatDic.TryGetValue(data.level, out LevelData levelData) == false)
        {
            levelData = levelStatDic[1];
        }

        MaxHp = levelData.maxHp;
        Damage = levelData.damage;
        Defense = levelData.defense;
        ExpRequired = levelData.expRequired;
        
        MaxMp = 100;
        Speed = 0;
        CriticalChance = 30;
        CriticalMultiplier = 150;
        HpRegen = 0;
        MpRegen = 0;
    }
}
