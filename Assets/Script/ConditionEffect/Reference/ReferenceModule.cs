using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReferenceModule
{
    public abstract void InitData(string referenceData);

    public abstract void AddValueToRecord<T>(T value);
}
