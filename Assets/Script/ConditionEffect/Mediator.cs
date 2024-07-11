using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mediator
{
    public abstract int GetRecordData(string name);
    public abstract void SetRecordData(string name, int value);
    public abstract void AddRecordData(string name, int value);
    public abstract void OnChangedRecordData();
}
