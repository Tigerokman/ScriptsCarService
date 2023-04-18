using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStrengh : PlayerUpgrade
{
    [SerializeField] private int _increaseStat;

    private PlayerBag _playerBag;
    private AudioSource _audioUpgrade;

    private void Start()
    {
        _audioUpgrade = GetComponent<AudioSource>();
        Player.TryGetComponent<PlayerBag>(out PlayerBag bag);
        _playerBag = bag;
    }

    protected override void Upgrade()
    {
        _audioUpgrade.Play();
        _playerBag.StrenghtUp(_increaseStat);
        base.Upgrade();
    }
}
