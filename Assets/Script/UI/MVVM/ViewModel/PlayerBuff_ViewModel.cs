using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff_ViewModel : ViewModelBase<Record_Message>
{
    private Dictionary<string, int> _buffDictionary;

    public Dictionary<string, int> BuffDictionary
    {
        get
        {
            if (_buffDictionary == null)
                _buffDictionary = new Dictionary<string, int>();
            return _buffDictionary;
        }
    }

    public void SetBuff(Record_Message message)
    {
        if (message.RecordValue == 0)
        {
            BuffDictionary.Remove(message.RecordName);
        }
        else
        {
            BuffDictionary[message.RecordName] = message.RecordValue;
        }
        
        OnPropertyChanged(message.RecordName);
    }

    protected override void OnResponseMessage(Record_Message message)
    {
        SetBuff(message);
    }
}
