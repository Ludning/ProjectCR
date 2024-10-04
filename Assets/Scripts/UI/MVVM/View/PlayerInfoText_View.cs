using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class PlayerInfoText_View : ViewBase<PlayerInfoText_ViewModel, PlayerInfoText_Message>
{
    [SerializeField]
    private TextMeshProUGUI playerInfoTextUI;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "PlayerInfo")
        {
            playerInfoTextUI.text = _vm.PlayerInfo;
        }
    }
    protected override void OnEnableExpansion()
    {
        PlayerInfoText_Message msg = new PlayerInfoText_Message()
        {
            Context = PlayerManager.Instance.GetPlayerStatText()
        };
        MessageManager.Instance.InvokeCallback(msg);
    }
}
