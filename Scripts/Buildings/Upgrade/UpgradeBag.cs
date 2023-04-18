using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBag : PlayerUpgrade
{
    [SerializeField] private int _increaseStat;

    private Bag _playerBag;
    private AudioSource _audioUpgrade;

    private void Start()
    {
        _audioUpgrade = GetComponent<AudioSource>();
        Player.TryGetComponent<Bag>(out Bag bag);
        _playerBag = bag;
    }

    protected override void Upgrade()
    {
        _audioUpgrade.Play();
        _playerBag.UpgradeBag(_increaseStat);
        base.Upgrade();
    }
}
