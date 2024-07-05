using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct KeyValueData<TKey, TValue>
{
    public TKey Key;
    public TValue Value;
    public KeyValueData(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}
