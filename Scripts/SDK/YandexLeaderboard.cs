using Agava.YandexGames;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class YandexLeaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private GameObject _buttonEnter;

    private const string _leaderboardName = "BestOfTheBest";
    private int _maxPlace = 5;
    private int _currentPlayerScore = 0;
    private float _currentDelay = 10;
    private float _delayUpdate = 3;

    public void Construct(LeaderboardView leaderboard)
    {
        _leaderboardView = leaderboard;
    }

    public void FormListOfTopPlayers(int score = 0)
    {
        List<PlayerInfoLeaderboard> top5Players = new List<PlayerInfoLeaderboard>();

#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
        };
#endif

        if (score > _currentPlayerScore)
        {
            _currentPlayerScore = score;
        }

        if (PlayerAccount.IsAuthorized == true && _buttonEnter.activeSelf == true)
        {
            _buttonEnter.SetActive(false);

            if (_currentPlayerScore > 0)
                AddPlayerToLeaderboard(_currentPlayerScore);
        }

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            int resultsAmount = result.entries.Length;

            resultsAmount = Mathf.Clamp(resultsAmount, 1, 5);

            for (int i = 0; i < resultsAmount; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymos";

                int score = result.entries[i].score;

                top5Players.Add(new PlayerInfoLeaderboard(name, score));
            }

            if(resultsAmount < _maxPlace)
            {
                int leftPlace = _maxPlace - resultsAmount;

                for (int i = 0; i < leftPlace; i++)
                {
                    top5Players.Add(new PlayerInfoLeaderboard("Anonymos", leftPlace - i - 666));
                }
            }

            _leaderboardView.ConstructLeaderboard(top5Players, _maxPlace);
        });
    }

    public void OnGetLeaderboardPlayerEntryButtonClick()
    {
        Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
        {
            if (result == null)
                Debug.Log("Player is not present in the leaderboard.");
            else
                Debug.Log($"My rank = {result.rank}, score = {result.score}");
        });
    }

    public void AddPlayerToLeaderboard(int score)
    {
        Leaderboard.GetPlayerEntry(_leaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Leaderboard.SetScore(_leaderboardName, score);
        });

        _currentPlayerScore = 0;
    }

    public void UpdateLeaderBoardOn(Timer timer = null)
    {
        StartCoroutine(UpdateLeaderBoard(timer));
    }

    private IEnumerator UpdateLeaderBoard(Timer timer)
    {
        while (PlayerAccount.IsAuthorized == false)
        {

            while (_currentDelay > 0)
            {
                _currentDelay -= Time.unscaledDeltaTime;
                yield return null;
            }

            Debug.Log("Try");
            FormListOfTopPlayers();
            _currentDelay = _delayUpdate;
        }

        if(timer != null)
        {
            timer.SaveResult();
        }
    }

}

public class PlayerInfoLeaderboard
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerInfoLeaderboard(string name, int score)
    {
        Name = name;
        Score = score;
    }
}