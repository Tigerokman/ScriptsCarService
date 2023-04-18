using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaleTrackerText : BuildingText
{
    [SerializeField] private SaleTrackerWallet _wallet;
    [SerializeField] private float _timeToFill;
    [SerializeField] private TMP_Text _priceTrack;

    private void Start()
    {
        PriceTrackChange();
    }

    private void OnEnable()
    {
        _wallet.WalletChanged += PriceTrackChange;
    }

    private void OnDisable()
    {
        _wallet.WalletChanged -= PriceTrackChange;
    }

    protected override IEnumerator FillProgress()
    {
        while (_wallet.PlayerStay)
        {
            FillIn(_timeToFill);
            yield return null;
        }

        ResetFill();
    }

    private void PriceTrackChange()
    {
        _priceTrack.text = "$" + _wallet.SummPrice.ToString();
    }
}
