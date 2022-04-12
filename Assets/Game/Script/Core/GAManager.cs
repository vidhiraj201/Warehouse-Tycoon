using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;

public class GAManager : MonoBehaviour
{
    private void Awake()
    {
        FB.Init();
    }
    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public void RoomUnlocked(Transform t)
    {
        GameAnalytics.NewDesignEvent("Room Unlocked" + t.name);
        print("SECTION DATA SENT TO _GAME ANALYTICS_");
    }
    public void CompleteLevel(int l)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, l.ToString("D4"));
        print("LEVEL COMPLETE DATA SENT TO _GAME ANALYTICS_");
    }
}
