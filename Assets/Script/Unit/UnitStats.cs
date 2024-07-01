using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitStats
{
    #region Field & Property
    [SerializeField]
    private int _level;
    [SerializeField]
    private int _maxHp;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _defense;
    [SerializeField]
    private int _speed;
    

    public int Level
    {
        get => _level;
        set => _level = value;
    }
    
    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public int Defense
    {
        get => _defense;
        set => _defense = value;
    }

    public int Speed
    {
        get => _speed;
        set => _speed = value;
    }

    #endregion


    public void LoadUnitData()
    {

    }
}
