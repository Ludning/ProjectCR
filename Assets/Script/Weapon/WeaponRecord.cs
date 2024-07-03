using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WeaponRecord
{
    private RecordType _recordType;
    private int _recordLimit;
    private float _duration;
    private int _recordResetValue;


    public WeaponRecord(string recordData)
    {
        SetRecordData(recordData);
    }

    private string SetRecordData(string recordData)
    {
        return string.Empty;
    }
}
