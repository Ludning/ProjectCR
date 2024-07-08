using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    
    private void Awake()
    {
        LoadData();
    }
    
    public void LoadData()
    {
        OnLoadPlayerData();
        if(_currentStats == null)
            _currentStats = new PlayerFinalStats();
        _currentStats.LoadData(_stats);
        _currentStats.InitPlayerStat();

        
        GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.CharacterAsset, "Character1");
        GameObject playerObject = Instantiate(prefab);
        _player = playerObject.GetComponent<Player>();
        
        _player.OnLoadPlayer(_nickname, _stats.Level, _currentStats.Hp/(float)_currentStats.MaxHp);
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
    private void OnLoadPlayerData()
    {
        _stats = new PlayerStats();
        _inventoryData = new InventoryData();
        _equipmentData = new EquipmentData();
        _skillTreeData = new SkillTreeData();

        //네트워크에서 데이터를 가져와 초기화
        //웹서버에 _identification 로 데이터를 받아옴
        //TODO
        TestPlayerData();

        InitEquipmentData(_equipmentData);
    }
    private void TestPlayerData()
    {
        //TODO
        GameData gameData = DataManager.Instance.GetGameData();
        PlayerData data = gameData.PlayerData["11"];//_identification];
        
        _nickname = data.nickname;
        _stats.LoadPlayerStatsData(data, data.level);
        _inventoryData.LoadData(data.inventory_data);
        _equipmentData.LoadData(data.equipment_data);
        _skillTreeData.LoadData(data.ownedSkill_data, data.equipmentSkill_data);
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
    public void SetEquipItem(Item item, ItemSlotType slotType)
    {
        _equipmentData.EquipItem(item, slotType);
        InstallEquipment(item, slotType);
    }
    //장비 데이터 초기화
    private void InitEquipmentData(EquipmentData data)
    {
        InstallEquipment(data.MainWeapon, ItemSlotType.MainWeapon);
        InstallEquipment(data.SubWeapon, ItemSlotType.SubWeapon);
        InstallEquipment(data.Armor, ItemSlotType.Armor);
        InstallEquipment(data.Accessories, ItemSlotType.Accessories);
    }
    //장비 활성화
    private void InstallEquipment(Item item, ItemSlotType slotType)
    {
        switch (slotType)
        {
            case ItemSlotType.MainWeapon:
                if (_primaryWeapon == null)
                    _primaryWeapon = new Equipment();
                _primaryWeapon.UnInstall();
                _primaryWeapon.Install(item);
                break;
            case ItemSlotType.SubWeapon:
                if (_subWeapon == null)
                    _subWeapon = new Equipment();
                _subWeapon.UnInstall();
                _subWeapon.Install(item);
                break;
            case ItemSlotType.Armor:
                if (_armor == null)
                    _armor = new Equipment();
                _armor.UnInstall();
                _armor.Install(item);
                break;
            case ItemSlotType.Accessories:
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
    public int GetDataByReferenceName(string referenceName)
    {
        return 1;
    }
    #endregion
}
