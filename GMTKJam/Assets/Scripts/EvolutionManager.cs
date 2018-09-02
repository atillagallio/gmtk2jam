using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{

  // Use this for initialization
  public CharacterStats charInfo;
  public int adultAge;
  public int seniorAge;

  public float evolutionThreshold;

  public enum EvolutionType
  {
    AdultNormal,
    Entrepreneur,
    Hippie,
    Punk,
    OldNormal,
    OldAtlete,
    OldWheelchair,
    OldZen,
  }

  public EvolutionType VerifyEvolution()
  {

    int entrepreneurInfluence = 0;
    int hippieInfluence = 0;
    int punkInfluence = 0;
    int atleteInfluence = 0;
    int wheelchairInfluence = 0;
    int zenInfluence = 0;


    Dictionary<EvolutionType, Vector2> evolutionVector = new Dictionary<EvolutionType, Vector2>();
    evolutionVector.Add(EvolutionType.Entrepreneur, Vector2.right);
    evolutionVector.Add(EvolutionType.Hippie, Vector2.right.Rotate(120f));
    evolutionVector.Add(EvolutionType.Punk, Vector2.right.Rotate(240f));
    evolutionVector.Add(EvolutionType.OldAtlete, Vector2.right);
    evolutionVector.Add(EvolutionType.OldWheelchair, Vector2.right.Rotate(120f));
    evolutionVector.Add(EvolutionType.OldZen, Vector2.right.Rotate(240f));


    foreach (var item in charInfo.ItemCount)
    {
      entrepreneurInfluence += item.Key.entrepreneurInfluence * item.Value;
      hippieInfluence += item.Key.hippieInfluence * item.Value;
      punkInfluence += item.Key.punkInfluence * item.Value;
      atleteInfluence += item.Key.atleteInfluence * item.Value;
      wheelchairInfluence += item.Key.wheelchairInfluence * item.Value;
      zenInfluence += item.Key.zenInfluence * item.Value;
    }

    evolutionVector[EvolutionType.Entrepreneur] *= entrepreneurInfluence;
    evolutionVector[EvolutionType.Hippie] *= hippieInfluence;
    evolutionVector[EvolutionType.Punk] *= punkInfluence;
    evolutionVector[EvolutionType.OldAtlete] *= atleteInfluence;
    evolutionVector[EvolutionType.OldWheelchair] *= wheelchairInfluence;
    evolutionVector[EvolutionType.OldZen] *= zenInfluence;

    Vector2 resultVector = Vector2.zero;
    Dictionary<float, EvolutionType> EvolutionDots = new Dictionary<float, EvolutionType>();
    if (charInfo.Age >= adultAge && charInfo.Age <= seniorAge)
    {
      resultVector = evolutionVector[EvolutionType.Entrepreneur] + evolutionVector[EvolutionType.Hippie] + evolutionVector[EvolutionType.Punk];
      EvolutionDots.Add(Vector2.Dot(resultVector, evolutionVector[EvolutionType.Entrepreneur]), EvolutionType.Entrepreneur);
      EvolutionDots.Add(Vector2.Dot(resultVector, evolutionVector[EvolutionType.Hippie]), EvolutionType.Hippie);
      EvolutionDots.Add(Vector2.Dot(resultVector, evolutionVector[EvolutionType.Punk]), EvolutionType.Punk);
    }
    else if (charInfo.Age >= seniorAge)
    {
      resultVector = evolutionVector[EvolutionType.OldAtlete] + evolutionVector[EvolutionType.OldWheelchair] + evolutionVector[EvolutionType.OldZen];
      EvolutionDots.Add(Vector2.Dot(resultVector, evolutionVector[EvolutionType.OldAtlete]), EvolutionType.OldAtlete);
      EvolutionDots.Add(Vector2.Dot(resultVector, evolutionVector[EvolutionType.OldWheelchair]), EvolutionType.OldWheelchair);
      EvolutionDots.Add(Vector2.Dot(resultVector, evolutionVector[EvolutionType.OldZen]), EvolutionType.OldZen);
    }


    if (resultVector.magnitude <= evolutionThreshold)
      return EvolutionType.AdultNormal;


    float biggestDotValue = 0;
    foreach (var value in EvolutionDots.Keys)
    {
      if (value >= biggestDotValue) biggestDotValue = value;
    }

    print(EvolutionDots[biggestDotValue]);
    return EvolutionDots[biggestDotValue];
  }
}
