using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowTouchIndicator : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Arrow arrow;
    Vector2 arrowRotationCenter;

    Vector2 prevTouch;

    private void Awake()
    {
        arrow = GetComponentInParent<Arrow>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(WatchManager.Instance.GetActiveClockMode() == ClockMode.Alarm)
        {
            Vector3 prevDirection = prevTouch - arrowRotationCenter;
            Vector2 direction = eventData.position - arrowRotationCenter;

            float angle = Vector2.SignedAngle(prevDirection, direction);
            prevTouch = eventData.position;

            arrow.RotateArrow(angle);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (WatchManager.Instance.GetActiveClockMode() == ClockMode.Alarm)
        {
            arrowRotationCenter = (Vector2)arrow.transform.position;
            prevTouch = eventData.position;
            AlarmManager.Instance.SetTotalSeconds();
        }
    }
}
