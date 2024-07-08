using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_ViewModel : ViewModelBase<Equipment_Message>
{
    protected override void OnResponseMessage(Equipment_Message message)
    {
        /*if (message.ID != ID) return;
        HpRatio = message.HpRatio;*/
    }
}
