using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class RecordModule
{
    public abstract void InitRecordData(string recordData);

    public abstract void AddValueToRecord<T>(T value);

}
