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

    private void Start()
    {
        UIFader.Instance.Fade(UIFader.FADE.FadeIn, .5f, 1f);
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to server succesfull");

        UI_GameStartPanel.Instance.DisplayBtnPlay(true);
        UI_GameStartPanel.Instance.DisplayTxtConnecting(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("On disconnect from server " + cause);
    }

    public void OnClick_JoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        UIFader.Instance.Fade(UIFader.FADE.FadeOut, .5f, 0f, () =>
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: options);
        });
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: options);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.LoadLevel("GamePlay");

        }
    }
}
