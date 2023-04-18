using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class BuildingText : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private int _nameID;

    public int NameID => _nameID;
    private float _currentTimeFill = 0;
    private bool _isFilling = false;

    public bool IsFilling => _isFilling;
    public event UnityAction FillEnd;

    private void Start()
    {
        _fill.fillAmount = 0;
    }

    public void StartFill()
    {
        _isFilling = true;
        StartCoroutine(FillProgress());
    }

    protected void FillIn(float timeToFill = 2)
    {
        _currentTimeFill += Time.deltaTime;
        _fill.fillAmount = _currentTimeFill / timeToFill;
        Mathf.Clamp(_fill.fillAmount, 0, 1);

        if (_currentTimeFill >= timeToFill)
        {
            FillEnd?.Invoke();
            _currentTimeFill = 0;
        }
    }

    protected void ResetFill()
    {
        _fill.fillAmount = 0;
        _currentTimeFill = 0;
        _isFilling = false;
    }

    protected abstract IEnumerator FillProgress();
}
