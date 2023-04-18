using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingTextShop : BuildingText
{
    [SerializeField] private TMP_Text _textSell;
    [SerializeField] private Shop _bagShop;
    [SerializeField] private Image _cloud;
    [SerializeField] private Image _object;
    [SerializeField] private Sprite _cloudPicture;
    [SerializeField] private Sprite _objectPicture;


    private void Start()
    {
        _textSell.TryGetComponent<TranslatableText>(out TranslatableText text);
        text.SetId(_bagShop.SellDetail.NameID);
        _cloud.sprite = _cloudPicture;
        _object.sprite = _objectPicture;
    }

    protected override IEnumerator FillProgress()
    {
        while (_bagShop.IsTrade)
        {
            FillIn(_bagShop.FillBreake);
            yield return null;
        }

        ResetFill();
    }
}
