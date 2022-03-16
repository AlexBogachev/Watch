using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AlarmIsOn : UnityEvent<bool>
{
}

public class AlarmManager : MonoBehaviour
{
    public static AlarmManager Instance;

    DigitalClock digitalClock;
    MechanicalClock mechanicalClock;

    float totalSeconds;

    int alarmHour;
    int alarmMinute;

    [HideInInspector] public AlarmIsOn alarmIsOn = new AlarmIsOn();

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

        digitalClock = FindObjectOfType<DigitalClock>();
        mechanicalClock = FindObjectOfType<MechanicalClock>();
    }

    public void SwitchMode(ClockMode mode)
    {
        if(mode == ClockMode.Alarm)
        {
            UpdateClocks();
        }
    }

    public void CheckAlarm(int totalSeconds)
    {
        if(totalSeconds % 60 == 0)
        {
            int hour = TimeConverter.ConvertTotalSecondsToHours(totalSeconds);
            int minute = TimeConverter.ConvertTotalSecondsToMinutes(totalSeconds);
            if(hour == alarmHour && minute == alarmMinute)
            {
                IsAlarmOn(true);
            }
        }
    }

    public void SetTotalSeconds()
    {
        totalSeconds = TimeConverter.ConvertToTotalSeconds(alarmHour, alarmMinute, 0);
    }

    public void AddSecondsToTotalSeconds(float addedSecond)
    {
        totalSeconds += addedSecond;

        if (totalSeconds >= TimeConverter.SecondsInDay)
        {
            totalSeconds = 0.0f;
        }
        else if (totalSeconds <= 0)
        {
            totalSeconds = TimeConverter.SecondsInDay;
        }
        alarmHour = TimeConverter.ConvertTotalSecondsToHours((int)totalSeconds);
        alarmMinute = TimeConverter.ConvertTotalSecondsToMinutes((int)totalSeconds);

        digitalClock.UpdateClock((int)totalSeconds);
    }

    public void UpdateClocks()
    {
        digitalClock.UpdateClock((int)totalSeconds);
        mechanicalClock.UpdateClock((int)totalSeconds);
    }

    public void SetAlarmTime(int hour, int minute)
    {
        alarmHour = hour;
        alarmMinute = minute;

        mechanicalClock.UpdateClock(TimeConverter.ConvertToTotalSeconds(hour, minute, 0));
    }

    public void IsAlarmOn(bool isOn)
    {
        alarmIsOn.Invoke(isOn);
    }

    public int GetAlarmHour()
    {
        return alarmHour;
    }

    public int GetAlarmMinute()
    {
        return alarmMinute;
    }
}
