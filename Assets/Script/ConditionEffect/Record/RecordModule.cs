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
    private int _recordResetValue;
    private bool _noRecordDuration = false;
    private bool _isRecordResetAll = false;

    public void InitData(RecordData recordData)
    {
        string s;
        int a = int.Parse("12");
        
        _recordValue = 0;
        _recordLimit = int.Parse(recordData.RecordLimit);
        _duration = recordData.Duration;
        _recordResetValue = int.Parse(recordData.RecordResetValue);

        /*string data = StringParserHelper.ParenthesesParser(recordData);
        List<string> dataStrings = StringParserHelper.PipeParser(data);
        foreach (var dataString in dataStrings)
        {
            var keyValueData = StringParserHelper.FirstColonParser(dataString);
            switch (keyValueData.Key)
            {
                case "RecordValue":
                    _recordValue = TypeConverterHelper.GetConvertingData<T>(keyValueData.Value);
                    break;
                case "_recordLimit":
                    _recordLimit = int.Parse(keyValueData.Value);
                    break;
                case "Duration":
                    _duration = int.Parse(keyValueData.Value);
                    break;
                case "RecordResetValue":
                    if (keyValueData.Value == "all")
                        _isRecordResetAll = true;
                    else
                        _recordResetValue = TypeConverterHelper.GetConvertingData<T>(keyValueData.Value);
                    break;
            }
        }

        if (_recordValue.HasValue == false)
            throw new System.NotImplementedException();
        if (_recordLimit.HasValue == false)
            throw new System.NotImplementedException();
        if (_duration.HasValue == false)
            _duration = -1;
        if (_recordResetValue.HasValue == false && _isRecordResetAll == false)
            _isRecordResetAll = true;*/
    }
    public void OnUpdate()
    {
        if (_duration == -1)
            return;
        if (_recordValue == 0)
            return;
    }
    public void AddValue(int value)
    {
        _recordValue = value;
    }
    public int GetValue()
    {
        return _recordValue;
    }
}