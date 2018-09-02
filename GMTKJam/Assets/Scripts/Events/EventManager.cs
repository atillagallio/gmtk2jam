using System;

public class EventManager
{
    public delegate void StartGameEvent();
    public static StartGameEvent OnStartGameEvent;
}