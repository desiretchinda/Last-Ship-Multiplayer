using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{

    public float speed = 5000;

    public ShipLookDirection lookDir = ShipLookDirection.Up;

    public Vector3 moveDir = Vector3.up;

    public SpriteRenderer spRender;
 
    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //if(boxCollider.GetContacts(contactColliders) > 0)
        //{
        //    for (int i = 0, length = contactColliders.Length; i < length; i++)
        //    {
        //        if(contactColliders[i] != null)
        //        {
        //            Debug.Log("enemy touch "+contactColliders[i].gameObject);
        //        }
        //    }
        //}

        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Instantiate(GameManager.Instance.redExplosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);            
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

    void Move()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }

    public void PlayShoot()
    {
        audioSource.Play();
    }

}
