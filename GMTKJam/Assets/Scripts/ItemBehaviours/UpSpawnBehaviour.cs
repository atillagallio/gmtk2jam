using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSpawnBehaviour : MonoBehaviour
{

    // Use this for initialization
    private float y = 5f;
    void Start()
    {
        var transform = GetComponent<Transform>();
        transform.position += Vector3.up * y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
