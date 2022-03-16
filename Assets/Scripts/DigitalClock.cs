using UnityEngine;
using UnityEngine.UI;

public class DigitalClock : MonoBehaviour
{
    [SerializeField] InputField hoursField;
    [SerializeField] InputField minutesField;
    [SerializeField] InputField secondsField;

    public void UpdateClock(int totalSeconds)
    {
        int hour = TimeConverter.ConvertTotalSecondsToHours(totalSeconds);
        int minute = TimeConverter.ConvertTotalSecondsToMinutes(totalSeconds);
        int seconds = TimeConverter.ConvertTotalSecondsToSeconds(totalSeconds);

        hoursField.text = hour.ToString();
        minutesField.text = minute.ToString();
        secondsField.text = seconds.ToString();
    }

    public void SwitchMode(ClockMode mode)
    {
        if(mode == ClockMode.Alarm)
        {
            hoursField.onValueChanged.AddListener(SetAlarm);
            minutesField.onValueChanged.AddListener(SetAlarm);

            hoursField.interactable = true;
            minutesField.interactable = true;

            secondsField.gameObject.SetActive(false);
        }
        else
        {
            hoursField.onValueChanged.RemoveListener(SetAlarm);
            minutesField.onValueChanged.RemoveListener(SetAlarm);

            hoursField.interactable = false;
            minutesField.interactable = false;

            secondsField.gameObject.SetActive(true);
        }
    }

    private void SetAlarm(string stub)
    {
        int hour;
        int minute;

        int.TryParse(hoursField.text, out hour);
        int.TryParse(minutesField.text, out minute);

        AlarmManager.Instance.SetAlarmTime(hour, minute);
    }
}
