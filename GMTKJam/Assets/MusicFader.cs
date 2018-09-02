using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFader : MonoBehaviour
{

    public float FadeOutTime;
    public float FadeInTime;
    public AnimationCurve FadeInCurve;
    public AnimationCurve FadeOutCurve;

    void Start()
    {
        EventManager.OnEndGameEvent += () => { FadeToMenu(); };
    }
    public void CrossFade(AudioLowPassFilter fadeIn, AudioLowPassFilter fadeOut)
    {
        StartCoroutine(FadeIn(fadeIn));
        StartCoroutine(FadeOut(fadeOut));
    }

    public IEnumerator FadeIn(AudioLowPassFilter filter)
    {
        for (float t = 0; t < FadeInTime; t += Time.deltaTime)
        {
            filter.cutoffFrequency = 22000 * FadeInCurve.Evaluate(t / FadeInTime);
            yield return null;
        }
        filter.cutoffFrequency = 22000;
    }

    public IEnumerator FadeOut(AudioLowPassFilter filter)
    {
        for (float t = 0; t < FadeInTime; t += Time.deltaTime)
        {
            filter.cutoffFrequency = 22000 * FadeOutCurve.Evaluate(1.0f - (t / FadeOutTime));
            yield return null;
        }
        filter.cutoffFrequency = 0;
    }

    public AudioLowPassFilter menuFilter;
    public AudioLowPassFilter gameFilter;

    public void FadeToGame()
    {
        CrossFade(gameFilter, menuFilter);
    }

    public void FadeToMenu()
    {
        CrossFade(menuFilter, gameFilter);

    }
}
