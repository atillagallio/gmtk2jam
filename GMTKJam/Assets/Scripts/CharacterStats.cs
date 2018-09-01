using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float ageTimer;
    // Use this for initialization
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

    private float currTimer = 0;
    public void Update()
    {
        currTimer += Time.deltaTime;
        if (currTimer >= ageTimer)
        {
            Age++;
            currTimer = 0;
        }
    }
}
