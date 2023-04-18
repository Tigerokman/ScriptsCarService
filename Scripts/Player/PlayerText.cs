using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerBag))]
public class PlayerText : MonoBehaviour
{
    [SerializeField] private GameObject _maxText;
    [SerializeField] private GameObject _hardText;

    private PlayerBag _bag;

    private void Awake()
    {
        _bag = GetComponent<PlayerBag>();
    }

    private void OnEnable()
    {
        _bag.DetailCountChange += HasStrenghtText;
    }

    private void OnDisable()
    {
        _bag.DetailCountChange -= HasStrenghtText;
    }

    private void HasStrenghtText()
    {
        _maxText.SetActive(!_bag.FreePlace);

        //if (_bag.FreePlace == true)
        //{
        //    _hardText.SetActive(!_bag.HasStrenght);
        //}
        //else
        //{
        //    _hardText.SetActive(false);
        //}
    }
}
