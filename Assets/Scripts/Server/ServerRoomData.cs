using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ServerRoomData
{
    public string ServerID;
    public string ServerName;
    public string ServerOwner;
    public int ServerCurrentConnection;
    public int ServerMaxConnectionLimit;
    public string ServerPassword;
}
