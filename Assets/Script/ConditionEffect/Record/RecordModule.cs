using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Script.Util;
using UnityEngine;

public class RecordModule
{
    private int _recordValue;
    private int _recordLimit;
    private float _duration;
    private float _durationLimit;
    private int _recordResetValue;
    private bool _noRecordDuration = false;
    private bool _isRecordResetAll = false;

    public void InitData(RecordData recordData)
    {
        _recordValue = 0;
        _recordLimit = recordData.RecordLimit;
        _durationLimit = recordData.Duration;
        _recordResetValue = recordData.RecordResetValue;
        _noRecordDuration = recordData.NoRecordDuration;
        _isRecordResetAll = recordData.IsRecordResetAll;
    }
    public void OnUpdate()
    {
        if (_noRecordDuration == true)
            return;
        if (_recordValue == 0)
            return;
    }
    public void AddValue(int value)
    {
        _recordValue += value;
    }
    public void SetValue(int value)
    {
        _recordValue = value;
    }
    public void AddDuration(int value)
    {
        _duration += value;
    }
    public void SetDuration(int value)
    {
        _duration = value;
    }
    public int GetValue()
    {
        return _recordValue;
    }
    
}