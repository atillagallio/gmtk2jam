using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class ParallaxThing : MonoBehaviour
{
  // Update is called once per frame
  public float Side
  {
    get
    {
      return collider.size.x * transform.localScale.x;
    }
  }
  private BoxCollider collider;
  void Awake()
  {
    collider = GetComponent<BoxCollider>();
  }

  void Update()
  {
    transform.localScale = 0.5f * Vector3.one * Mathf.Tan(Camera.main.fieldOfView / 2) * (-transform.position.z + Camera.main.transform.position.z);
  }
}
