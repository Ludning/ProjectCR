using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestWepServerConnection : MonoBehaviour
{
    private PlayerData playerData;

    async UniTaskVoid Start()
    {
        playerData = await WepServerConnectionManager.Instance.RequestPlayerData("11", "11");
        if (playerData == null)
        {
            Debug.Log($"{playerData} is null");
            return;
        }
        Debug.Log(playerData.identification);
        Debug.Log(playerData.password);
        Debug.Log(playerData.nickname);
        Debug.Log(playerData.character);
        Debug.Log(playerData.level);
        Debug.Log(playerData.exp);
    }
}