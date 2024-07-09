using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

public class Inventory_View : ViewBase<Inventory_ViewModel, Inventory_Message>
{
    [SerializeField]
    private List<ItemSlot> itemSlotList;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "InventoryData")
        {
            for (int index = 0; index < itemSlotList.Count; index++)
            {
                Debug.Log($"{index}");
                itemSlotList[index].LoadData(SlotType.Inventory, index);
            }
        }
    }

    protected override void OnEnableExpansion()
    {
        MessageManager.Instance.InvokeCallback(new Inventory_Message());
    }
}
