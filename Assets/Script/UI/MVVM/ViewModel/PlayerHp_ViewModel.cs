using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp_ViewModel : ViewModelBase<PlayerHp_Message>
{
    private float _hpRatio;
    public float HpRatio
    {
        get => _hpRatio;
        set
        {
            _hpRatio = value;
            OnPropertyChanged(nameof(HpRatio));
        }
    }

    protected override void OnResponseMessage(PlayerHp_Message message)
    {
        HpRatio = message.HpRatio;
    }
}
