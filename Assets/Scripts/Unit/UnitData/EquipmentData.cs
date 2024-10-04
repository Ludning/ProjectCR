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
    

    public void LoadData(Dictionary<int, Item> dataDictionary)
    {
        if (dataDictionary != null)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        mainWeapon = dataDictionary.TryGetValue(i, out Item tempMainWeapon) ? tempMainWeapon : new Item();
                        break;
                    case 1:
                        subWeapon = dataDictionary.TryGetValue(i, out Item TempSubWeapon) ? TempSubWeapon : new Item();
                        break;
                    case 2:
                        armor = dataDictionary.TryGetValue(i, out Item tempArmor) ? tempArmor : new Item();
                        break;
                    case 3:
                        accessories = dataDictionary.TryGetValue(i, out Item tempAccessories) ? tempAccessories : new Item();
                        break;
                }
            }
        }
        else
        {
            mainWeapon = new Item();
            subWeapon = new Item();
            armor = new Item();
            accessories = new Item();
        }
        
    }

    public Item EquipItem(Item itemData, EquipmentSlotType slotType)
    {
        Item prevItem = new Item();
        switch (slotType)
        {
            case EquipmentSlotType.MainWeapon:
                if (mainWeapon.index != 0) prevItem = mainWeapon;
                mainWeapon = itemData;
                break;
            case EquipmentSlotType.SubWeapon:
                if (subWeapon.index != 0) prevItem = subWeapon;
                subWeapon = itemData;
                break;
            case EquipmentSlotType.Armor:
                if (armor.index != 0) prevItem = armor;
                armor = itemData;
                break;
            case EquipmentSlotType.Accessories:
                if (accessories.index != 0) prevItem = accessories;
                accessories = itemData;
                break;
        }

        return prevItem;
    }

    public Item GetItemByType(EquipmentSlotType type)
    {
        switch (type)
        {
            case EquipmentSlotType.MainWeapon:
                return MainWeapon;
            case EquipmentSlotType.SubWeapon:
                return SubWeapon;
            case EquipmentSlotType.Armor:
                return Armor;
            case EquipmentSlotType.Accessories:
                return Accessories;
        }
        return null;
    }
}
