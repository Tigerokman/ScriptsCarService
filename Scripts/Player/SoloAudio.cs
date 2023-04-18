using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloAudio : MonoBehaviour
{
    [SerializeField] private float _delayBetweenText;

    private bool _canSay = true;
    private AudioSource _audioText;

    public bool CanSay => _canSay;
    public AudioSource AudioText => _audioText;

    private void Start()
    {
        _audioText = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        _audioText.Play();
        StartCoroutine(IsCanSayText());
    }

    private IEnumerator IsCanSayText()
    {
        _canSay = false;
        float delay = _delayBetweenText;

        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        _canSay = true;
    }
}
