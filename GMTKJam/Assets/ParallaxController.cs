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

  }
}
