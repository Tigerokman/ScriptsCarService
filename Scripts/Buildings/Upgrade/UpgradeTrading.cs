using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTrading : PlayerUpgrade
{
    [SerializeField] private float _increaseStat;

    private PlayerWallet _playerWallet;
    private AudioSource _audioUpgrade;

    private void Start()
    {
        _audioUpgrade = GetComponent<AudioSource>();
        Player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet);
        _playerWallet = wallet;
    }

    protected override void Upgrade()
    {
        _audioUpgrade.Play();
        _playerWallet.UpgradeTrade(_increaseStat);
        base.Upgrade();
    }
}
