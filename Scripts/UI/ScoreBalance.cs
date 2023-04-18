using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBalance : MonoBehaviour
{
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private GameObject _boostTime;

    private TranslatableText _text;
    private int _currentScore = 0;
    private bool _isBoost = false;
    public int Score => _currentScore;

    private void Start()
    {
        _score.TryGetComponent<TranslatableText>(out TranslatableText text);
        _text = text;
        _score.text = Translator.GetText(_text.TextID) + _currentScore.ToString();
    }

    private void OnEnable()
    {
        _wallet.ScoreAdd += ScoreAdd;
    }

    private void OnDisable()
    {
        _wallet.ScoreAdd -= ScoreAdd;
    }

    public void ScoreStartUpdate()
    {
        ScoreAdd(0);
    }

    public void BoostOn()
    {
        StartCoroutine(BoostOff());
    }

    private void ScoreAdd(int score)
    {
        if (_isBoost == true)
            score *= 2;

        _currentScore += score;
        _score.text = Translator.GetText(_text.TextID) + _currentScore.ToString();
    }

    private IEnumerator BoostOff()
    {
        _boostTime.TryGetComponent<TMP_Text>(out TMP_Text boost);
        _boostTime.SetActive(true);
        _isBoost = true;
        float time = 180;
        int currentTime;

        while (time > 0)
        {
            time -= Time.deltaTime;
            currentTime = (int)time;
            boost.text = currentTime.ToString();
            yield return null;
        }

        _boostTime.SetActive(false);
        _isBoost = false;
    }
}
