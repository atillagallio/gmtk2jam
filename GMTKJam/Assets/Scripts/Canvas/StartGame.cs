using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    // Use this for initialization
    public GameObject startGameObj;
    public RawImage whitescreen;

    void Start()
    {
        EventManager.OnStartGameEvent += () => { Destroy(startGameObj); };

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.OnStartGameEvent();
        }
    }

}
