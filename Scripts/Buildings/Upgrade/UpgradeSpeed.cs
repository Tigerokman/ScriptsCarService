using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpeed : PlayerUpgrade
{
    [SerializeField] private float _increaseStat;

    private PlayerMovement _playerMovement;
    private AudioSource _audioUpgrade;

    private void Start()
    {
        _audioUpgrade = GetComponent<AudioSource>();
        Player.TryGetComponent<PlayerMovement>(out PlayerMovement movement);
        _playerMovement = movement;
    }

    protected override void Upgrade()
    {
        _audioUpgrade.Play();
        _playerMovement.SpeedUpgrade(_increaseStat);
        base.Upgrade();
    }
}
