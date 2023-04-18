using System.Collections;
using Agava.YandexGames;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeToEndMin;
    [SerializeField] private TMP_Text _textTime;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private ScoreBalance _scoreBalance;
    [SerializeField] private YandexLeaderboard _leaderboard;
    [SerializeField] private Analytics _analytics;
    
    private AudioSource _endGameSound;
    private Animator _animator;
    private bool _colorIsEnd = false;
    private bool _animOn = false;
    private float _timeToEndUpdate;
    private int _currentTime;
    private int _secInMin = 60;
    private string _currentMin;
    private string _currentSec;
    private int _cesWithout0 = 10;

    private void Start()
    {
        _endGameSound = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _timeToEndUpdate = _timeToEndMin * _secInMin + 1;
        _currentTime = (int)_timeToEndMin * _secInMin;
        _currentMin = (_currentTime / _secInMin).ToString();
        _textTime.text = _currentMin;
    }

    void Update()
    {
        if (_timeToEndUpdate >= 0)
        {
            _timeToEndUpdate -= Time.deltaTime;
            _currentTime = (int)_timeToEndUpdate;

            if (_currentTime / _secInMin < _cesWithout0)
                _currentMin = "0" + (_currentTime / _secInMin).ToString();
            else
                _currentMin = (_currentTime / _secInMin).ToString();


            if (_currentTime % _secInMin < _cesWithout0)
                _currentSec = "0" + (_currentTime % _secInMin).ToString();
            else
                _currentSec = (_currentTime % _secInMin).ToString();

            _textTime.text = _currentMin + ":" + _currentSec;

            if(_timeToEndUpdate <= _secInMin && _animOn == false)
            {
                if(_colorIsEnd == false)
                {
                    string colour = "Colour";
                    _animator.SetTrigger(colour);
                    Debug.Log("Смена цвета");
                    _colorIsEnd = true;
                }

                if(_timeToEndUpdate <= 15)
                {
                    _animOn = true;
                    string end = "IsEnd";
                    _animator.SetBool(end, true);
                }

            }

            if (_timeToEndUpdate <= 0)
            {
                _textTime.text = "00:00";
                EndGame();
            }
        }
    }

    public void PutEndGame()
    {
        _timeToEndUpdate = 0;
    }

    public void SaveResult()
    {
        if (PlayerAccount.IsAuthorized == true)
            StartCoroutine(StartLeaderboard());
        else
            _leaderboard.UpdateLeaderBoardOn(this);

    }

    private void EndGame()
    {
        string eventName = "GameEnd";
        _endPanel.SetActive(true);
        _endGameSound.Play();
        _leaderboard.FormListOfTopPlayers(_scoreBalance.Score);
        SaveResult();
        _analytics.AnalyticsEventCount(eventName);

        _endPanel.TryGetComponent<EndGame>(out EndGame endGame);
        endGame.ShowPlayerPoint(_scoreBalance.Score);
    }

    private IEnumerator StartLeaderboard()
    {
        float delay = 0.5f;

        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        _leaderboard.FormListOfTopPlayers();
    }
}
