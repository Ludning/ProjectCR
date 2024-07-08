using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField, ReadOnly] private string _nickName;
    [SerializeField, ReadOnly] private int _level;
    [SerializeField, ReadOnly] private float _hpRatio;
    
    [SerializeField] private Animator _animator;
    
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private PlayerInfoUIHandler playerInfoUIHandler;
    
    public WeaponHandler WeaponHandler;

    public string NickName
    {
        get => _nickName;
        set
        {
            _nickName = value;
            playerInfoUIHandler.ChangePlayerInfo();
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            playerInfoUIHandler.ChangePlayerInfo();
        }
    }

    public void OnLoadPlayer(string nickName, int level, float hpRatio)
    {
        Debug.Log("OnLoadPlayer");
        
        _nickName = nickName;
        _level = level;
        _hpRatio = hpRatio;
        
        PlayerInfo_Message msg = new PlayerInfo_Message()
        {
            ID = gameObject.GetInstanceID(),
            Level = _level,
            NickName = _nickName
        };
        MessageManager.Instance.InvokeCallback(msg);
    }

    public void OnDamage(int damageValue)
    {
        PlayerManager.Instance.OnDamage(damageValue);
    }
}
