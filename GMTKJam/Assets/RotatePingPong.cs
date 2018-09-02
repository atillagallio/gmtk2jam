using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePingPong : MonoBehaviour
{

  Vector3 axis;
  float Speed = 5;

  float Range = 10;

  private float offset;

  int type;
  int types = 3;
  Vector3 initialPosition;
  Vector3 initialScale;


  void Start()
  {
    offset = Random.Range(0, Mathf.PI);
    axis = Random.Range(0f, 1.0f) > 0.5 ? Vector3.forward : Vector3.up;
    Range = Random.Range(8.0f, 20.0f);
    Speed = Random.Range(1.0f, 5.0f);
    type = Random.Range(0, types);
    initialPosition = transform.position;
    initialScale = transform.localScale;
  }
  // Update is called once per frame
  void Update()
  {
    if (type == 1)
    {
      transform.rotation = Quaternion.AngleAxis(Range * Mathf.Sin(offset + Speed * Time.time), axis);
    }
    else if (type == 2)
    {
      transform.position = initialPosition + 0.1f * Vector3.up * Range * Mathf.Sin(offset + Speed * Time.time);
    }
    else if (type == 3)
    {
      transform.localScale = initialScale + Quaternion.AngleAxis(90, Vector3.up) * axis * Range * Mathf.Sin(offset + Speed * Time.time);
    }
  }
}
