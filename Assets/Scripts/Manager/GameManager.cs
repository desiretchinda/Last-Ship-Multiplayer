using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum ShipLookDirection
{
    Up,
    Down
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float delaySpawnEnemy = 2f;

    public List<Transform> spawnEnemyPos = new List<Transform>();

    public Transform playerSpawnPoint;

    public PlayerManager playerPrefab;

    public BulletComponent bulletPrefab;

    public EnemyComponent enemyPrefab;

    public RectTransform canvasTransform;

    public UI_PlayerName uiPlayerName;

    public GameObject redExplosion;

    public Camera mainCamera;

    float counterDelaySpawnEnemy;

    private void Awake()
    {
        Instance = this;
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
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            SpawnEnemy();
        }
        
    }

    public void SpawnPlayer()
    {
        if (!playerPrefab)
            return;

        if (!playerSpawnPoint)
            return;

        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint.position, Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        counterDelaySpawnEnemy -= Time.deltaTime;
        if (counterDelaySpawnEnemy <= 0)
        {

            var enemy = PhotonNetwork.InstantiateRoomObject(GameManager.Instance.enemyPrefab.name, GameManager.Instance.spawnEnemyPos[Random.Range(0, GameManager.Instance.spawnEnemyPos.Count - 1)].position, Quaternion.identity);

            if (enemy)
            {
                enemy.GetComponent<EnemyComponent>().SetUp();
            }
            
            counterDelaySpawnEnemy = delaySpawnEnemy;
        }
    }
}
