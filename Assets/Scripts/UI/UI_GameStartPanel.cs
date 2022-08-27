using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameStartPanel : MonoBehaviour
{
    public static UI_GameStartPanel Instance;

    public GameObject btnPlay;

    public GameObject txtConnecting;


    private void Awake()
    {
        Instance = this;
        DisplayBtnPlay(false);
        DisplayTxtConnecting(true);
    }

    public void DisplayBtnPlay(bool state = true)
    {
        btnPlay.SetActive(state);
    }

    public void DisplayTxtConnecting(bool state = true)
    {
        txtConnecting.SetActive(state);
    }
}
