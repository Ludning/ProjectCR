using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class WepServerConnectionManager : SingleTon<WepServerConnectionManager>
{
    private const string url = "http://localhost:3000/"; // 서버 URL

    #region ClientRequest
    public async UniTask RequestItemSlotChange(string identification, SlotType prevSlotType, int prevSlotIndex, SlotType newSlotType, int newSlotIndex)
    {
        bool success = await SendItemSlotChangeRequestToServerAsync(identification, prevSlotType, prevSlotIndex, newSlotType, newSlotIndex); // 서버에 요청을 보내는 메서드

        if (success)
        {
            //성공 처리
        }
        else
        {
            //실패 처리
            MessageManager.Instance.InvokeCallback(new Inventory_Message());
            MessageManager.Instance.InvokeCallback(new Equipment_Message());
        }
    }
    public async UniTask<PlayerData> RequestPlayerData(string identification, string password)
    {
        PlayerData playerData = await RequestPlayerDataAsync(identification, password); // 서버에 요청을 보내는 메서드

        if (playerData != null)
        {
            //성공 처리
            return playerData;
        }
        else
        {
            //실패 처리
            return null;
        }
    }
    public async UniTask<Dictionary<int, Item>> RequestInventoryData(string identification)
    {
        Dictionary<int, Item> inventoryData = await RequestInventoryDataAsync(identification); // 서버에 요청을 보내는 메서드

        if (inventoryData != null)
        {
            //성공 처리
            return inventoryData;
        }
        else
        {
            //실패 처리
            return null;
        }
    }
    public async UniTask<Dictionary<int, Item>> RequestEquipmentData(string identification)
    {
        Dictionary<int, Item> equipmentData = await RequestEquipmentDataAsync(identification); // 서버에 요청을 보내는 메서드

        if (equipmentData != null)
        {
            //성공 처리
            return equipmentData;
        }
        else
        {
            //실패 처리
            return null;
        }
    }
    public async void RequestUpdateCharacterInfo(PlayerData playerData)
    {
        bool success = await RequestUpdateCharacterInfoAsync(playerData); // 서버에 요청을 보내는 메서드

        if (success)
        {
            //성공 처리
        }
        else
        {
            //실패 처리
        }
    }
    #endregion
    

    #region WepServer
    private async UniTask<bool> SendItemSlotChangeRequestToServerAsync(string identification, SlotType prevSlotType, int prevSlotIndex, SlotType newSlotType, int newSlotIndex)
    {
        string additiveUrl = "moveItem/";
        
        WWWForm form = new WWWForm();
        form.AddField("userId", identification);
        form.AddField("fromSlotType", prevSlotType.ToString());
        form.AddField("fromSlotId", prevSlotIndex);
        form.AddField("toSlotType", newSlotType.ToString());
        form.AddField("toSlotId", newSlotIndex);

        using (UnityWebRequest www = UnityWebRequest.Post(url+additiveUrl, form))
        {
            await www.SendWebRequest().ToUniTask();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
                // 실패한 경우 false 반환
                return false;
            }
            else
            {
                // 성공한 경우, 서버에서 받은 데이터를 처리
                string response = www.downloadHandler.text;
                // 응답 처리에 따라 성공 여부 반환 (예: 응답이 "success"일 경우 true 반환)
                return response.Contains("success");
            }
        }
    }
    
    private async UniTask<PlayerData> RequestPlayerDataAsync(string playerId, string password)
    {
        string additiveUrl = "player/";

        // WWWForm 인스턴스 생성
        WWWForm form = new WWWForm();
        form.AddField("id",playerId);
        form.AddField("password",password);
        
        // HTTP POST 방식으로 요청 보내기
        using (var request = UnityWebRequest.Post(url+additiveUrl, form))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to get character info: {request.error}");
                return null;
            }

            string jsonResponse = request.downloadHandler.text;
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonResponse);

            return playerData;
        }
    }
    private async UniTask<Dictionary<int, Item>> RequestInventoryDataAsync(string playerId)
    {
        string additiveUrl = "inventory/";

        // WWWForm 인스턴스 생성
        WWWForm form = new WWWForm();
        form.AddField("id",playerId);
        
        // HTTP POST 방식으로 요청 보내기
        using (var request = UnityWebRequest.Post(url+additiveUrl, form))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to get character info: {request.error}");
                return null;
            }

            string json = request.downloadHandler.text;
            if (json == "[]")
            {
                Debug.Log("No equipment data found.");
                return null;
            }
            Dictionary<int, Item> inventoryData = JsonConvert.DeserializeObject<Dictionary<int, Item>>(json);

            return inventoryData;
        }
    }
    private async UniTask<Dictionary<int, Item>> RequestEquipmentDataAsync(string playerId)
    {
        string additiveUrl = "equipment/";

        // WWWForm 인스턴스 생성
        WWWForm form = new WWWForm();
        form.AddField("id",playerId);
        
        // HTTP POST 방식으로 요청 보내기
        using (var request = UnityWebRequest.Post(url+additiveUrl, form))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to get character info: {request.error}");
                return null;
            }

            string json = request.downloadHandler.text;
            if (json == "[]")
            {
                Debug.Log("No equipment data found.");
                return null;
            }
            Dictionary<int, Item> equipmentData = JsonConvert.DeserializeObject<Dictionary<int, Item>>(json);

            return equipmentData;
        }
    }
    private async UniTask<bool> RequestUpdateCharacterInfoAsync(PlayerData playerData)
    {
        string additiveUrl = "updatePlayer/";
        
        WWWForm form = new WWWForm();
        
        string jsonData = JsonUtility.ToJson(playerData);

        using (UnityWebRequest www = UnityWebRequest.Post(url+additiveUrl, form))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            
            await www.SendWebRequest().ToUniTask();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
                // 실패한 경우 false 반환
                return false;
            }
            else
            {
                // 성공한 경우, 서버에서 받은 데이터를 처리
                string response = www.downloadHandler.text;
                // 응답 처리에 따라 성공 여부 반환 (예: 응답이 "success"일 경우 true 반환)
                return response.Contains("success");
            }
        }
    }
    #endregion
}
