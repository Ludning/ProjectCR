using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [SerializeField, ReadOnly]
    private int inventorySize = 30;
    [SerializeField, ReadOnly]
    private Dictionary<int, Item> itemDictionary = new Dictionary<int, Item>();
    public void LoadData(List<string> dataList)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (dataList.Count <= i)
            {
                itemDictionary[i] = new Item();
                continue;
            }

            itemDictionary[i] = new Item();
            itemDictionary[i].InitItemData(dataList[0]);
        }
    }
}
