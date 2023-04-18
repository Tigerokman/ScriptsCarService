using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFactory : BuildingUpgrade
{
    [SerializeField] private float _speedIncrease;
    [SerializeField] private int _factoryCountUp;
    [SerializeField] private Factory _factory;

    private float _currentSpeedIncrease = 0;
    private AudioSource _audioUpgrade;

    public float CurrentSpeedIncrease => _currentSpeedIncrease;

    private void Start()
    {
        _audioUpgrade = GetComponent<AudioSource>();
    }

    protected override void Upgrade()
    {
        _factory.UpgradeBag(_factoryCountUp);
        _currentSpeedIncrease += _speedIncrease;
        base.Upgrade();
        _audioUpgrade.Play();
        PriceIncreaseDuplicate();
    }
}
