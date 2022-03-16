using UnityEngine;
using UnityEngine.UI;

public class AlarmModeButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(WatchManager.Instance.SwitchMode);
    }
}
