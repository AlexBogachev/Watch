using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] ArrowType type;
    RectTransform rectTransform;

    float prevAngle;

    float dragAngle;

    float oneSecondStep = 360.0f/TimeConverter.SecondsInMinute;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        switch (type)
        {
            case ArrowType.Hours:
                oneSecondStep = 360.0f / (TimeConverter.SecondsInDay / 2);
                break;
            case ArrowType.Minutes:
                oneSecondStep = 360.0f / TimeConverter.SecondsInHour;
                break;
            case ArrowType.Seconds:
                oneSecondStep = 360.0f / TimeConverter.SecondsInMinute;
                break;
        }
    }

    public void UpdatePosition(int time)
    {
        rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, - time * oneSecondStep);
    }

    public void RotateArrow(float angle)
    {
        dragAngle += angle;
        rectTransform.Rotate(0.0f, 0.0f, angle, Space.Self);
        float secondsFromAngle = angle / oneSecondStep;
        AlarmManager.Instance.AddSecondsToTotalSeconds(-secondsFromAngle);
    }
}
