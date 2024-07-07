using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.GettingStarted;
using UnityEngine;

public abstract class Mediator
{
    public Player Owner { get; set; }
    public abstract int GetData(string name, DataModuleType type);
    public abstract void SetRecordData(string name, int value, RecordDataType type);
    public abstract void AddRecordData(string name, int value, RecordDataType type);
}
