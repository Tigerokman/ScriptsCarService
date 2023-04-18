using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class YandexSDK : MonoBehaviour
{
    [SerializeField] private YandexLeaderboard _leaderboard;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        InterstitialAd.Show();

        if (PlayerAccount.IsAuthorized == true)
            _leaderboard.FormListOfTopPlayers();
        else
            _leaderboard.UpdateLeaderBoardOn();
    }

    public void ShowVideoAD()
    {
        Time.timeScale = 0;
        Debug.Log("Показал");
        VideoAd.Show();
    }

    public void AuthorizePlayer()
    {
        PlayerAccount.Authorize();
    }
}
