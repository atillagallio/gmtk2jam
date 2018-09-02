using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAvatar : MonoBehaviour
{
  public Animator Anim;
  public CharacterController charController;
  void Start()
  {
    Anim = GetComponent<Animator>();
    charController = GetComponent<CharacterController>();
  }

  void Update()
  {
  }
}
