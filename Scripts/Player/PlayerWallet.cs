using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    private AudioSource _audioTakeMoney;

    private int _money = 0;
    private float _trade = 0;
    private PlayerTextSound _textSound;
    private PlayerParticles _particles;

    public int Money => _money;

    public event UnityAction GoldChanged;
    public event UnityAction<int> ScoreAdd;

    private void Start()
    {
        _audioTakeMoney = GetComponent<AudioSource>();
        _particles = GetComponent<PlayerParticles>();
        _textSound = GetComponent<PlayerTextSound>();
    }

    public void UpdateBalance(int money)
    {
        if (money > 0)
        {
            ScoreAdd?.Invoke(money);
            _money += money + (int)(money * _trade);
            _audioTakeMoney.Play();
        }
        else
            _money += money;

        GoldChanged?.Invoke();
    }

    public void UpgradeTrade(float upgrade)
    {
        _trade += upgrade;
        _textSound.LevelUpText();
        _particles.UpgradeOn(Color.yellow);
    }
}
