using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{

    public float destroyDelay;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }


}
