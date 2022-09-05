using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public static PlayerManager Instance;

    public float speed = 200;

    private Vector2 movement = Vector2.zero;

    public Transform spawnBulletPt;

    public float delaySpawnBullet = 0.5f;

    float counterDelaySpawnBullet;

    Rigidbody2D rigidbody2;

    [SerializeField]
    BoxCollider2D boxCollider;

    public SpriteRenderer spRenderer;

    [HideInInspector]
    public bool isDead;

    bool canMove;

    float lag;

    private void Awake()
    {
        Instance = this;
    }

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

        if (isDead)
            return;

        if (Application.platform == RuntimePlatform.Android)
        {
            movement.x = Input.acceleration.x;
        }
        else
        {
            movement.x = Input.GetAxis("Horizontal");
        }

        SpawnBullet();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        if (isDead)
            return;

        PlayerMovement();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (rigidbody2 == null)
            return;

        if (stream.IsWriting)
        {
            stream.SendNext(rigidbody2.position);
            stream.SendNext(rigidbody2.velocity);
        }
        else
        {
            rigidbody2.position = (Vector2)stream.ReceiveNext();
            rigidbody2.velocity = (Vector2)stream.ReceiveNext();

            lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            rigidbody2.position += rigidbody2.velocity * lag;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isDead)
        {
            Instantiate(GameManager.Instance.redExplosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Instantiate(GameManager.Instance.redExplosion, transform.position, transform.rotation);
            spRenderer.enabled = false;
            isDead = true;
            PhotonNetwork.LeaveRoom();
            //StartCoroutine(QuitRoom());
        }
    }

    IEnumerator QuitRoom()
    {
        yield return new WaitForSeconds(2);

        UIFader.Instance.Fade(UIFader.FADE.FadeOut, .5f, 1f, () =>
        {
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.LeaveRoom();

        });
    }

    private void OnDestroy()
    {

    }

    void PlayerMovement()
    {

        rigidbody2.velocity = movement * speed * Time.deltaTime;
        rigidbody2.position = new Vector2(Mathf.Clamp(rigidbody2.position.x, -2f, 2), rigidbody2.position.y);
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
        bullet.SetUp();
        if (photonView.IsMine)
        {
            bullet.spRender.color = Color.white;
            bullet.SetPitch();
            bullet.PlayShoot();
        }

    }

    void SetPlayerName()
    {
        var playerName = Instantiate(GameManager.Instance.uiPlayerName, GameManager.Instance.canvasTransform);
        playerName.SetOwner(this);
    }
}
