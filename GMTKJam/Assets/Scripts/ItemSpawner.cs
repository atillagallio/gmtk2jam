using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

  [Serializable]
  public class CharacterInfo
  {
    public Dictionary<Item, int> ItemCount = new Dictionary<Item, int>();
    public float Age;
    public AdultInfluence GetHighestAdultInfluence()
    {
      return AdultInfluence.Normal;
    }

    public OldInfluence GetHighestOldInfluence()
    {
      return OldInfluence.Normal;
    }
  }

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
  List<Item> RepeatableItems;
  IEnumerable<Item> PossibleRepeatableItems
  {
    get
    {
      return RepeatableItems.Where((i) => CheckItemPossibility(i, charInfo));
    }
  }

  void Awake()
  {
    var allItems = Resources.LoadAll<Item>("Items");
    RepeatableItems = allItems.Where((i) => (i.itemType == ItemType.Addictive || i.itemType == ItemType.Repeatable)).ToList();
    UniqueItems = allItems.Where((i) => (i.itemType == ItemType.Unique)).ToList();
  }


  static void AddItem(Item i, CharacterInfo c)
  {
    if (c.ItemCount.ContainsKey(i))
    {
      c.ItemCount[i]++;
    }
    else
    {
      c.ItemCount.Add(i, 0);
    }
  }

  static bool CheckItemPossibility(Item i, CharacterInfo c)
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
  CharacterInfo charInfo;

  bool ChooseSpawnItem(out Item item)
  {
    var isSpawningUnique = PossibleUniqueItems.Count() > 0;
    item = (isSpawningUnique ? PossibleUniqueItems : PossibleRepeatableItems).FirstOrDefault();
    if (item != null && isSpawningUnique)
    {
      UniqueItems.Remove(item);
    }
    if (item.itemType == ItemType.Addictive)
    {
      RepeatableItems.Add(item);
    }
    return item != null;
  }


  public float spawnCooldown;
  public float SpawnInterval;
  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    spawnCooldown -= Time.deltaTime;
    if (spawnCooldown < 0)
    {
      spawnCooldown = SpawnInterval;
      Item i;
      if (ChooseSpawnItem(out i))
      {
        print(i.name);
      }
    }
  }
}
