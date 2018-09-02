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

    private Vector3 initialCharPos;
    void Awake()
    {

        var allItems = Resources.LoadAll<Item>("Items");
        RepeatableItems = allItems.Where((i) => (i.itemType == ItemType.Addictive || i.itemType == ItemType.Repeatable)).ToList();
        UniqueItems = allItems.Where((i) => (i.itemType == ItemType.Unique)).ToList();
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

    bool ChooseSpawnItem(out Item item)
    {
        var isSpawningUnique = PossibleUniqueItems.Count() > 0;
        item = (isSpawningUnique ? PossibleUniqueItems : PossibleRepeatableItems).FirstOrDefault();
        if (item != null && isSpawningUnique)
        {
            UniqueItems.Remove(item);
        }
        RepeatableItems = RepeatableItems.Shuffle().ToList();
        if (item.itemType == ItemType.Addictive)
        {
            RepeatableItems.Add(item);
        }
        return item != null;
    }


    [Header("Spawn Info")]
    public float spawnCooldown;
    public float SpawnInterval;
    public float spawnDistance;
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
            var instantiatedItem = Instantiate(baseItemPrefab, initialCharPos + playerDistance + itemPos, Quaternion.identity);
            instantiatedItem.GetComponent<BaseItemScript>().itemSO = i;
            var itemImg = Instantiate(i.visual, instantiatedItem.transform.position, Quaternion.identity);
            itemImg.transform.SetParent(instantiatedItem.transform);

            foreach (var itBehaviour in i.itemBehaviourList)
            {
                if (itBehaviour == ItemBehaviour.UpSpawn)
                {
                    instantiatedItem.AddComponent<UpSpawnBehaviour>();
                }

                if (itBehaviour == ItemBehaviour.UpDown)
                {
                    instantiatedItem.AddComponent<UpDownBehaviour>();
                }
            }

            print(i.name);

            evoManager.VerifyEvolution();
        }
    }
}
