using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Equipment_View : ViewBase<Equipment_ViewModel, Equipment_Message>
{
    [SerializeField]
    private Dictionary<int, Transform> itemSlotDictionary;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            /*case nameof(_vm.Level):
                LevelText.text = _vm.Level.ToString();
                break;
            case nameof(_vm.NickName):
                PlayerNameText.text = _vm.NickName;
                break;*/
        }
    }
}
