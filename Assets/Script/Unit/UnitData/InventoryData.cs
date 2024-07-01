using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [SerializeField]
    private int inventorySize = 30;
    [SerializeField]
    private Dictionary<int, int> itemDictionary = new Dictionary<int, int>();
    public void LoadData(string dataString)
    {
        dataString = dataString.Trim('{', '}');
        string[] itemIdArr = dataString.Split(',');

        for (int i = 0; i < inventorySize; i++)
        {
            if (itemIdArr.Length <= i)
            {
                itemDictionary[i] = 0;
                continue;
            }
            Debug.Log(itemIdArr.Length);
            Debug.Log(i);
            if (int.TryParse(itemIdArr[i], out int number) == false)
            {
                Debug.LogError("인벤토리 데이터 파싱 실패");
            }
            itemDictionary[i] = number;
        }
    }
}
