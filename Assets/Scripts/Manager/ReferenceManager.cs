using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : SingleTon<ReferenceManager>
{
    private Dictionary<string, int> _referenceDictionary = new Dictionary<string, int>();

    public object GetReference(string name)
    {
        if (_referenceDictionary.TryGetValue(name, out int reference))
        {
            return reference;
        }

        return null;
    }
}
