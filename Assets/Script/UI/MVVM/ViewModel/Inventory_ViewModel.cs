using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ViewModel : ViewModelBase<Inventory_Message>
{
    protected override void OnResponseMessage(Inventory_Message message)
    {
        OnPropertyChanged("InventoryData");
    }
}
