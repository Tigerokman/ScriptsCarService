using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private TMP_Text _money;

    private void OnEnable()
    {
        _wallet.GoldChanged += MoneyViewChange;
        MoneyViewChange();
    }

    private void OnDisable()
    {
        _wallet.GoldChanged -= MoneyViewChange;
    }

    private void MoneyViewChange()
    {
        _money.text = "$" + _wallet.Money.ToString();
    }
}
