using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

  PlayerController ThingToFollow;
  private Vector3 offset;

  // Does not follow in Y direction
  void Start()
  {
    ThingToFollow = FindObjectOfType<PlayerController>();
    offset = transform.position - ThingToFollow.transform.position;
  }

  void Update()
  {
    transform.position =
      Vector3.Scale(transform.position, Vector3.up) +
      Vector3.Scale(ThingToFollow.currentAvatar.transform.position + offset, Vector3.one - Vector3.up);
  }
}
