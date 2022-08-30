using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbySscene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        UIFader.Instance.Fade(UIFader.FADE.FadeIn, .5f, 1f);

        NetworkManager.Instance.Connect();
    }

    public void OnClick_JoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;


        if (string.IsNullOrEmpty(UI_GameStartPanel.Instance.txtName.text))
        {
            UI_GameStartPanel.Instance.DisplayError(true);
            return;
        }

        PhotonNetwork.NickName = UI_GameStartPanel.Instance.txtName.text;
        PlayerPrefs.SetString("nickname", PhotonNetwork.NickName);

        UIFader.Instance.Fade(UIFader.FADE.FadeOut, .5f, 0f, () =>
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: options);
        });
    }

    public void OnClick_PlayOffline()
    {
       
        if (string.IsNullOrEmpty(UI_GameStartPanel.Instance.txtName.text))
        {
            UI_GameStartPanel.Instance.DisplayError(true);
            return;
        }

        PhotonNetwork.NickName = UI_GameStartPanel.Instance.txtName.text;

        //UIFader.Instance.Fade(UIFader.FADE.FadeOut, .5f, 0f, () =>
        //{
        //    RoomOptions options = new RoomOptions();
        //    options.MaxPlayers = 4;
        //    PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: options);
        //});
    }

    public void OnClick_PlayVS()
    {
        if (!PhotonNetwork.IsConnected)
            return;


        if (string.IsNullOrEmpty(UI_GameStartPanel.Instance.txtName.text))
        {
            UI_GameStartPanel.Instance.DisplayError(true);
            return;
        }

        PhotonNetwork.NickName = UI_GameStartPanel.Instance.txtName.text;

        //UIFader.Instance.Fade(UIFader.FADE.FadeOut, .5f, 0f, () =>
        //{
        //    RoomOptions options = new RoomOptions();
        //    options.MaxPlayers = 4;
        //    PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: options);
        //});
    }

}
