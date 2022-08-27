using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{

    public float speed = 5000;

    public Vector3 direction = Vector3.up; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
}
