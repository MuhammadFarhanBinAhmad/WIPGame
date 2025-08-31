using UnityEngine;
using UnityEngine.Events;

public class TimerBehaviour : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] UnityEvent onTimerEnd = null;


    Timer timer;

    private void Start()
    {
        timer = new Timer(duration);

        timer.OnTimerEnd += HandleTimerEnd;
    }

    void HandleTimerEnd()
    {
        onTimerEnd.Invoke();
        print("end");
        Destroy(this);
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
    }
}
