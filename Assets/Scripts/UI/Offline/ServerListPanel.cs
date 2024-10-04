using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using kcp2k;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ServerListPanel : MonoBehaviour
{
     [SerializeField] private Transform Context;
     private List<ServerRoomPanel> ServerRoomPanels = new List<ServerRoomPanel>();

     private ServerRoomPanel selectedServerRoomPanel;

     [SerializeField] private GameObject serverRoomPanelPrefab;

     public Color highlightedColor = Color.blue; // 강조 색상
     public Color normalColor = Color.white; // 기본 색상

     private void OnEnable()
     {
          InitServerList().Forget();
     }
     private void OnDisable()
     {
          foreach (var serverRoomPanel in ServerRoomPanels)
          {
               Destroy(serverRoomPanel);
          }
          serverRoomPanelPrefab = null;
     }

     private async UniTask InitServerList()
     {
          List<ServerRoomData> serverRoomDataList = await WepServerConnectionManager.Instance.RequestServerRoomList();
          Debug.Log(serverRoomDataList.Count);
          foreach (var serverRoomData in serverRoomDataList)
          {
               ServerRoomPanel serverRoomPanel = Instantiate(serverRoomPanelPrefab, Context).GetComponent<ServerRoomPanel>();
               serverRoomPanel.SetServerInfo(serverRoomData);
               serverRoomPanel.OnClickEvent += OnClickElement;
               ServerRoomPanels.Add(serverRoomPanel);
          }
     }

     private void OnClickElement(ServerRoomPanel clickedPanel)
     {
          // 클릭된 버튼 강조 표시, 다른 버튼은 강조 비활성화
          foreach (var serverRoomPanel in ServerRoomPanels)
          {
               if (serverRoomPanel == clickedPanel)
               {
                    serverRoomPanel.BackgroundImage.color = highlightedColor; // 강조 색상 적용
                    selectedServerRoomPanel = serverRoomPanel;
               }
               else
               {
                    serverRoomPanel.BackgroundImage.color = normalColor; // 기본 색상 적용
               }
          }
     }

     public void OnEnterServer()
     {
          if (selectedServerRoomPanel == null)
               return;
          Debug.Log("서버에 접속");
          Debug.Log($"{selectedServerRoomPanel.RoomData.ServerName}");
          Debug.Log($"{selectedServerRoomPanel.RoomData.ServerOwner}");
          Debug.Log($"{selectedServerRoomPanel.RoomData.ServerCurrentConnection}");
          Debug.Log($"{selectedServerRoomPanel.RoomData.ServerMaxConnectionLimit}");

          // selectedServerRoomPanel.RoomData 에 대한 정보를 웹서버에서 받아옴
          // networkAddress IP 번호와 port 번호를 받아옴
          
          NetworkManager.singleton.networkAddress = "localHost";
          NetworkManager.singleton.GetComponent<KcpTransport>().port = 7777;
          NetworkManager.singleton.StartClient();
     }
}
