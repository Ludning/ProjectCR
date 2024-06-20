using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMp_ViewModel : ViewModelBase<PlayerMp_Message>
{
    private float _mpRatio;
    public float MpRatio
    {
        get => _mpRatio;
        set
        {
            _mpRatio = value;
            OnPropertyChanged(nameof(MpRatio));
        }
    }
    protected override void OnResponseMessage(PlayerMp_Message message)
    {
        MpRatio = message.MpRatio;
    }
}
