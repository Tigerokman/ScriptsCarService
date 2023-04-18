using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBag))]
public class PlayerTextSound : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _texts;
    [SerializeField] private float _delayBetweenText;
    [SerializeField] private SoloAudio _levelUp;
    [SerializeField] private AudioSource _startGame;
    [SerializeField] private SoloAudio _tired;
    [SerializeField] private SoloAudio _maxBag;

    private PlayerBag _playerBag;
    private AudioSource _lastComposition;
    private bool _isPlaying = false;
    private float _currentDelay;

    private void Awake()
    {
        _playerBag = GetComponent<PlayerBag>();
    }
    private void Start()
    {
        _currentDelay = _delayBetweenText;
    }

    private void Update()
    {
        if(_currentDelay > 0 && _isPlaying == false)
        {
            _currentDelay -= Time.deltaTime;
        }
        else if (_isPlaying == false)
        {
            TextOn();
        }
    }

    private void OnEnable()
    {
        _playerBag.DetailCountChange += HasStrenghtSound;
    }

    private void OnDisable()
    {
        _playerBag.DetailCountChange -= HasStrenghtSound;
    }

    public void LevelUpText()
    {
        if (_levelUp.CanSay == true && _isPlaying == false)
        {
            _levelUp.PlayAudio();
            StartCoroutine(IsPlayingText(_levelUp.AudioText.clip.length));
        }
    }

    public void StartText()
    {
        _startGame.Play();
        StartCoroutine(IsPlayingText(_startGame.clip.length));
    }

    private void HasStrenghtSound()
    {
        if (_playerBag.HasStrenght == false)
        {
            IsTiredText();
            Debug.Log("lol");
        }
        
        if (_playerBag.FreePlace == false)
        {
            Debug.Log("авы");
            MaxSizeText();
        }
    }

    private void IsTiredText()
    {
        if (_tired.CanSay == true && _isPlaying == false)
        {
            _tired.PlayAudio();
            StartCoroutine(IsPlayingText(_tired.AudioText.clip.length));
        }
    }

    private void MaxSizeText()
    {
        if (_maxBag.CanSay == true && _isPlaying == false)
        {
            _maxBag.PlayAudio();
            StartCoroutine(IsPlayingText(_maxBag.AudioText.clip.length));
        }
    }

    private void TextOn()
    {
        int random = Random.Range(0, _texts.Count);

        while (_lastComposition == _texts[random])
        {
            int newRandom = Random.Range(0, _texts.Count);
            random = newRandom;
        }

        _texts[random].Play();
        _currentDelay = _delayBetweenText;
        StartCoroutine(IsPlayingText(_texts[random].clip.length));
        _lastComposition = _texts[random];
    }

    private IEnumerator IsPlayingText(float lenght)
    {
        _isPlaying = true;

        while (lenght > 0)
        {
            lenght -= Time.deltaTime;
            yield return null;
        }

        _isPlaying = false;
    }

    private IEnumerator IsCanSayText(float delay)
    {

        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
    }
}
