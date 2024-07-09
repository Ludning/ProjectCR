using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentData
{
    #region Field & Property
    [SerializeField] private Item mainWeapon;
    [SerializeField] private Item subWeapon;
    [SerializeField] private Item armor;
    [SerializeField] private Item accessories;
    public Item MainWeapon
    {
        get
        {
            if (mainWeapon == null)
                mainWeapon = new Item();
            return mainWeapon;
        }
    }
    public Item SubWeapon
    {
        get
        {
            if (subWeapon == null)
                subWeapon = new Item();
            return subWeapon;
        }
    }
    public Item Armor
    {
        get
        {
            if (armor == null)
                armor = new Item();
            return armor;
        }
    }
    public Item Accessories
    {
        get
        {
            if (accessories == null)
                accessories = new Item();
            return accessories;
        }
    }
    #endregion
    

    public void LoadData(List<string> dataList)
    {
        string tempItemString = "(0|0|0|0)";
        if(dataList.Count > 0)
            MainWeapon.InitItemData(dataList[0]);
        else
            MainWeapon.InitItemData(tempItemString);
        
        if(dataList.Count > 1)
            SubWeapon.InitItemData(dataList[1]);
        else
            SubWeapon.InitItemData(tempItemString);
        
        if(dataList.Count > 2)
            Armor.InitItemData(dataList[2]);
        else
            Armor.InitItemData(tempItemString);
        
        if(dataList.Count > 3)
            Accessories.InitItemData(dataList[3]);
        else
            Accessories.InitItemData(tempItemString);
    }

    public Item EquipItem(Item itemData, ItemSlotType slotType)
    {
        Item prevItem = new Item();
        switch (slotType)
        {
            case ItemSlotType.MainWeapon:
                if (mainWeapon.index != 0) prevItem = mainWeapon;
                mainWeapon = itemData;
                break;
            case ItemSlotType.SubWeapon:
                if (subWeapon.index != 0) prevItem = subWeapon;
                subWeapon = itemData;
                break;
            case ItemSlotType.Armor:
                if (armor.index != 0) prevItem = armor;
                armor = itemData;
                break;
            case ItemSlotType.Accessories:
                if (accessories.index != 0) prevItem = accessories;
                accessories = itemData;
                break;
        }

        return prevItem;
    }

    public Item GetItemByType(ItemSlotType type)
    {
        switch (type)
        {
            case ItemSlotType.MainWeapon:
                return MainWeapon;
            case ItemSlotType.SubWeapon:
                return SubWeapon;
            case ItemSlotType.Armor:
                return Armor;
            case ItemSlotType.Accessories:
                return Accessories;
        }
        return null;
    }
}
