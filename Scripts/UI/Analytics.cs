using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Analytics : MonoBehaviour
{

    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void AnalyticsEventCount(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName) == false)
        {
            int temp = 1;
            SetAnalyticsCount(temp, keyName);
        }
        else
        {
            int temp = PlayerPrefs.GetInt(keyName);
            temp++;
            SetAnalyticsCount(temp, keyName);
        }
    }

    private void SetAnalyticsCount(int count, string keyName)
    {
        PlayerPrefs.SetInt(keyName, count);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, keyName, count.ToString());
    }
}
