using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UpdateClock : UnityEvent<int>
{
}

[Serializable]
public class ClockModeSwitched : UnityEvent<ClockMode>
{
}

public enum ArrowType
{
    Seconds,
    Minutes,
    Hours
}

public enum ClockMode
{
    Clock,
    Alarm
}

public class WatchManager : MonoBehaviour
{
    public static WatchManager Instance;

    [SerializeField] DigitalClock digitalClock;
    [SerializeField] MechanicalClock mechanicalClock;

    ClockMode activeMode;
    float timer = 0.0f;
    float adjustTimer = 0.0f;

    int totalSeconds;
    [HideInInspector] public UpdateClock updateClock = new UpdateClock();
    [HideInInspector] public ClockModeSwitched clockModeSwitched = new ClockModeSwitched();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        TimeManager.InitializeTime();
    }

    private void Start()
    {
        updateClock.AddListener(digitalClock.UpdateClock);
        updateClock.AddListener(mechanicalClock.UpdateClock);
        updateClock.AddListener(AlarmManager.Instance.CheckAlarm);

        clockModeSwitched.AddListener(digitalClock.SwitchMode);
        clockModeSwitched.AddListener(mechanicalClock.SwitchMode);

        clockModeSwitched.AddListener(AlarmManager.Instance.SwitchMode);

        activeMode = ClockMode.Clock;
    }

    public void InternetUpdateClocks(DateTime dateTime)
    {
        int hours = dateTime.Hour;
        int minutes = dateTime.Minute;
        int seconds = dateTime.Second;

        totalSeconds = TimeConverter.ConvertToTotalSeconds(hours,minutes,seconds);

        adjustTimer = 0.0f;
        timer = 0.0f;
    }

    public void SwitchMode()
    {
        if(activeMode == ClockMode.Clock)
        {
            activeMode = ClockMode.Alarm;
        }
        else
        {
            activeMode = ClockMode.Clock;
        }
        clockModeSwitched.Invoke(activeMode);
    }

    private void Update()
    {
        adjustTimer = Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            totalSeconds++;
            totalSeconds = totalSeconds >= 86400 ? 0 : totalSeconds;

            if(activeMode == ClockMode.Clock)
                updateClock.Invoke(totalSeconds);

            timer = 0.0f;
        }
        if (adjustTimer >= TimeConverter.SecondsInHour)
        {
            TimeManager.InitializeTime();
        }
    }

    public ClockMode GetActiveClockMode()
    {
        return activeMode;
    }
}
