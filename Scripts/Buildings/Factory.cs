using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    [SerializeField] private GameObject _createObject;
    [SerializeField] private GameObject _createPoint;
    [SerializeField] protected float StartBagY;
    [SerializeField] private UpgradeFactory _upgradeFactory;

    public GameObject CreateObject => _createObject;

    private void Start()
    {
        CheckWork();
        UpdateBagPosition(StartBagY);
    }

    private void OnEnable()
    {
        _upgradeFactory.UpgradeUp += CheckWork;
    }

    private void OnDisable()
    {
        _upgradeFactory.UpgradeUp -= CheckWork;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag))
        {
            CurrentPut = StartCoroutine(PutInOrSell(bag));
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag) && CurrentPut == null)
        {
            CurrentPut = StartCoroutine(PutInOrSell(bag));
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag) && CurrentPut != null)
        {
            StopCoroutine(CurrentPut);
            CurrentPut = null;
        }
    }

    protected override IEnumerator PutInOrSell(Bag bag)
    {
        Detail current;

        while (Details.Count > 0 && bag.FreePlace)
        {
            current = Details.Pop();
            bag.AddDetail(current);
            current.SetMoveInBag(bag.CurrentBag, bag.CurrentPositionY, PathTimeTo, AnimPut);
            bag.UpdateBagPosition(current.SizeDetail);
            UpdateBagPosition(-current.SizeDetail);

            CheckWork();

            yield return new WaitForSeconds(BreakeBetweenPut);
        }

        CurrentPut = null;
    }

    protected override IEnumerator Take(Bag bag)
    {

        GameObject temp;
        float tempY;

        while (FreePlace)
        {
            temp = Instantiate(_createObject, _createPoint.transform);
            temp.TryGetComponent<Detail>(out Detail current);
            current.SetMoveInBag(CurrentBag, 0, PathTimeFrom - _upgradeFactory.CurrentSpeedIncrease, AnimTake);
            yield return new WaitForSeconds(BreakeBetweenTake - _upgradeFactory.CurrentSpeedIncrease);
            tempY = current.SizeDetail;
            AddDetail(current);
            current.SetMoveInBag(CurrentBag, CurrentPositionY, 0, AnimTake);
            UpdateBagPosition(tempY);
        }

        CurrentTake = null;
    }

    private void CheckWork()
    {
        if (CurrentTake == null)
        {
            CurrentBag.TryGetComponent<Bag>(out Bag buildingBag);
            CurrentTake = StartCoroutine(Take(buildingBag));
        }
    }
}
