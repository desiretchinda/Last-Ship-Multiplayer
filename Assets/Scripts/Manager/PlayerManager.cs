using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    public float speed = 200;

    private Vector2 movement = Vector2.zero;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        movement.x = Input.GetAxis("Horizontal");

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


}
