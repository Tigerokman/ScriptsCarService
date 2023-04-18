using GameAnalyticsSDK;
using UnityEngine;

public class AnalyticsTest : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "666");
    }
}