using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecyclingPut : Building
{
    [SerializeField] private GameObject _createObject;
    [SerializeField] private GameObject _recyclingPoint;
    [SerializeField] private RecyclingTake _recyclingTake;

    public GameObject CreateObject => _createObject;
    public event UnityAction BagChanged;

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag))
        {
            CurrentPut = StartCoroutine(PutInOrSell(bag));
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag))
        {
            if(CurrentPut == null && CountDetail > 0 && bag.FreePlace)
            {
                CurrentPut = StartCoroutine(PutInOrSell(bag));
            }
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

    public void CreateDetail()
    {
        CurrentBag.TryGetComponent<Bag>(out Bag buildingBag);
        StartCoroutine(Take(buildingBag));
    }

    protected override IEnumerator PutInOrSell(Bag bag = null)
    {
        Detail current;

        _recyclingTake.StartPut();

        while (Details.Count > 0 && bag.FreePlace)
        {
            current = Details.Pop();
            bag.AddDetail(current);
            current.SetMoveInBag(bag.CurrentBag, bag.CurrentPositionY, PathTimeTo, AnimPut);
            bag.UpdateBagPosition(current.SizeDetail);
            UpdateBagPosition(-current.SizeDetail);
            BagChanged?.Invoke();

            yield return new WaitForSeconds(BreakeBetweenPut);
        }

        CurrentPut = null;
    }

    protected override IEnumerator Take(Bag bag)
    {
        GameObject temp;

        temp = Instantiate(_createObject, _recyclingPoint.transform);
        temp.TryGetComponent<Detail>(out Detail current);
        current.SetMoveInBag(CurrentBag, CurrentPositionY, PathTimeFrom, AnimTake);
        AddDetail(current);
        UpdateBagPosition(current.SizeDetail);
        BagChanged?.Invoke();
        yield return null;
    }
}
