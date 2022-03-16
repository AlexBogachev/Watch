using UnityEngine;
using UnityEngine.UI;

public class TurnOffAlarmButton : MonoBehaviour
{
    Image image;
    Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(delegate { AlarmManager.Instance.IsAlarmOn(false); });
        AlarmManager.Instance.alarmIsOn.AddListener(SetActive);

        SetActive(false);
    }

    private void SetActive(bool isActive)
    {
        image.enabled = isActive;
        button.enabled = isActive;

        foreach (Transform t in transform)
            t.gameObject.SetActive(isActive);
    }
}
