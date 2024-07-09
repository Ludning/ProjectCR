using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] private ItemElement _item;

    private SlotType _slotType;
    private int _index;
    
    public SlotType SlotType => _slotType;
    public int Index => _index;
    
    public ItemElement Item
    {
        get => _item;
        set
        {
            _item = value;
        }
    }
    public bool HasItem => Item != null;

    public void LoadData(SlotType slotType, int index)
    {
        _slotType = slotType;
        _index = index;
        
        if (_slotType == SlotType.Inventory)
        {
            InventoryData inventoryData = PlayerManager.Instance.InventoryData;
            LoadSlotData(inventoryData.GetItemByType(_index).index);
        }
        else if (_slotType == SlotType.Equipment)
        {
            EquipmentData equipmentData = PlayerManager.Instance.EquipmentDatas;
            LoadSlotData(equipmentData.GetItemByType((ItemSlotType)_index).index);
        }
    }

    private void LoadSlotData(int itemIndex)
    {
        if (itemIndex != 0)
        {
            var itemDatas = DataManager.Instance.GetGameData().ItemData;
            if (itemDatas.TryGetValue(itemIndex, out ItemData itemData))
            {
                Sprite sprite =
                    ResourceManager.Instance.LoadResourceWithCaching<Sprite>(AssetAddressType.SpriteAsset,
                        itemData.iconPathName);

                if (_item == null)
                    InstantiateItemElement();
                if(_item.transform.parent != transform)
                    _item.transform.SetParent(transform, false);
                _item.SetImage(sprite);
            }
        }
        else
        {
            if (_item == null) 
                return;
            GameObject temp = _item.gameObject;
            _item = null;
            PoolManager.Instance.ReturnToPool(temp);
        }
    }

    private void InstantiateItemElement()
    {
        GameObject itemIconPrefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.ElementUIAsset, "ItemIcon");
        GameObject itemIcon = Instantiate(itemIconPrefab, transform, false);
        _item = itemIcon.GetComponent<ItemElement>();
    }
}
