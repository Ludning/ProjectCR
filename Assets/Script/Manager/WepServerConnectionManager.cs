using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepServerConnectionManager : SingleTon<WepServerConnectionManager>
{
    public void RequestItemSlotChange(SlotType prevSlotType, int prevSlotIndex, SlotType newSlotType, int newSlotIndex)
    {
        bool success = SendItemSlotChangeRequestToServer(prevSlotType, prevSlotIndex, newSlotType, newSlotIndex); // 서버에 요청을 보내는 메서드

        if (!success)
        {
            MessageManager.Instance.InvokeCallback(new Inventory_Message());
            MessageManager.Instance.InvokeCallback(new Equipment_Message());
        }
    }

    private bool SendItemSlotChangeRequestToServer(SlotType prevSlotType, int prevSlotIndex, SlotType newSlotType, int newSlotIndex)
    {
        return true;
    }
}
