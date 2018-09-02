using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    public GameObject character;
    public GameObject deathObj;
    public GameObject becomeStarParticle;

    public GameObject Camera;

    // Use this for initialization
    void Start()
    {
        EventManager.OnStartPlayerRun += () => { RestartGame(); };
        EventManager.OnEndGameEvent += () => { GameDeath(); };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RestartGame()
    {
        deathObj.SetActive(false);
        Camera.GetComponent<Follow>().enabled = true;
        GetComponent<Follow>().enabled = true;
    }
    void GameDeath()
    {
        Instantiate(becomeStarParticle, character.transform.position, Quaternion.identity);
        Camera.GetComponent<Follow>().enabled = false;
        GetComponent<Follow>().enabled = false;
        deathObj.SetActive(true);
        foreach (var material in character.GetComponentInChildren<SkinnedMeshRenderer>().materials)
        {
            material.color = Color.white;
        }
        //character.SetActive(false);


    }
}
