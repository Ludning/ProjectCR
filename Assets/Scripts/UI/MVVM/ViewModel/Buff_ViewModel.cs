using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ViewModel : ViewModelBase<Buff_Message>
{
    private string[] _buffData;
    private float[] _buffDuration;
    public string[] BuffData
    {
        get => _buffData;
        set
        {
            _buffData = value;
            OnPropertyChanged(nameof(BuffData));
        }
    }
    public float[] BuffDuration
    {
        get => _buffDuration;
        set
        {
            _buffDuration = value;
            OnPropertyChanged(nameof(BuffDuration));
        }
    }
    protected override void OnResponseMessage(Buff_Message message)
    {
        BuffData = message.BuffData;
        BuffDuration = message.BuffDuration;
    }
}
