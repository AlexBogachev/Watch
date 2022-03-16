using UnityEngine;

public class MechanicalClock : MonoBehaviour
{
    [SerializeField] Arrow hourArrow;
    [SerializeField] Arrow minuteArrow;
    [SerializeField] Arrow secondsArrow;

    public void UpdateClock(int totalSeconds)
    {
        if (totalSeconds >= TimeConverter.SecondsInDay / 2)
            totalSeconds -= TimeConverter.SecondsInDay / 2;

        int hour = totalSeconds;
        int minute = totalSeconds% TimeConverter.SecondsInHour;
        int seconds = totalSeconds% TimeConverter.SecondsInMinute;

        hourArrow.UpdatePosition(hour);
        minuteArrow.UpdatePosition(minute);
        secondsArrow.UpdatePosition(seconds);
    }

    public void SwitchMode(ClockMode mode)
    {
        if (mode == ClockMode.Alarm)
        {
            secondsArrow.gameObject.SetActive(false);
        }
        else
        {
            secondsArrow.gameObject.SetActive(true);
        }
    }
}
