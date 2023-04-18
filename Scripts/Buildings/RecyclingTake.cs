using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecyclingTake : Building
{
    [SerializeField] private Detail _takeDetail;
    [SerializeField] private GameObject _recyclingPoint;
    [SerializeField] private RecyclingPut _recyclingPut;
    [SerializeField] private float _delayBetweenCreate;

    private bool _recycling = true;
    public Detail TakeDetail => _takeDetail;

    public event UnityAction BagChanged;


    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag))
        {
            CurrentTake = StartCoroutine(Take(bag));
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag))
        {
            if (CurrentTake == null && bag.CountDetail > 0 && FreePlace && bag.LastDetail.NameID == _takeDetail.NameID)
            {
                CurrentTake = StartCoroutine(Take(bag));
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag) && CurrentTake != null)
        {
            StopCoroutine(CurrentTake);
            CurrentTake = null;
        }
    }

    public override void UpgradeBag(int upgrade)
    {
        base.UpgradeBag(upgrade);
        StartPut();
    }

    public void StartPut()
    {
        if(CurrentPut == null)
            CurrentPut = StartCoroutine(PutInOrSell());
    }

    protected override IEnumerator PutInOrSell(Bag bag = null)
    {
        yield return new WaitForSeconds(BreakeBetweenPut);

        while (Details.Count > 0 && _recyclingPut.FreePlace)
        {
            Detail temp = Details.Pop();
            UpdateBagPosition(-temp.SizeDetail);
            temp.SetMoveInBag(_recyclingPoint,CurrentPositionY,PathTimeTo, AnimPut, _recycling);
            BagChanged?.Invoke();
            yield return new WaitForSeconds(PathTimeTo + _delayBetweenCreate);
            _recyclingPut.CreateDetail();
            yield return new WaitForSeconds(BreakeBetweenPut);
        }

        CurrentPut = null;
    }

    protected override IEnumerator Take(Bag bag)
    {
        Detail current;

        if (bag.CountDetail != 0 && CurrentPut == null && bag.LastDetail.NameID == _takeDetail.NameID)
            CurrentPut = StartCoroutine(PutInOrSell());

        while (bag.CountDetail > 0 && FreePlace && bag.LastDetail.NameID == _takeDetail.NameID)
        {
            yield return new WaitForSeconds(BreakeBetweenTake);
            current = bag.LastDetail;
            AddDetail(current);
            current.SetMoveInBag(CurrentBag, CurrentPositionY, PathTimeFrom, AnimTake);
            UpdateBagPosition(current.SizeDetail);
            bag.UpdateBagPosition(-current.SizeDetail);
            bag.RemoveDetail();
            BagChanged?.Invoke();
        }

        CurrentTake = null;
    }
}
