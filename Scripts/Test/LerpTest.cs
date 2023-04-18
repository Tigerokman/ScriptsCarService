using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    [SerializeField] private Transform _a;
    [SerializeField] private Transform _b;
    [SerializeField] private Transform _target;
    [SerializeField] private float _pathTime;
    [SerializeField] private float _duration;

    private void FixedUpdate()
    {
        _duration += Time.deltaTime;
        _target.position = Vector3.Lerp(_a.position, _b.position, _duration / _pathTime);
    }
}
