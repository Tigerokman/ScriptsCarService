using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingTextTrash : BuildingText
{
    [SerializeField] private Trash _bagTrash;
    [SerializeField] private Image _cloud;
    [SerializeField] private Image _object;
    [SerializeField] private Sprite _cloudPicture;
    [SerializeField] private Sprite _objectPicture;


    private void Start()
    {
        _cloud.sprite = _cloudPicture;
        _object.sprite = _objectPicture;
    }

    protected override IEnumerator FillProgress()
    {
        while (_bagTrash.IsTrade)
        {
            FillIn(_bagTrash.FillBreake);
            yield return null;
        }

        ResetFill();
    }
}
