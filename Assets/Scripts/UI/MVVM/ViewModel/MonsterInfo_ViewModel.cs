using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo_ViewModel : ViewModelBase<MonsterInfo_Message>
{
    private int _id;
    public int ID
    {
        get => _id;
        set => _id = value;
    }

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
    protected override void OnResponseMessage(MonsterInfo_Message message)
    {
        if (message.ID != ID) return;
        HpRatio = message.HpRatio;
    }
}
