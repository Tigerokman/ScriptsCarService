using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class SaleTrackerWallet : MonoBehaviour
{
    [SerializeField] private BuildingText _buildingText;
    [SerializeField] private SaleTracker _saleTracker;
    [SerializeField] private GameObject _fill;
    [SerializeField] private ParticleSystem _particleSell;

    private BoxCollider _boxCollider;
    private bool _isFillOn = false;
    private int _summPrice = 0;
    private PlayerWallet _wallet;
    private bool _playerStay = false;

    public bool PlayerStay => _playerStay;
    public int SummPrice => _summPrice;
    public event UnityAction WalletChanged;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _buildingText.FillEnd += Upgrade;
        _saleTracker.SaleTrackerOn += SaleTrackerChange;
        _saleTracker.DetailGet += GetDetail;
    }

    private void OnDisable()
    {
        _buildingText.FillEnd -= Upgrade;
        _saleTracker.SaleTrackerOn -= SaleTrackerChange;
        _saleTracker.DetailGet -= GetDetail;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            PlayerStayChange();

            _wallet = wallet;
            _buildingText.StartFill();

        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            if (_buildingText.IsFilling == false)
            {
                _buildingText.StartFill();
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            PlayerStayChange();
        }
    }

    private void GetDetail(Detail detail)
    {
        UpdateBalance(detail.Price);
    }

    private void UpdateBalance(int money)
    {
        _summPrice += money;
        WalletChanged?.Invoke();
    }

    private void PlayerStayChange()
    {
        _playerStay = !_playerStay;
    }

    private void SaleTrackerChange()
    {
        _isFillOn = !_isFillOn;
        _boxCollider.enabled = _isFillOn;
        _fill.SetActive(_isFillOn);
    }

    private void Upgrade()
    {
        UpdateBalance(_saleTracker.Track.Price);
        _wallet.UpdateBalance(_summPrice);
        _summPrice = 0;
        WalletChanged?.Invoke();
        _saleTracker.Track.Saling();
        _particleSell.Play();
        SaleTrackerChange();
        PlayerStayChange();
    }
}
