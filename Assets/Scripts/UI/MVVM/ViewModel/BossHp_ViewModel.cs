using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp_ViewModel : ViewModelBase<BossHp_Message>
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
    protected override void OnResponseMessage(BossHp_Message message)
    {
        HpRatio = message.HpRatio;
    }
}
