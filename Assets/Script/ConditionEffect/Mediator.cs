using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.GettingStarted;
using UnityEngine;

public abstract class Mediator
{
    public abstract int DataTransfer(string name, DataModuleType type);
}
