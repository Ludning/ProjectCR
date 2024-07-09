using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_View : ViewBase<Equipment_ViewModel, Equipment_Message>
{
    [SerializeField]
    private ItemSlot _mainWeaponSlot;
    [SerializeField]
    private ItemSlot _subWeaponSlot;
    [SerializeField]
    private ItemSlot _armorSlot;
    [SerializeField]
    private ItemSlot _accessoriesSlot;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "EquipmentData")
        {
            _mainWeaponSlot.LoadData(SlotType.Equipment, 1);
            _subWeaponSlot.LoadData(SlotType.Equipment, 2);
            _armorSlot.LoadData(SlotType.Equipment, 3);
            _accessoriesSlot.LoadData(SlotType.Equipment, 4);
        }
    }
    protected override void OnEnableExpansion()
    {
        MessageManager.Instance.InvokeCallback(new Equipment_Message());
    }
}
