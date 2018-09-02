using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

  public EvolutionManager evoManager;
  public GameObject baseItemPrefab;
  public GameObject player;

  public bool gameStarted = false;

  [SerializeField]
  List<Item> UniqueItems;
  IEnumerable<Item> PossibleUniqueItems
  {
    get
    {
      return UniqueItems.Where((i) => CheckItemPossibility(i, charInfo));
    }
  }
  [SerializeField]
  public List<Item> RepeatableItems;
  IEnumerable<Item> PossibleRepeatableItems
  {
    get
    {
      return RepeatableItems.Where((i) => CheckItemPossibility(i, charInfo));
    }
  }

  [SerializeField]
  List<Item> DeathItems;

  private Vector3 initialCharPos;
  void Awake()
  {
    var allItems = Resources.LoadAll<Item>("Items");
    RepeatableItems = allItems.Where((i) => (i.itemType == ItemType.Addictive || i.itemType == ItemType.Repeatable)).ToList();
    UniqueItems = allItems.Where((i) => (i.itemType == ItemType.Unique)).ToList();
    DeathItems = allItems.Where((i) => (i.itemType == ItemType.Death)).Shuffle().ToList();
    initialCharPos = player.transform.position;
  }

  public static void AddItem(Item i, CharacterStats c)
  {
    if (c.ItemCount.ContainsKey(i))
    {
      c.ItemCount[i]++;
    }
    else
    {
      c.ItemCount.Add(i, 1);
    }
  }

  static bool CheckItemPossibility(Item i, CharacterStats c)
  {
    if (c.Age < i.minAge) return false;
    if (i.maxAge > 0 && c.Age > i.maxAge) return false;
    if (i.conditionList.Count == 0) return true;

    // caso do or
    if (i.conditionType == ConditionType.Or)
    {
      var agg = false;
      foreach (var cond in i.conditionList)
      {
        int itemCount = 0;
        c.ItemCount.TryGetValue(i, out itemCount);
        agg = agg || cond.VerifyCondition(itemCount);
      }
      return agg;
    }
    else if (i.conditionType == ConditionType.And)
    {
      var agg = true;
      foreach (var cond in i.conditionList)
      {
        int itemCount = 0;
        c.ItemCount.TryGetValue(i, out itemCount);
        agg = agg && cond.VerifyCondition(itemCount);
      }
      return agg;
    }

    return false;
  }

  [SerializeField]
  public CharacterStats charInfo;

  public AnimationCurve DeathSpawnProbability;

  float GetDeathProbability()
  {
    var baseProb = DeathSpawnProbability.Evaluate(charInfo.Age / GlobalData.GameData.MaxLifetime);
    var incProb = 0.0f;
    foreach (var kvp in charInfo.ItemCount)
    {
      var item = kvp.Key;
      var count = kvp.Value;
      incProb += item.DeathProbabilityIncrement * count;
    }
    var prob = baseProb + incProb / 100.0f;
    print(prob);
    return prob;
  }

  bool ChooseSpawnItem(out Item item)
  {
    if (UnityEngine.Random.Range(0.0f, 1.0f) < GetDeathProbability())
    {
      item = DeathItems.FirstOrDefault();
      return item != null;
    }

    var isSpawningUnique = PossibleUniqueItems.Count() > 0;
    item = (isSpawningUnique ? PossibleUniqueItems : PossibleRepeatableItems.Shuffle()).FirstOrDefault();
    if (item != null && isSpawningUnique)
    {
      UniqueItems.Remove(item);
    }

    //RepeatableItems = RepeatableItems.Shuffle().ToList();
    /* 
    if (item.itemType == ItemType.Addictive)
    {
      RepeatableItems.Add(item);
    }
    */
    return item != null;
  }


  [Header("Spawn Info")]
  public float spawnCooldown;
  public float SpawnInterval;
  public float spawnDistance;
  // Use this for initialization
  void Start()
  {
    EventManager.OnStartGameEvent += () => { gameStarted = true; };
  }

  // Update is called once per frame
  void Update()
  {
    if (gameStarted)
      UpdateSpawnCooldown();
  }

  void UpdateSpawnCooldown()
  {
    spawnCooldown -= Time.deltaTime;
    if (spawnCooldown < 0)
    {
      spawnCooldown = SpawnInterval;
      SpawnItem();

    }
  }

  void SpawnItem()
  {
    Item i;
    if (ChooseSpawnItem(out i))
    {
      Vector3 itemPos = Vector3.right * spawnDistance;
      Vector3 playerDistance = new Vector3(player.transform.position.x - initialCharPos.x, 0, 0);
      //var instantiatedItem = Instantiate(i.visual, initialCharPos + playerDistance + itemPos, Quaternion.identity);
      //instantiatedItem.GetComponent<BaseItemScript>().itemSO = i;
      var itemImg = Instantiate(i.visual, initialCharPos + playerDistance + itemPos, Quaternion.identity);
      var itemScript = itemImg.AddComponent<BaseItemScript>();
      itemScript.itemSO = i;
      itemImg.transform.GetChild(0).gameObject.AddComponent<RotatePingPong>();

      foreach (var itBehaviour in i.itemBehaviourList)
      {
        if (itBehaviour == ItemBehaviour.UpSpawn)
        {
          itemImg.AddComponent<UpSpawnBehaviour>();
        }

        if (itBehaviour == ItemBehaviour.UpDown)
        {
          itemImg.AddComponent<UpDownBehaviour>();
        }
      }

      //print(i.name);
    }
  }
}
