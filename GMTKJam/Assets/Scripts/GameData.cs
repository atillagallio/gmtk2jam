using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "Data/Game")]
public class GameData : ScriptableObject
{
  public float GameDuration;
  public float MaxLifetime;
  public float ItemSpawnInterval;
  public float Speed;
  public float JumpHeight;
  public float JumpDuration;
}
