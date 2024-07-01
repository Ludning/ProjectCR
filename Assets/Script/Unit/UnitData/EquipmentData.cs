using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentData
{
    [SerializeField]
    private int mainWeapon;
    [SerializeField]
    private int subWeapon;
    [SerializeField]
    private int armor;
    [SerializeField]
    private int accessories;

    public void LoadData(string dataString)
    {
        dataString = dataString.Trim('{', '}');
        string[] itemIdArr = dataString.Split(',');

        mainWeapon = SafeDataParser(itemIdArr, 0);
        subWeapon = SafeDataParser(itemIdArr, 1);
        armor = SafeDataParser(itemIdArr, 2);
        accessories = SafeDataParser(itemIdArr, 3);
    }

    private int SafeDataParser(string[] itemIdArr, int index)
    {
        if (index < itemIdArr.Length && int.TryParse(itemIdArr[index], out int number))
        {
            return number;
        }
        else
        {
            Debug.LogError($"인덱스 {index}가 범위를 벗어나거나 유효하지 않은 값입니다.");
            return 0;
        }
    }
}
