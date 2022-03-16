using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlarmNotificator : MonoBehaviour
{
    Image image;
    bool isFlicking;

    private void Awake()
    {
        image = GetComponentInParent<Image>();
    }

    private void Start()
    {
        AlarmManager.Instance.alarmIsOn.AddListener(TurnOnAlarmNotificator);
    }

    private void TurnOnAlarmNotificator(bool isOn)
    {
        isFlicking = isOn;
        if (isFlicking)
        {
            StartCoroutine(AlarmFlicking());
        }
    }

    IEnumerator AlarmFlicking()
    {
        Color initialColor = image.color;
        while (isFlicking)
        {
            image.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        image.color = initialColor;
    }
}
