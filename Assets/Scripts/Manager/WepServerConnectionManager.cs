using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Mirror.Discovery;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class WepServerConnectionManager : SingleTon<WepServerConnectionManager>
{
    private const string url = "http://localhost:3000/"; // 서버 URL

    
    #region ClientRequest
    //서버 등록
    public async UniTask<(bool, long)> RegisterServerData(ServerRoomData roomData, string ip, string port)
    {
        (bool value, long serverId) result = await PostServerData(roomData, ip, port); // 서버에 요청을 보내는 메서드

        if (result.value == true)
        {
            //성공 처리
            Debug.Log("서버등록 완료");
        }
        else
        {
            //실패 처리
            Debug.LogWarning("서버등록 실패");
        }
        return result;
    }
    //서버리스트 요청
    public async UniTask<List<ServerRoomData>> RequestServerRoomList()
    {
        List<ServerRoomData> serverRoomDataList = await GetServerRoomList(); // 서버에 요청을 보내는 메서드

        if (serverRoomDataList != null)
        {
            //성공 처리
            return serverRoomDataList;
        }
        else
        {
            //실패 처리
            return null;
        }
    }
    //서버 정보 요청
    public async UniTask<ServerData> RequestServerData(string serverId)
    {
        ServerData serverData = await GetServerData(serverId); // 서버에 요청을 보내는 메서드
        return serverData;
    }
    //플레이어 정보 요청
    public async UniTask<PlayerData> RequestPlayerData(string identification, string password)
    {
        PlayerData playerData = await GetPlayerData(identification, password); // 서버에 요청을 보내는 메서드

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
    //인벤토리 정보 요청
    public async UniTask<Dictionary<int, Item>> RequestInventoryData(string identification)
    {
        Dictionary<int, Item> inventoryData = await GetInventoryData(identification); // 서버에 요청을 보내는 메서드

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
    //장비 정보 요청
    public async UniTask<Dictionary<int, Item>> RequestEquipmentData(string identification)
    {
        Dictionary<int, Item> equipmentData = await GetEquipmentData(identification); // 서버에 요청을 보내는 메서드

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
    //캐릭터 정보 요청
    public async void RequestUpdateCharacterInfo(PlayerData playerData)
    {
        bool success = await PostUpdateCharacterInfo(playerData); // 서버에 요청을 보내는 메서드

        if (success)
        {
            //성공 처리
        }
        else
        {
            //실패 처리
        }
    }
    //인벤토리 슬룻 변경 요청
    public async UniTask RequestItemSlotChange(string identification, SlotType prevSlotType, int prevSlotIndex, SlotType newSlotType, int newSlotIndex)
    {
        bool success = await PostItemSlotChangeToServer(identification, prevSlotType, prevSlotIndex, newSlotType, newSlotIndex); // 서버에 요청을 보내는 메서드

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
    #endregion

    #region WepServer
    private async UniTask<(bool, long)> PostServerData(ServerRoomData roomData, string ip, string port)
    {
        string additiveUrl = "server/register/";
        
        // POST 데이터 생성
        WWWForm form = new WWWForm();
        
        form.AddField("ServerName", roomData.ServerName);
        form.AddField("ServerOwner", roomData.ServerOwner);
        form.AddField("ServerCurrentConnection", roomData.ServerCurrentConnection);
        form.AddField("ServerMaxConnectionLimit", roomData.ServerMaxConnectionLimit);
        form.AddField("ServerPassword", roomData.ServerPassword);
        form.AddField("IP", ip);
        form.AddField("Port", port);
        
        // HTTP POST 방식으로 요청 보내기
        using (var request = UnityWebRequest.Post(url+additiveUrl, form))
        {
            await request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("서버 등록 실패: " + request.error);
                return (false, -1);
            }
            string jsonResponse = request.downloadHandler.text;
            ServerResponse response = JsonUtility.FromJson<ServerResponse>(jsonResponse);
            Debug.Log("서버가 성공적으로 등록되었습니다. serverId: " + response.serverId);
            return (true, response.serverId);
        }
    }
    private async UniTask<List<ServerRoomData>> GetServerRoomList()
    {
        string additiveUrl = "server/GetServerRoomList/";
        
        // HTTP POST 방식으로 요청 보내기
        using (var request = UnityWebRequest.Get(url + additiveUrl))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to get server room list: {request.error}");
                return null;
            }

            string jsonResponse = request.downloadHandler.text;
            
            List<ServerRoomData> serverRoomDataList = JsonConvert.DeserializeObject<List<ServerRoomData>>(jsonResponse);
            return serverRoomDataList;
        }
    }
    private async UniTask<ServerData> GetServerData(string serverId)
    {
        string additiveUrl = "server/GetData/";
        
        // WWWForm 인스턴스 생성
        WWWForm form = new WWWForm();
        form.AddField("serverId",serverId);
        
        // HTTP POST 방식으로 요청 보내기
        using (var request = UnityWebRequest.Post(url+additiveUrl, form))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to get character info: {request.error}");
                return new ServerData();
            }
            
            string jsonResponse = request.downloadHandler.text;
            ServerData serverData = JsonUtility.FromJson<ServerData>(jsonResponse);

            return serverData;
        }
    }
    private async UniTask<PlayerData> GetPlayerData(string playerId, string password)
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
    private async UniTask<Dictionary<int, Item>> GetInventoryData(string playerId)
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
    private async UniTask<Dictionary<int, Item>> GetEquipmentData(string playerId)
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
    private async UniTask<bool> PostUpdateCharacterInfo(PlayerData playerData)
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
    private async UniTask<bool> PostItemSlotChangeToServer(string identification, SlotType prevSlotType, int prevSlotIndex, SlotType newSlotType, int newSlotIndex)
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
    #endregion
}

public struct ServerData
{
    public string IP;
    public string Port;
}
[System.Serializable]
public class ServerRoomDataList
{
    public ServerRoomData[] serverRooms; // 서버 방 데이터를 담는 배열
}