using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo_View : ViewBase<PlayerInfo_ViewModel, PlayerInfo_Message>
{
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI PlayerNameText;
    
    public void SetId(int id)
    {
        _vm.ID = id;
    }
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Level):
                LevelText.text = _vm.Level.ToString();
                break;
            case nameof(_vm.NickName):
                PlayerNameText.text = _vm.NickName;
                break;
        }
    }
}
