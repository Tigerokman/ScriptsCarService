using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingTextFactory : BuildingText
{
    [SerializeField] private Factory _factory;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private BuildingUpgrade _upgrade;
    [SerializeField] private TMP_Text _priceUp;

    private void Start()
    {
        _factory.CreateObject.TryGetComponent<Detail>(out Detail detail);
        _nameText.TryGetComponent<TranslatableText>(out TranslatableText text);
        text.SetId(detail.NameID);
        PriceChanged();
    }

    private void OnEnable()
    {
        _upgrade.UpgradeUp += PriceChanged;
    }

    private void OnDisable()
    {
        _upgrade.UpgradeUp -= PriceChanged;
    }

    protected override IEnumerator FillProgress()
    {
        while (_upgrade.PlayerStay && _upgrade.CanBuy)
        {
            FillIn();
            yield return null;
        }

        ResetFill();
    }

    private void PriceChanged()
    {
        _priceUp.text = "$" + _upgrade.CurrentPrice.ToString();
    }
}
