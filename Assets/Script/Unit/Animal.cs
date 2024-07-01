using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Animal : MonoBehaviour, IDamageable
{
    [SerializeField]
    private string _monsterName;
    [SerializeField]
    private MonsterStats _stats;
    [SerializeField]
    private MonsterFinalStats _currentStats;
    
    public DetectTarget DetectTarget;

    [SerializeField] private Animator _animator;

    #region Monobehavior Function
    private void Awake()
    {
        OnLoadMonsterData();
        OnInitMonsterStats();
        _currentStats.InitMonsterStat();
    }
    #endregion
    
    
    public void OnDamage(int damageValue)
    {
        _currentStats.Hp -= damageValue;
        if (_currentStats.Hp <= 0)
        {
            _currentStats.Hp = 0;
            OnDie();
        }
        MonsterInfo_Message msg = new MonsterInfo_Message()
        {
            ID = gameObject.GetInstanceID(),
            HpRatio = (_currentStats.MaxHp != 0) ? _currentStats.Hp / (float)_currentStats.MaxHp : 0f
        };
        MessageManager.Instance.InvokeCallback(msg);
    }

    public void OnDie()
    {
        Debug.Log($"으앙{gameObject.name}주금");
    }

    #region Data
    private void OnLoadMonsterData()
    {
        _stats = new MonsterStats();
        MonsterData data = DataManager.Instance.GetGameData().MonsterData[gameObject.name];
        
        _monsterName = data.monsterName;
        _stats.LoadMonsterStatsData(data);
    }

    private void OnInitMonsterStats()
    {
        _currentStats = new MonsterFinalStats();
        _currentStats.LoadData(_stats);
    }
    #endregion
    
}
