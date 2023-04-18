using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRecycling : BuildingUpgrade
{
    [SerializeField] private Bag _take;
    [SerializeField] private Bag _put;
    [SerializeField] private int _takeCountUp;
    [SerializeField] private int _putCountUp;

    private AudioSource _audioUpgrade;

    private void Start()
    {
        _audioUpgrade = GetComponent<AudioSource>();
    }

    protected override void Upgrade()
    {
        _take.UpgradeBag(_takeCountUp);
        _put.UpgradeBag(_putCountUp);
        base.Upgrade();
        _audioUpgrade.Play();
    }
}
