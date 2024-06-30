using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Nickname;
    private PlayerStats _stats;
    private PlayerTotalStats _totalStats;
    private InventoryDatas _InventoryDatas;
    
    private void Awake()
    {
        PlayerInit();
    }

    private void PlayerInit()
    {
        //네트워크에서 데이터를 가져와 초기화
        _stats = new PlayerStats();
        _stats.Init();
        _InventoryDatas.Init();
        OnInstallEquipment();
    }

    //장착중인 장비 스텟에 적용
    private void OnInstallEquipment()
    {
        _totalStats = new PlayerTotalStats();
        _totalStats.Init();
    }
}
