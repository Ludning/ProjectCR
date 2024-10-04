using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ServerRoomPanel : MonoBehaviour, IPointerClickHandler
{
    public event Action<ServerRoomPanel> OnClickEvent;
    public ServerRoomData RoomData;

    public Image BackgroundImage;
    
    [SerializeField] private TextMeshProUGUI TMP_ServerName;
    [SerializeField] private TextMeshProUGUI TMP_ServerOwner;
    [SerializeField] private TextMeshProUGUI TMP_ServerLimit;
    
    public void SetServerInfo(ServerRoomData roomData)
    {
        RoomData = roomData;
        TMP_ServerName.text = $"{roomData.ServerName}";
        TMP_ServerOwner.text = $"{roomData.ServerOwner}";
        TMP_ServerLimit.text = $"{roomData.ServerCurrentConnection}/{roomData.ServerMaxConnectionLimit}";
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
}
