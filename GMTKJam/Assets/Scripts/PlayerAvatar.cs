using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAvatar : MonoBehaviour
{
  public Animator _anim;

  public Animator Anim
  {
    get
    {
      if (_anim == null)
      {
        _anim = GetComponent<Animator>();
      }
      return _anim;
    }
  }
}
