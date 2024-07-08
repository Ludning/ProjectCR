using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Cysharp.Threading.Tasks;
using Script.Util;
using UnityEngine;

public class RecordModule
{
    private Mediator _mediator;
    
    private int _recordValue;
    private int _recordLimit;
    private float _duration;
    private int _recordResetValue;
    private bool _noRecordDuration = false;
    private bool _isRecordResetAll = false;

    private bool isTaskRunning = false;


    public int RecordValue
    {
        get => _recordValue;
        set
        {
            if (_recordValue != value)
            {
                _recordValue = value;
                OnValueChange();
            }
        }
    }
    
    public void InitData(RecordData recordData, Mediator mediator)
    {
        _mediator = mediator;
        
        _recordLimit = recordData.RecordLimit;
        _duration = recordData.Duration;
        _recordResetValue = recordData.RecordResetValue;
        _noRecordDuration = recordData.NoRecordDuration;
        _isRecordResetAll = recordData.IsRecordResetAll;
    }

    private async void UpdateDuration()
    {
        if (isTaskRunning)
            return;

        isTaskRunning = true;

        while (RecordValue  > 0)
        {
            await UniTask.Delay((int)(_duration * 1000));

            if (_isRecordResetAll == true)
                RecordValue  = 0;
            else
                RecordValue  -= _recordResetValue;


            if (RecordValue <= 0)
            {
                RecordValue = 0;
                break;
            }
        }

        isTaskRunning = false;
    }


    private void OnValueChange()
    {
        _mediator.OnChangedRecordData();
        if (_recordValue <= 0)
        {
            _recordValue = 0;
            return;
        }
        if (_noRecordDuration == true)
            return;
        
        if(isTaskRunning == false)
            UpdateDuration();
    }
}