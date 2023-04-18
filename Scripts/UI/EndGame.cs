using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerScore;
    [SerializeField] private GameObject _buttonExit;
    [SerializeField] private GameObject _joystick;

    private void Start()
    {
        _joystick.SetActive(false);
    }

    public void ShowPlayerPoint(int score)
    {
        //_playerScore.text = _playerScore.text + score.ToString();
        StartCoroutine(AnimationScore(score));
    }

    public void StopGame()
    {
        _buttonExit.SetActive(true);
        //Time.timeScale = 0;
    }

    private IEnumerator AnimationScore(int score)
    {
        int temp;
        int currentAnimValue;
        int maxAnimValue = 15;
        float animDelay = 0.05f;
        string scoreString = "";

        _playerScore.TryGetComponent<TranslatableText>(out TranslatableText text);
        string tempScoreString = Translator.GetText(text.TextID);

        for (int i = 0; i < score.ToString().Length; i++)
        {
            currentAnimValue = 0;

            while (currentAnimValue != maxAnimValue)
            {
                yield return new WaitForSeconds(animDelay);
                temp = Random.Range(0,10);
                _playerScore.text = tempScoreString + scoreString + temp.ToString();

                currentAnimValue++;
            }

            scoreString = scoreString + score.ToString()[i];
        }

        _playerScore.text = tempScoreString + scoreString;
        yield return null;
    }
}
