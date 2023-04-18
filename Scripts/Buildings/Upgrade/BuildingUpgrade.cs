using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BuildingUpgrade : MonoBehaviour
{
    [SerializeField] private int _startPrice;
    [SerializeField] private int _increasePrice;
    [SerializeField] protected BuildingText BuildingText;

    protected int _currentPrice;
    protected float CurrentTime;

    private PlayerWallet _wallet;

    private int _currentPriceIncrease;
    private bool _canBuy = false;
    private bool _playerStay = false;

    public bool PlayerStay => _playerStay;
    public bool CanBuy => _canBuy;
    public int CurrentPrice => _currentPrice;
    public event UnityAction UpgradeUp;

    private void Awake()
    {
        _currentPrice = _startPrice;
        _currentPriceIncrease = _increasePrice;
    }

    private void OnEnable()
    {
        BuildingText.FillEnd += Upgrade;
    }

    private void OnDisable()
    {
        BuildingText.FillEnd -= Upgrade;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            PlayerEnter();

            if (wallet.Money >= CurrentPrice)
            {
                CanBayChange();
                _wallet = wallet;
                BuildingText.StartFill();
            }

        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            if (wallet.Money >= CurrentPrice && BuildingText.IsFilling == false)
            {
                Debug.Log("Работаю");
                CanBayChange();
                BuildingText.StartFill();
            }
            else
            {
                Debug.Log("Не работаю");
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            PlayerExit();
        }
    }

    protected virtual void Upgrade()
    {
        _wallet.UpdateBalance(-CurrentPrice);
        PriceIncrease();
        CanBayChange();
    }

    protected void PlayerEnter()
    {
        _playerStay = true;
        _canBuy = false;
    }

    protected void PlayerExit()
    {
        _playerStay = false;
        _canBuy = false;
    }

    protected void CanBayChange()
    {
        _canBuy = !_canBuy;
    }

    protected void PriceIncreaseDuplicate()
    {
        _currentPriceIncrease *= 2;
    }

    private void PriceIncrease()
    {
        _currentPrice += _currentPriceIncrease;
        Debug.Log(_currentPrice);
        UpgradeUp?.Invoke();
    }
}
