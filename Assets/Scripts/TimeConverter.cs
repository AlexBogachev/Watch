using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeConverter : MonoBehaviour
{
    public const int SecondsInMinute = 60;
    public const int SecondsInHour = 3600;
    public const int SecondsInDay = 86400;


    public static int ConvertTotalSecondsToHours (int totalSeconds)
    {
        int hours = Mathf.FloorToInt(totalSeconds /SecondsInHour);
        return hours;
    }

    public static int ConvertTotalSecondsToMinutes(int totalSeconds)
    {
        int minutes = Mathf.FloorToInt((totalSeconds % SecondsInHour)/SecondsInMinute);
        return minutes;
    }

    public static int ConvertTotalSecondsToSeconds(int totalSeconds)
    {
        int seconds = Mathf.FloorToInt(totalSeconds % SecondsInMinute);
        return seconds;
    }

    public static int ConvertToTotalSeconds(int hour, int minute, int second)
    {
        return SecondsInHour * hour + SecondsInMinute * minute + second;
    }


}
