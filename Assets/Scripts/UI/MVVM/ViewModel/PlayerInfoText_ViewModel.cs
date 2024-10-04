using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoText_ViewModel : ViewModelBase<PlayerInfoText_Message>
{
    private string _playerInfo;
    public string PlayerInfo
    {
        get => _playerInfo;
        set
        {
            _playerInfo = value;
            OnPropertyChanged(nameof(PlayerInfo));
        }
    }
    protected override void OnResponseMessage(PlayerInfoText_Message message)
    {
        PlayerInfo = message.Context;
    }
}
