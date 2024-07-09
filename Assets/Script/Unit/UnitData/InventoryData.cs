using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class InventoryData
{
    [SerializeField, ReadOnly]
    private int _inventorySize = 32;
    [SerializeField, ReadOnly]
    private Dictionary<int, Item> _itemDictionary = new Dictionary<int, Item>();
    public Dictionary<int, Item> ItemDictionary => _itemDictionary;
    public void LoadData(List<string> dataList)
    {
        for (int i = 0; i < _inventorySize; i++)
        {
            if (dataList.Count <= i)
            {
                _itemDictionary[i] = new Item();
                continue;
            }

            _itemDictionary[i] = new Item();
            _itemDictionary[i].InitItemData(dataList[0]);
        }
    }
    public Item GetItemByType(int index)
    {
        return _itemDictionary.GetValueOrDefault(index);
    }
}
