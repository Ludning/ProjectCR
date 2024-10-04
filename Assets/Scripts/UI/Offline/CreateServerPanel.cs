using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cysharp.Threading.Tasks;
using Mirror;
using TMPro;
using UnityEngine;

public class CreateServerPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField TMP_ServerName;
    [SerializeField] private TMP_InputField TMP_ServerPassword;
    [SerializeField] private TMP_InputField TMP_ServerMaxLimit;
    private DifficultyLevelType SelectedDifficultyLevel = DifficultyLevelType.EASY;
    
    [SerializeField] private DifficultyLevelNode EasyDifficultyLevelNode;
    [SerializeField] private DifficultyLevelNode NormalDifficultyLevelNode;
    [SerializeField] private DifficultyLevelNode HardDifficultyLevelNode;
    [SerializeField] private DifficultyLevelNode HellDifficultyLevelNode;
    
    
    public Color highlightedColor = Color.blue; // 강조 색상
    public Color normalColor = Color.white; // 기본 색상
    
    private void OnEnable()
    {
        EasyDifficultyLevelNode.OnClickEvent += OnClickElement;
        NormalDifficultyLevelNode.OnClickEvent += OnClickElement;
        HardDifficultyLevelNode.OnClickEvent += OnClickElement;
        HellDifficultyLevelNode.OnClickEvent += OnClickElement;
        
        EasyDifficultyLevelNode.BackgroundImage.color = highlightedColor;
        NormalDifficultyLevelNode.BackgroundImage.color = normalColor;
        HardDifficultyLevelNode.BackgroundImage.color = normalColor;
        HellDifficultyLevelNode.BackgroundImage.color = normalColor;
    }

    private void OnDisable()
    {
        EasyDifficultyLevelNode.OnClickEvent -= OnClickElement;
        NormalDifficultyLevelNode.OnClickEvent -= OnClickElement;
        HardDifficultyLevelNode.OnClickEvent -= OnClickElement;
        HellDifficultyLevelNode.OnClickEvent -= OnClickElement;
    }

    private void OnClickElement(DifficultyLevelNode clickedPanel)
    {
        List<DifficultyLevelNode> difficultyLevelNodes = new List<DifficultyLevelNode>()
        {
            EasyDifficultyLevelNode,
            NormalDifficultyLevelNode,
            HardDifficultyLevelNode,
            HellDifficultyLevelNode,
        };

        foreach (var node in difficultyLevelNodes)
        {
            if (node == clickedPanel)
            {
                node.BackgroundImage.color = highlightedColor; // 강조 색상 적용
                
                if (node == EasyDifficultyLevelNode)
                    SelectedDifficultyLevel = DifficultyLevelType.EASY;
                else if (node == NormalDifficultyLevelNode)
                    SelectedDifficultyLevel = DifficultyLevelType.NORMAL;
                else if (node == HardDifficultyLevelNode)
                    SelectedDifficultyLevel = DifficultyLevelType.HARD;
                else if (node == HellDifficultyLevelNode)
                    SelectedDifficultyLevel = DifficultyLevelType.HELL;
            }
            else
            {
                node.BackgroundImage.color = normalColor; // 기본 색상 적용
            }
        }
    }

    public void OnCreateServer()
    {
        if (string.IsNullOrWhiteSpace(TMP_ServerName.text))
        {
            Debug.LogWarning("서버이름을 입력해주세요");
            return;
        }
        if (string.IsNullOrWhiteSpace(TMP_ServerMaxLimit.text))
        {
            Debug.LogWarning("서버인원수를 입력해주세요");
            return;
        }
        
        int serverMaxLimit = int.TryParse(TMP_ServerMaxLimit.text, out int value) ? value : 4;
        
        ServerRoomData roomData = new ServerRoomData()
        {
            ServerName = TMP_ServerName.text,
            ServerOwner = "홍길동",
            //ServerOwner = UserDataManager.Instance.UserName,
            ServerCurrentConnection = 0,
            ServerMaxConnectionLimit = serverMaxLimit,
            ServerPassword = TMP_ServerPassword.text,
        };
        
        Debug.Log("서버 생성");
        Debug.Log($"{GetLocalIPAddress()}");
        string ipAddress = GetLocalIPAddress();
        CreateServer(roomData, ipAddress, "7777").Forget();
    }

    private async UniTask CreateServer(ServerRoomData roomData, string ipAddress, string port)
    {
        (bool value, long serverId) result = await WepServerConnectionManager.Instance.RegisterServerData(roomData, ipAddress, port);

        if (result.value == false)
            Debug.Log("웹 서버 등록 실패");
        else
        {
            CustomNetworkManager.Singleton.serverId = result.serverId;
            NetworkManager.singleton.StartHost();
        }
    }
    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}
