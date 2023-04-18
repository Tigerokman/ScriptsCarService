using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Building
{
    [SerializeField] private BuildingTextTrash _textTrash;

    private bool _isTrade = false;

    public float FillBreake => BreakeBetweenTake;
    public bool IsTrade => _isTrade;

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag))
        {
            CurrentTake = StartCoroutine(Take(bag));
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag) && CurrentTake != null)
        {
            StopCoroutine(CurrentTake);
            _isTrade = false;
        }
    }

    protected override IEnumerator Take(Bag bag)
    {
        Detail current;

        if (bag.CountDetail != 0 && CurrentPut == null)
            CurrentPut = StartCoroutine(PutInOrSell());

        while (bag.CountDetail > 0 && MaxBagSize > CountDetail)
        {
            if (_isTrade == false)
            {
                _isTrade = true;
                _textTrash.StartFill();
            }

            yield return new WaitForSeconds(BreakeBetweenTake);

            current = bag.LastDetail;
            AddDetail(current);
            current.SetMoveInBag(CurrentBag, CurrentPositionY, PathTimeFrom, AnimTake);
            UpdateBagPosition(current.SizeDetail);
            bag.UpdateBagPosition(-current.SizeDetail);
            bag.RemoveDetail();
        }

        _isTrade = false;
    }

    protected override IEnumerator PutInOrSell(Bag bag = null)
    {
        yield return new WaitForSeconds(BreakeBetweenPut);

        while (Details.Count > 0)
        {
            Detail temp = Details.Pop();
            UpdateBagPosition(-temp.SizeDetail);
            temp.DestroyThis();
            yield return new WaitForSeconds(BreakeBetweenPut);
        }

        CurrentPut = null;
    }
}
