using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Equipment_ViewModel : ViewModelBase<Equipment_Message>
{
    protected override void OnResponseMessage(Equipment_Message message)
    {
        ReloadEquipment().Forget();
    }
    
    private async UniTaskVoid ReloadEquipment()
    {
        var dataDic = await WepServerConnectionManager.Instance.RequestEquipmentData(PlayerManager.Instance.Identification);
        PlayerManager.Instance.EquipmentDatas.LoadData(dataDic);
        OnPropertyChanged("EquipmentData");
    }
}
