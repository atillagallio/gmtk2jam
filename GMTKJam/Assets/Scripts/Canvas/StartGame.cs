using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

  // Use this for initialization
  public GameObject startGameObj;
  public Image whitescreen;

  private AudioSource gameStartSound;
  private bool startedGame = false;
  private bool fadeInAudio = false;
  private bool fadeOutAudio = false;

  Color c;
  void Start()
  {
    EventManager.OnStartGameEvent += () => { StartGameAnimation(); startedGame = true; };
    gameStartSound = GetComponent<AudioSource>();

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && !startedGame)
    {
      EventManager.OnStartGameEvent();
    }
    if (fadeInAudio)
      fadeInSound();
    if (fadeOutAudio)
      fadeOutSound();
  }

  void StartGameAnimation()
  {

    gameStartSound.Play();
    gameStartSound.volume = 0f;
    fadeInAudio = true;
    FindObjectOfType<MusicFader>().FadeToGame();
    c = whitescreen.color;
    c.a = 0;
    StartCoroutine(fadeInImage());

  }

  IEnumerator fadeInImage()
  {
    while (whitescreen.color.a < 1)
    {
      c.a += 0.3f * Time.deltaTime;
      whitescreen.color = c;
      yield return null;
    }
    foreach (Transform child in this.gameObject.transform)
    {
      GameObject.Destroy(child.gameObject);
    }
    fadeOutAudio = true;
    StartCoroutine(fadeOutImage());

    yield return null;
  }

  IEnumerator fadeOutImage()
  {
    while (whitescreen.color.a > 0)
    {
      c.a -= 0.2f * Time.deltaTime;
      whitescreen.color = c;
      yield return null;
    }
    yield return null;
  }
  void fadeInSound()
  {
    if (gameStartSound.volume < 1)
    {
      gameStartSound.volume += 0.2f * Time.deltaTime;
    }
    else
    {
      fadeInAudio = false;
    }
  }
  void fadeOutSound()
  {
    if (gameStartSound.volume > 0)
    {
      gameStartSound.volume -= 0.7f * Time.deltaTime;
    }
    else
    {
      fadeOutAudio = false;
    }
  }

}
