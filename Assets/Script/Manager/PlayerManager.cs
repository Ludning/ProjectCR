using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PlayerManager : SingleTonMono<PlayerManager>
{
    #region Field & Property
    [SerializeField, ReadOnly] private string _identification;
    [SerializeField, ReadOnly] private string _nickname;

    [SerializeField, ReadOnly] private Player _player;
    
    [SerializeField, ReadOnly] private PlayerStats _stats;
    [SerializeField, ReadOnly] private PlayerFinalStats _currentStats;
    [SerializeField, ReadOnly] private InventoryData _inventoryData;
    [SerializeField, ReadOnly] private EquipmentData _equipmentData;
    [SerializeField, ReadOnly] private SkillTreeData _skillTreeData;
    public Player Player => _player;

    public string Identification
    {
        get => _identification;
        set
        {
            _identification = value;
        }
    }
    public string NickName
    {
        get => _nickname;
        set
        {
            _nickname = value;
            if(_player != null)
                _player.NickName = value;
        }
    }
    public int Level
    {
        get => _stats.Level;
        set
        {
            _stats.Level = value;
            if(_player != null)
                _player.Level = value;
        }
    }
    
    public InventoryData InventoryData => _inventoryData;
    public EquipmentData EquipmentDatas => _equipmentData;
    #endregion


    public TMP_InputField InputField_Id;
    public TMP_InputField InputField_Password;
    
    public void OnClickLoginButton()
    {
        Login(InputField_Id.text, InputField_Password.text).Forget();
    }
    public async UniTaskVoid Login(string identification, string password)
    {
        PlayerData playerData = await WepServerConnectionManager.Instance.RequestPlayerData(identification, password);
        if (playerData == null)
        {
            return;
        }
        _identification = identification;
        
        Dictionary<int, Item> equipmentDictionary = await WepServerConnectionManager.Instance.RequestEquipmentData(identification);
        
        LoadData(playerData, equipmentDictionary);
    }
    
    private void LoadData(PlayerData playerData, Dictionary<int, Item> equipmentDictionary)
    {
        OnLoadPlayerData(playerData, equipmentDictionary);
        
        if(_currentStats == null)
            _currentStats = new PlayerFinalStats();
        _currentStats.LoadData(_stats);
        _currentStats.InitPlayerStat();

        Debug.Log(playerData.character);
        GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.CharacterAsset, playerData.character);
        GameObject playerObject = Instantiate(prefab);
        _player = playerObject.GetComponent<Player>();
        
        _player.OnLoadPlayer(_nickname, _stats.Level, _currentStats.Hp/(float)_currentStats.MaxHp);
        
        
        GameObject vCamPrefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.SpawnableAsset, "VirtualCamera");
        GameObject vCam = Instantiate(vCamPrefab);
        CinemachineVirtualCamera virtualCamera = vCam.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = playerObject.transform;
        virtualCamera.LookAt = playerObject.transform;
        
        InitEquipmentData(_equipmentData);
    }

    public void OnDamage(int damageValue)
    {
        _currentStats.Hp -= damageValue;
        if (_currentStats.Hp <= 0)
        {
            _currentStats.Hp = 0;
            OnDie();
        }
        PlayerHp_Message msg = new PlayerHp_Message()
        {
            HpRatio = (_currentStats.MaxHp != 0) ? _currentStats.Hp / (float)_currentStats.MaxHp : 0f
        };
        MessageManager.Instance.InvokeCallback(msg);
    }

    public void OnDie()
    {
        Debug.Log($"으앙{gameObject.name}주금");
    }

    public int GetLevel()
    {
        return _stats.Level;
    }
    public string GetNickName()
    {
        return _nickname;
    }

    public void AddStats(IncreasedStatType type, int value)
    {
        switch (type)
        {
            case IncreasedStatType.Damage:
                _currentStats.Damage += value;
                break;
            case IncreasedStatType.Speed:
                _currentStats.Speed += value;
                break;
            case IncreasedStatType.CriticalChance:
                _currentStats.CriticalChance += value;
                break;
            case IncreasedStatType.CriticalMultiplier:
                _currentStats.CriticalMultiplier += value;
                break;
            case IncreasedStatType.Stagger:
                _currentStats.Stagger += value;
                break;
            case IncreasedStatType.ProjectileSpeed:
                _currentStats.ProjectileSpeed += value;
                break;
        }
    }


    #region DataLoad
    private void OnLoadPlayerData(PlayerData playerData, Dictionary<int, Item> equipmentDictionary)
    {
        _stats = new PlayerStats();
        _inventoryData = new InventoryData();
        _equipmentData = new EquipmentData();
        //_skillTreeData = new SkillTreeData();

        _nickname = playerData.nickname;
        _stats.LoadPlayerStatsData(playerData.level);
        _equipmentData.LoadData(equipmentDictionary);
        //_skillTreeData.LoadData(playerData.ownedSkill_data, playerData.equipmentSkill_data);

    }
    #endregion
    
    
    
    
    #region Equipment
    [SerializeField, ReadOnly] private Equipment _primaryWeapon;
    [SerializeField, ReadOnly] private Equipment _subWeapon;
    [SerializeField, ReadOnly] private Equipment _armor;
    [SerializeField, ReadOnly] private Equipment _accessories;
    public WeaponIndexType CurrentWeaponIndex;
    public Equipment HoldWeapon
    {
        get
        {
            switch (CurrentWeaponIndex)
            {
                case WeaponIndexType.Primary:
                    return _primaryWeapon;
                case WeaponIndexType.Secondary:
                    return _subWeapon;
                default:
                    return null;
            }
        }
    }
    public Equipment FreeWeapon
    {
        get
        {
            switch (CurrentWeaponIndex)
            {
                case WeaponIndexType.Primary:
                    return _subWeapon;
                case WeaponIndexType.Secondary:
                    return _primaryWeapon;
                default:
                    return null;
            }
        }
    }
    //무기 스왑
    public void SwapWeapon(WeaponIndexType weaponIndexType)
    {
        if (FreeWeapon == null)
            return;
        if (CurrentWeaponIndex == weaponIndexType)
            return;
        CurrentWeaponIndex = weaponIndexType;
        SendWeaponHoldFreeTrigger();
    }
    
    //장비 아이템 장착
    public void SetEquipItem(Item item, EquipmentSlotType slotType)
    {
        _equipmentData.EquipItem(item, slotType);
        InstallEquipment(item, slotType);
    }

    public async UniTask ReloadEquipment()
    {
        var data = await WepServerConnectionManager.Instance.RequestEquipmentData(_identification);
        _equipmentData.LoadData(data);
        InitEquipmentData(_equipmentData);
    }

    //장비 데이터 초기화
    private void InitEquipmentData(EquipmentData data)
    {
        InstallEquipment(data.MainWeapon, EquipmentSlotType.MainWeapon);
        InstallEquipment(data.SubWeapon, EquipmentSlotType.SubWeapon);
        InstallEquipment(data.Armor, EquipmentSlotType.Armor);
        InstallEquipment(data.Accessories, EquipmentSlotType.Accessories);
    }
    //장비 활성화
    private void InstallEquipment(Item item, EquipmentSlotType slotType)
    {
        switch (slotType)
        {
            case EquipmentSlotType.MainWeapon:
                if (_primaryWeapon == null)
                    _primaryWeapon = new Equipment();
                _primaryWeapon.UnInstall();
                _primaryWeapon.Install(item);
                break;
            case EquipmentSlotType.SubWeapon:
                if (_subWeapon == null)
                    _subWeapon = new Equipment();
                _subWeapon.UnInstall();
                _subWeapon.Install(item);
                break;
            case EquipmentSlotType.Armor:
                if (_armor == null)
                    _armor = new Equipment();
                _armor.UnInstall();
                _armor.Install(item);
                break;
            case EquipmentSlotType.Accessories:
                if (_accessories == null)
                    _accessories = new Equipment();
                _accessories.UnInstall();
                _accessories.Install(item);
                break;
        }
        SendWeaponHoldFreeTrigger();
    }
    
    //현재 무기 활성화, 나머지 무기 비활성화
    private void SendWeaponHoldFreeTrigger()
    {
        if (HoldWeapon != null)
        {
            HoldWeapon.ReceptionHandlerEvent(Trigger.HoldWeapon);
        }
        if (FreeWeapon != null)
        {
            FreeWeapon.ReceptionHandlerEvent(Trigger.FreeWeapon);
        }
    }
    #endregion



    #region ReferenceInterface
    public int GetDataByReferenceName(ReferenceType reference)
    {
        switch (reference)
        {
            case ReferenceType.PlayerHP:
                return _currentStats.Hp;
            case ReferenceType.PlayerMP:
                return _currentStats.Mp;
            case ReferenceType.PlayerDamage:
                return _currentStats.Damage;
        }

        return int.MaxValue;
    }
    #endregion
}
