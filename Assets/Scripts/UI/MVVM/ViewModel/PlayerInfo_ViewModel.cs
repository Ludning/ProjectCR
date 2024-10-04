using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo_ViewModel : ViewModelBase<PlayerInfo_Message>
{
    private int _id;
    public int ID
    {
        get => _id;
        set => _id = value;
    }
    
    private int _level;
    private string _nickName;
    public int Level
    {
        get => _level;
        set
        { 
            _level = value;
            OnPropertyChanged(nameof(Level));
        } 
    }

    public string NickName
    {
        get => _nickName;
        set
        {
            _nickName = value;
            OnPropertyChanged(nameof(NickName));
        }
    }

    protected override void OnResponseMessage(PlayerInfo_Message message)
    {
        if (message.ID != ID) return;
        Level = message.Level;
        NickName = message.NickName;
    }
}
