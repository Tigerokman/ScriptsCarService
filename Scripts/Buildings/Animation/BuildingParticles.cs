using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public void ParticleOn()
    {
        _particle.Play();
    }
}
