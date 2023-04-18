using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsAnimation : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private Material _material;
    [SerializeField] private float _speedY;

    private float _currentX;
    private float _currentY;
    private string _materialName = "_MainTex";

    private void Start()
    {
        _currentX = GetComponent<Renderer>().material.mainTextureOffset.x;
        _currentY = GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    private void FixedUpdate()
    {
        _currentY += Time.deltaTime * _speedY;
        _currentX += Time.deltaTime * _speedX;
        _material.SetTextureOffset(_materialName, new Vector2(_currentX, _currentY));;
    }
}
