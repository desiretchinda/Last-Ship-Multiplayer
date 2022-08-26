using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public string gameversion = "0.0.1";

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameversion;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        DontDestroyOnLoad(this);
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to server succesfull");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("On disconnect from server " + cause);
    }
}
