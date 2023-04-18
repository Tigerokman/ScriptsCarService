using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateBuilding : MonoBehaviour
{
    [SerializeField] private Transform _createPoint;
    [SerializeField] private int _buildingPrice;
    [SerializeField] private GameObject _building;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _buildingName;
    [SerializeField] private AudioSource _buildingCreateSound;

    private int _waitingToCreate = 2;
    private Coroutine _fillIn;
    private float _time = 0;

    private void Start()
    {
        _building.TryGetComponent<BuildingText>(out BuildingText building);
        _buildingName.TryGetComponent<TranslatableText>(out TranslatableText text);
        text.SetId(building.NameID);
        _fill.fillAmount = 0;
        _price.text = "$" + _buildingPrice;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            if (wallet.Money >= _buildingPrice)
            {
                _fillIn = StartCoroutine(FillProgress(wallet));
            }

        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            if (wallet.Money >= _buildingPrice && _fillIn == null)
            {
                _fillIn = StartCoroutine(FillProgress(wallet));
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet) && _fillIn != null)
        {
            StopCoroutine(_fillIn);
            _time = 0;
            _fill.fillAmount = 0;
            _fillIn = null;
        }
    }

    private IEnumerator FillProgress(PlayerWallet wallet)
    {
        while (_time < _waitingToCreate)
        {
            _time += Time.deltaTime;
            _fill.fillAmount = _time / _waitingToCreate;
            Mathf.Clamp(_fill.fillAmount, 0, 1);

            yield return null;
        }

        wallet.UpdateBalance(-_buildingPrice);
        GameObject temp = Instantiate(_building, _createPoint);
        temp.transform.parent = null;
        _buildingCreateSound.Play();
        Destroy(gameObject);
    }
}
