using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressViewImage : MonoBehaviour
{
    [SerializeField] private TMP_Text _countProgress;
    [SerializeField] private GameObject _imageComplete;
    [SerializeField] private int _nameId;

    private int _countToFinish;
    private int _currentCount = 0;

    public int NameID => _nameId;

    public void Initialiaze(int countToFinish)
    {
        _countToFinish = countToFinish;
        TextUpdate();
    }

    public void ProgressUpdate()
    {
        _currentCount++;
        TextUpdate();

        if( _currentCount == _countToFinish )
            _imageComplete.SetActive(true);
    }

    public void ResetProgress()
    {
        _currentCount = 0;
        _imageComplete.SetActive(false);
        TextUpdate();
    }

    private void TextUpdate()
    {
        _countProgress.text = _currentCount + "/" + _countToFinish;
    }
}
