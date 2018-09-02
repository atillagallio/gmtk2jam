using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    // Use this for initialization
    public GameObject startGameObj;
    public Image whitescreen;

    public List<Image> Logo;

    private AudioSource gameStartSound;
    private bool startedGame = false;
    private bool fadeInAudio = false;
    private bool fadeOutAudio = false;

    Color c;
    void Start()
    {
        EventManager.OnStartGameEvent += () => { StartGameAnimation(); startedGame = true; };
        EventManager.OnEndGameEvent += () => { startedGame = false; ReapearLogo(); };
        gameStartSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void ReapearLogo()
    {
        foreach (var img in Logo)
        {
            img.gameObject.SetActive(true);
            if (img.gameObject.transform.parent.parent != null)
                img.gameObject.transform.parent.parent.gameObject.SetActive(true);
            StartCoroutine(fadeInImage(img, false));
        }

    }
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
        StartCoroutine(fadeInImage(whitescreen, true));

    }

    IEnumerator fadeInImage(Image img, bool startGame)
    {
        while (img.color.a < 1)
        {
            c.a += 0.3f * Time.deltaTime;
            img.color = c;
            yield return null;
        }
        if (startGame == true)
        {
            foreach (Transform child in this.gameObject.transform)
            {
                if (child.GetComponent<Image>() != null)
                    child.GetComponent<Image>().color = Color.clear;
                child.gameObject.SetActive(false);
                //GameObject.Destroy(child.gameObject);
            }
            fadeOutAudio = true;
            StartCoroutine(fadeOutImage());
            EventManager.OnStartPlayerRun();
        }


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
