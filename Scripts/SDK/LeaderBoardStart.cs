using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(YandexLeaderboard))]
public class LeaderBoardStart : MonoBehaviour
{
    private YandexLeaderboard _leaderboard;

    private void Start()
    {
        //_leaderboard = GetComponent<YandexLeaderboard>();
        //_leaderboard.FormListOfTopPlayers(true);
    }
}
