using System;

public class EventManager
{
    public delegate void StartGameEvent();
    public static StartGameEvent OnStartGameEvent;

    public delegate void EndGameEvent();
    public static EndGameEvent OnEndGameEvent;

    public delegate void StartGameFadeIn();
    public static StartGameFadeIn OnStartPlayerRun;
}