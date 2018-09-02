using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EvolutionType
{
  Crianca,
  AdultoPunk,
  AdultoEmpresario,
  AdultoHippie,
  AdultoNormal,
  VelhoNormal,
  VelhoAtleta,
  VelhoCadeirante,
  VelhoZen,
}
public class EvolutionManager : MonoBehaviour
{

  // Use this for initialization
  public CharacterStats charInfo;
  public int adultAge;
  public int seniorAge;

  public float evolutionThreshold;

  public EvolutionType VerifyEvolution()
  {

    int entrepreneurInfluence = 0;
    int hippieInfluence = 0;
    int punkInfluence = 0;
    int atleteInfluence = 0;
    int wheelchairInfluence = 0;
    int zenInfluence = 0;

    Dictionary<EvolutionType, Vector2> evolutionVector = new Dictionary<EvolutionType, Vector2>();
    evolutionVector.Add(EvolutionType.AdultoEmpresario, Vector2.right);
    evolutionVector.Add(EvolutionType.AdultoHippie, Vector2.right.Rotate(120f));
    evolutionVector.Add(EvolutionType.AdultoPunk, Vector2.right.Rotate(240f));
    evolutionVector.Add(EvolutionType.VelhoAtleta, Vector2.right);
    evolutionVector.Add(EvolutionType.VelhoCadeirante, Vector2.right.Rotate(120f));
    evolutionVector.Add(EvolutionType.VelhoZen, Vector2.right.Rotate(240f));
    evolutionVector.Add(EvolutionType.Crianca, Vector2.right.Rotate(240f));


    foreach (var item in charInfo.ItemCount)
    {
      entrepreneurInfluence += item.Key.entrepreneurInfluence * item.Value;
      hippieInfluence += item.Key.hippieInfluence * item.Value;
      punkInfluence += item.Key.punkInfluence * item.Value;
      atleteInfluence += item.Key.atleteInfluence * item.Value;
      wheelchairInfluence += item.Key.wheelchairInfluence * item.Value;
      zenInfluence += item.Key.zenInfluence * item.Value;
    }

    evolutionVector[EvolutionType.AdultoEmpresario] *= entrepreneurInfluence;
    evolutionVector[EvolutionType.AdultoHippie] *= hippieInfluence;
    evolutionVector[EvolutionType.AdultoPunk] *= punkInfluence;
    evolutionVector[EvolutionType.VelhoAtleta] *= atleteInfluence;
    evolutionVector[EvolutionType.VelhoCadeirante] *= wheelchairInfluence;
    evolutionVector[EvolutionType.VelhoZen] *= zenInfluence;

    Vector2 resultVector = Vector2.zero;
    Dictionary<EvolutionType, float> EvolutionDots = new Dictionary<EvolutionType, float>();
    if (charInfo.Age < adultAge)
    {
      return EvolutionType.Crianca;
    }
    else if (charInfo.Age >= adultAge && charInfo.Age <= seniorAge)
    {
      resultVector = evolutionVector[EvolutionType.AdultoEmpresario] + evolutionVector[EvolutionType.AdultoHippie] + evolutionVector[EvolutionType.AdultoPunk];
      EvolutionDots.Add(EvolutionType.AdultoEmpresario, Vector2.Dot(resultVector, evolutionVector[EvolutionType.AdultoEmpresario]));
      EvolutionDots.Add(EvolutionType.AdultoHippie, Vector2.Dot(resultVector, evolutionVector[EvolutionType.AdultoHippie]));
      EvolutionDots.Add(EvolutionType.AdultoPunk, Vector2.Dot(resultVector, evolutionVector[EvolutionType.AdultoPunk]));
      if (resultVector.magnitude <= evolutionThreshold)
        return EvolutionType.AdultoNormal;
    }
    else if (charInfo.Age >= seniorAge)
    {
      resultVector = evolutionVector[EvolutionType.VelhoAtleta] + evolutionVector[EvolutionType.VelhoCadeirante] + evolutionVector[EvolutionType.VelhoZen];
      EvolutionDots.Add(EvolutionType.VelhoAtleta, Vector2.Dot(resultVector, evolutionVector[EvolutionType.VelhoAtleta]));
      EvolutionDots.Add(EvolutionType.VelhoCadeirante, Vector2.Dot(resultVector, evolutionVector[EvolutionType.VelhoCadeirante]));
      EvolutionDots.Add(EvolutionType.VelhoZen, Vector2.Dot(resultVector, evolutionVector[EvolutionType.VelhoZen]));
      if (resultVector.magnitude <= evolutionThreshold)
        return EvolutionType.VelhoNormal;
    }

    EvolutionType selectedType = EvolutionType.Crianca;
    float biggestDotValue = -1;
    foreach (var kvp in EvolutionDots)
    {
      if (kvp.Value >= biggestDotValue)
      {
        selectedType = kvp.Key;
        biggestDotValue = kvp.Value;
      }
    }

    //print(EvolutionDots[biggestDotValue]);
    return selectedType;
  }
}
