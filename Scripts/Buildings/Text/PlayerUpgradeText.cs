using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BuildingUpgrade))]
public class PlayerUpgradeText : BuildingText
{
    [SerializeField] private TMP_Text _priceUp;
    [SerializeField] private TMP_Text _nameText;

    private BuildingUpgrade _upgrade;

    private void Start()
    {
        _upgrade = GetComponent<BuildingUpgrade>();
        _nameText.TryGetComponent<TranslatableText>(out TranslatableText text);
        text.SetId(NameID);
        _upgrade.UpgradeUp += PriceChanged;
        PriceChanged();
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
