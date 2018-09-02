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
    Func<bool, Action<ParallaxThing>> toggleRenderer = b =>
    {
      return (e) =>
      {
        var sprite = e.gameObject.GetComponent<SpriteRenderer>();
        if (sprite == null) return;
        sprite.enabled = b;
      };
    };
    EventManager.OnEndGameEvent += () =>
      {
        LayerCopy.ForEach(toggleRenderer(false));
        LayerDefinition.ForEach(toggleRenderer(false));
      };
    EventManager.OnStartGameEvent += () =>
        {
          LayerCopy.ForEach(toggleRenderer(true));
          LayerDefinition.ForEach(toggleRenderer(true));
        };
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

