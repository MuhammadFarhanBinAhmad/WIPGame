
using System;

public class Timer 
{
    public float RemainingSeconds { get; private set; }

    public Timer(float duration)
    {
        RemainingSeconds = duration;
    }

    public event Action OnTimerEnd;

    public void Tick(float deltaTime)
    {
        if (RemainingSeconds <= 0) { return; }
         
        RemainingSeconds -= deltaTime;

        CheckForTimerEnd();
    }

    void CheckForTimerEnd()
    {
        if(RemainingSeconds >0) { return;}

        RemainingSeconds = 0;

        OnTimerEnd?.Invoke();
    }
}
