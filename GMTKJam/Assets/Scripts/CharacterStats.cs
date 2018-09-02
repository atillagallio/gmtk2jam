using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float ageTimer;
    // Use this for initialization
    public Dictionary<Item, int> ItemCount = new Dictionary<Item, int>();
    public float Age;

    private bool gameStarted;
    void Start()
    {
        EventManager.OnStartGameEvent += () => { gameStarted = true; };
        EventManager.OnStartPlayerRun += () => { Age = 0; };
        EventManager.OnEndGameEvent += () =>
        {
            gameStarted = false; currTimer = 0;
            ItemCount.Clear();

        };
    }
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
        if (gameStarted)
            UpdateGameTime();
    }

    void UpdateGameTime()
    {
        currTimer += Time.deltaTime;
        if (currTimer >= ageTimer)
        {
            Age++;
            currTimer = 0;
        }
    }
}
