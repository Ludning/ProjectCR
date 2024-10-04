using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Inventory_ViewModel : ViewModelBase<Inventory_Message>
{
    protected override void OnResponseMessage(Inventory_Message message)
    {
        ReloadInventory().Forget();
    }

    private async UniTaskVoid ReloadInventory()
    {
        PlayerManager.Instance.InventoryData.ItemDictionary = await WepServerConnectionManager.Instance.RequestInventoryData(PlayerManager.Instance.Identification);
        OnPropertyChanged("InventoryData");
    }
}
