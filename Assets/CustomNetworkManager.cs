using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
     public long serverId;
     public static CustomNetworkManager Singleton => (CustomNetworkManager)singleton;

     public Dictionary<NetworkConnectionToClient, int> playerDictionary = new Dictionary<NetworkConnectionToClient, int>();
     
     public override void OnClientConnect()
     {
          base.OnClientConnect();
          
          Debug.Log("I connected to a server!");
        
          autoCreatePlayer = true; // set the autoCreateFlag for the Network Manager (clients and host)
     }

     public override void OnServerChangeScene(string newSceneName){
     }

     public void myStartHost(){
          StartHost(); // manually start host
     }
     public void myJoinGame(){
          StartClient(); // manually start client
     }

     public override void OnServerAddPlayer(NetworkConnectionToClient conn)
     {
          base.OnServerAddPlayer(conn);
        
          // Custom player code below

          //MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
          //player.SetDisplayName($"Player {numPlayers}");
          //player.SetDisplayColor(new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
          
          playerDictionary.Add(conn, numPlayers);

          Debug.Log("A Player was added");
          Debug.Log($"There are now {numPlayers} players");
     }

     #region Event
     public event Action OnPlayerConnetServer;
     public event Action OnPlayerTeleport;
     public event Action OnPlayer;
     #endregion
}
