using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventoryLoader : MonoBehaviour
{
    public void OnClickEvent()
    {
        MessageManager.Instance.InvokeCallback(new Inventory_Message());
        MessageManager.Instance.InvokeCallback(new Equipment_Message());
    }
}
