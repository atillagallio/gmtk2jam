using UnityEngine;

public class GlobalData : MonoBehaviour
{
  private static GlobalData _instance;
  public static GlobalData Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = FindObjectOfType<GlobalData>();
      }
      return _instance;
    }
  }

  [SerializeField]
  private GameData _gameData;
  public static GameData GameData
  {
    get
    {
      return Instance._gameData;
    }
  }

}