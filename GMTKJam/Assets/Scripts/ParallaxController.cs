using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
  public List<ParallaxThing> LayerDefinition;
  public List<ParallaxThing> LayerCopy;

  void Start()
  {
    LayerCopy = LayerDefinition.Select(Instantiate).ToList();
    LayerCopy.ForEach((el) => el.transform.position += Vector3.right * el.Side);
  }

  // Update is called once per frame
  void Update()
  {
    Action<ParallaxThing> move = el =>
    {
      if (transform.position.x - el.transform.position.x > el.Side)
      {
        el.transform.position += 2.0f * Vector3.right * el.Side;
      }
    };
    LayerCopy.ForEach(move);
    LayerDefinition.ForEach(move);
  }
}

