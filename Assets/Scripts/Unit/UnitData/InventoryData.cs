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
    public Dictionary<int, Item> ItemDictionary
    {
        get => _itemDictionary;
        set
        {
            if (value != null)
            {
                for (int i = 0; i < _inventorySize; i++)
                {
                    if (value.ContainsKey(i))
                    {
                        _itemDictionary[i] = value[i];
                    }
                    else
                    {
                        _itemDictionary[i] = new Item();
                    }
                }
            }
            else
            {
                for (int i = 0; i < _inventorySize; i++)
                {
                    _itemDictionary[i] = new Item();
                }
            }
        }
    }

    public Item GetItemByType(int index)
    {
        return _itemDictionary.GetValueOrDefault(index);
    }
}
