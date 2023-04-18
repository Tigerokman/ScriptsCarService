using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _upgradeParticle;

    public void UpgradeOn(Color color)
    {
        _upgradeParticle.startColor = color;
        _upgradeParticle.Play();
    }
}
