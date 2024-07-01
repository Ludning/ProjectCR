using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public string _identification;
    public string _nickname;
    
    [SerializeField]
    private PlayerStats _stats;
    [SerializeField]
    private PlayerFinalStats _finalStats;
    [SerializeField]
    private InventoryData _inventoryData;
    [SerializeField]
    private EquipmentData _equipmentDatas;
    [SerializeField]
    private SkillTreeData _skillTreeData;
    
    private void Awake()
    {
        OnLoadPlayerData();
        OnInstallEquipment();
        
        _finalStats.InitPlayerStat();
    }

    private void OnLoadPlayerData()
    {
        _stats = new PlayerStats();
        _inventoryData = new InventoryData();
        _equipmentDatas = new EquipmentData();
        _skillTreeData = new SkillTreeData();

        //네트워크에서 데이터를 가져와 초기화
        //웹서버에 _identification 로 데이터를 받아옴
        //TODO
        TestPlayerData();
    }

    //장착중인 장비 스텟에 적용
    private void OnInstallEquipment()
    {
        _finalStats = new PlayerFinalStats();
        _finalStats.LoadData(_stats, _equipmentDatas);
    }

    private void TestPlayerData()
    {
        GameData gameData = DataManager.Instance.GetGameData();
        PlayerData data = gameData.PlayerData[3];
        
        _nickname = data.nickname;
        _stats.LoadPlayerStatsData(data);
        _inventoryData.LoadData(data.inventory_data);
        _equipmentDatas.LoadData(data.equipment_data);
        _skillTreeData.LoadData(data.ownedSkill_data, data.equipmentSkill_data);
    }
}
