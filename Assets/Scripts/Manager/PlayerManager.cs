using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    public float speed = 200;

    private Vector2 movement = Vector2.zero;

    public Transform spawnBulletPt;

    public float delaySpawnBullet = 0.5f;

    float counterDelaySpawnBullet;

    Rigidbody2D rigidbody2;
    public SpriteRenderer spRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        if (photonView.IsMine)
        {
            spRenderer.sortingOrder += 1;
            spRenderer.color = Color.white;
        }

        SetPlayerName();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        movement.x = Input.GetAxis("Horizontal");

        SpawnBullet();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        PlayerMovement();
    }

    void PlayerMovement()
    {
        rigidbody2.velocity = movement * speed * Time.deltaTime;

    }

    void SpawnBullet()
    {
        counterDelaySpawnBullet -= Time.deltaTime;

        if (counterDelaySpawnBullet <= 0)
        {
            photonView.RPC("RPC_SpawnBullet", RpcTarget.AllViaServer);
            counterDelaySpawnBullet = delaySpawnBullet;
        }
    }

    [PunRPC]
    void RPC_SpawnBullet()
    {
        var bullet = Instantiate(GameManager.Instance.bulletPrefab, spawnBulletPt.position, Quaternion.identity);
        if (photonView.IsMine)
            bullet.spRender.color = Color.white;
    }


    void SetPlayerName()
    {
        var playerName = Instantiate(GameManager.Instance.uiPlayerName, GameManager.Instance.canvasTransform);
        playerName.SetOwner(this);
    }
}
