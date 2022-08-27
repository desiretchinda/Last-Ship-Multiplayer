using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform playerSpawnPoint;

    public PlayerManager playerPrefab;

    public BulletComponent bulletPrefab;

    public RectTransform canvasTransform;

    public UI_PlayerName uiPlayerName;

    public Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
        SpawnPlayer();
        UIFader.Instance.Fade(UIFader.FADE.FadeIn, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPlayer()
    {
        if (!playerPrefab)
            return;

        if (!playerSpawnPoint)
            return;

        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint.position, Quaternion.identity);
    }

}
