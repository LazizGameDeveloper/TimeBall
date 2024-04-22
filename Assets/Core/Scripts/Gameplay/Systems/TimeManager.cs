using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnTimeStoppedEvent;
    public Action OnTimeSlowedEvent;
    public Action OnTimeUnslowedEvent;

    [SerializeField] private float _slowdownFactor = 0.05f;

    public void StopTime()
    {
        Time.timeScale = 0f;
        OnTimeStoppedEvent?.Invoke();
    }

    public void DoSlowmotion()
    {
        Time.timeScale = _slowdownFactor;
        OnTimeSlowedEvent?.Invoke();
    }

    public void UndoSlowmotion()
    {
        Time.timeScale = 1f;
        OnTimeUnslowedEvent?.Invoke();
    }
}

