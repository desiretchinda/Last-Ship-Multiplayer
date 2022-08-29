using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyComponent : MonoBehaviourPunCallbacks
{
    public ShipLookDirection lookDir;

    public float speed = 2;

    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
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
