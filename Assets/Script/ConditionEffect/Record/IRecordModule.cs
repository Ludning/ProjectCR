using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordModule
{
    public void InitData(RecordData recordData);
    public void OnUpdate();
}
