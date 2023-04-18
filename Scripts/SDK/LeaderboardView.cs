using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private GameObject _leaderboardElementPrefab;

    private List<GameObject> _spawnedElements = new List<GameObject>();
    private int _currentPlace = 0;

    public void ConstructLeaderboard(List<PlayerInfoLeaderboard> playersInfo, int maxPlace)
    {
        ClearLeaderboard();

        foreach (PlayerInfoLeaderboard info in playersInfo)
        {
            if (_currentPlace == maxPlace)
                _currentPlace = 0;

            GameObject leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _parentObject);

            LeaderboardElement leaderboardElement = leaderboardElementInstance.GetComponent<LeaderboardElement>();
            leaderboardElement.Initialize(info.Name, info.Score, false, _currentPlace);

            _spawnedElements.Add(leaderboardElementInstance);
            _currentPlace++;
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
            Destroy(element);

        _spawnedElements = new List<GameObject>();
    }
}