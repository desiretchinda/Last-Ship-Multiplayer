using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyComponent : MonoBehaviourPunCallbacks
{
    public ShipLookDirection lookDir;

    public SpriteRenderer render;

    public float speed = 2;

    public float delaySpeedAdd = 15;

    public float speedToAdd = 0.2f;

    float counterSpeedAdd = 15;

    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {

        //if(PhotonNetwork.IsMasterClient && PhotonNetwork.IsMasterClient)
        //{
        //    if (transform.position.y < PlayerManager.Instance.transform.position.y && !render.isVisible)
        //    {
        //        Destroy(gameObject);
        //        return;
        //    }
        //}

        
        UpdateSpeed();

        transform.position += moveDir * speed * Time.deltaTime;

    }

    void UpdateSpeed()
    {
        counterSpeedAdd -= Time.deltaTime;
        if (counterSpeedAdd <= 0)
        {
            speed += speedToAdd;
            counterSpeedAdd = delaySpeedAdd;
        }
    }

    public void SetUp()
    {
        switch (lookDir)
        {
            case ShipLookDirection.Up:
                transform.rotation = new Quaternion(0, 0, 0, 0);
                moveDir = Vector3.up;
                break;
            case ShipLookDirection.Down:
                transform.rotation = new Quaternion(0, 0, 180, 0);
                moveDir = Vector3.down;
                break;
            default:
                break;
        }
    }
}
