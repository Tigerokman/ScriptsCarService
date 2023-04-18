using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNick;
    [SerializeField] private TMP_Text _playerResult;
    [SerializeField] private Image _playerIcon;
    [SerializeField] private Color _playerColor;
    [SerializeField] private List<Sprite> _icons;

    public void Initialize(string nick, int playerResult, bool isPlayer, int place)
    {
        _playerNick.text = nick;
        _playerResult.text = playerResult.ToString();
        _playerIcon.sprite = _icons[place];

        if (isPlayer)
        {
            _playerNick.color = _playerColor;
            _playerResult.color = _playerColor;
        }
    }
}