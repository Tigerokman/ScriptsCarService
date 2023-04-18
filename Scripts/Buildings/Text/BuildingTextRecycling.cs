using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingTextRecycling : BuildingText
{
    [SerializeField] private RecyclingPut _put;
    [SerializeField] private RecyclingTake _take;
    [SerializeField] private TMP_Text _nameTake;
    [SerializeField] private TMP_Text _namePut;
    [SerializeField] private TMP_Text _CountTake;
    [SerializeField] private TMP_Text _CountPut;
    [SerializeField] private TMP_Text _priceUp;
    [SerializeField] private BuildingUpgrade _upgrade;

    private void Start()
    {
        _nameTake.TryGetComponent<TranslatableText>(out TranslatableText text);
        text.SetId(_take.TakeDetail.NameID);
        _put.CreateObject.TryGetComponent<Detail>(out Detail detail);
        _namePut.TryGetComponent<TranslatableText>(out TranslatableText text2);
        text2.SetId(detail.NameID);
        TextCountChange();
        PriceChanged();
    }

    private void OnEnable()
    {
        _put.BagChanged += TextCountChange;
        _take.BagChanged += TextCountChange;
        _put.BagUp += TextCountChange;
        _take.BagUp += TextCountChange;
        _upgrade.UpgradeUp += PriceChanged;
    }

    private void OnDisable()
    {
        _put.BagChanged -= TextCountChange;
        _take.BagChanged -= TextCountChange;
        _put.BagUp -= TextCountChange;
        _take.BagUp -= TextCountChange;
        _upgrade.UpgradeUp -= PriceChanged;
    }

    protected override IEnumerator FillProgress()
    {
       while(_upgrade.PlayerStay && _upgrade.CanBuy)
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

    private void TextCountChange()
    {
        _CountTake.text = _take.CountDetail + "/" + _take.MaxBagSize;
        _CountPut.text = _put.CountDetail + "/" + _put.MaxBagSize;
    }
}
