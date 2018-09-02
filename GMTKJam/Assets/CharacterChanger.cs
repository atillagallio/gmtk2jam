using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{

  private EvolutionManager _evolutionManager;
  private EvolutionManager EvolutionManager
  {
    get
    {
      if (_evolutionManager == null)
      {
        _evolutionManager = FindObjectOfType<EvolutionManager>();
      }
      return _evolutionManager;
    }
  }

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
