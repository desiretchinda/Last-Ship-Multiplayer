using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UI_GameStartPanel : MonoBehaviour
{
    public static UI_GameStartPanel Instance;

    public GameObject btnPlay;

    public GameObject txtConnecting;

    public GameObject txtErrorName;

    public TMPro.TMP_InputField txtName;

    private void Awake()
    {
        Debug.Log("awake panel");
        Instance = this;
        
        if(PhotonNetwork.IsConnected)
        {
            DisplayBtnPlay(true);
            DisplayTxtConnecting(false);
            DisplayError(false);
        }
        else
        {
            DisplayBtnPlay(false);
            DisplayTxtConnecting(true);
            DisplayError(false);
        }

    }

    private void Start()
    {
        txtName.text = PlayerPrefs.GetString("nickname", string.Empty);
    }

    public void DisplayBtnPlay(bool state = true)
    {
        if (btnPlay)
            btnPlay.SetActive(state);
    }

    public void DisplayTxtConnecting(bool state = true)
    {
        if (txtConnecting)
            txtConnecting.SetActive(state);
    }

    public void DisplayError(bool state)
    {
        if (txtErrorName)
            txtErrorName.SetActive(state);
    }
}
