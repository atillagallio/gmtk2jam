using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoBg : MonoBehaviour
{

    public float speedRightAxis;
    public float speedUpAxis;
    public float speedForwardAxis;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * speedRightAxis);
        transform.Rotate(Vector3.up * Time.deltaTime * speedUpAxis);
        transform.Rotate(Vector3.forward * Time.deltaTime * speedForwardAxis);

    }
}
