using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public static class TimeManager
{
    private static DateTime utcStartTime = DateTime.UtcNow;
    public static DateTime UtcNow => utcStartTime.AddSeconds(Time.realtimeSinceStartup);

    public static void InitializeTime()
    {
        GetTimeFromNIST().WrapErrors();
        GetTimeFromMicrosoft();
    }

    private static async Task GetTimeFromNIST()
    {
        try
        {
            var client = new TcpClient();
            await client.ConnectAsync("time.nist.gov", 13);
            using (var streamReader = new StreamReader(client.GetStream()))
            {
                var response = await streamReader.ReadToEndAsync();
                string utcDateTimeString = response.Substring(7, 17);
                utcStartTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                WatchManager.Instance.InternetUpdateClocks(utcStartTime.ToLocalTime());
            }
        }
        catch
        {
            Debug.Log("ERROR ");
        }
    }

    private static void GetTimeFromMicrosoft()
    {
        try
        {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            var response = myHttpWebRequest.GetResponse();
            string utcDateTimeString = response.Headers["date"];
            utcStartTime = DateTime.ParseExact(utcDateTimeString, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture);
            WatchManager.Instance.InternetUpdateClocks(utcStartTime.ToLocalTime());
        }
        catch
        {
            Debug.Log("ERROR ");
        }
        
    }

    private static async void WrapErrors(this Task task)
    {
        await task;
    }
}