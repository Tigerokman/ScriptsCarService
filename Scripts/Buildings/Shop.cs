using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Building
{
    [SerializeField] private Detail _sellDetail;
    [SerializeField] private ParticleSystem _particleSell;
    [SerializeField] private BuildingTextShop _textShop;

    private bool _isTrade = false;
    private PlayerWallet _wallet;

    public Detail SellDetail => _sellDetail;
    public float FillBreake => BreakeBetweenTake;
    public bool IsTrade => _isTrade;

    private void OnTriggerEnter(Collider player)
    {
        if (player.TryGetComponent<Bag>(out Bag bag) && player.TryGetComponent<PlayerWallet>(out PlayerWallet wallet))
        {
            _wallet = wallet;
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

        if(bag.CountDetail != 0 && CurrentPut == null && bag.LastDetail.NameID == _sellDetail.NameID)
            CurrentPut = StartCoroutine(PutInOrSell());

        while (bag.CountDetail > 0 && MaxBagSize > CountDetail && bag.LastDetail.NameID == _sellDetail.NameID)
        {
            if (_isTrade == false)
            {
                _isTrade = true;
                _textShop.StartFill();
            }

            yield return new WaitForSeconds(BreakeBetweenTake);

            current = bag.LastDetail;
            AddDetail(current);
            current.SetMoveInBag(CurrentBag,CurrentPositionY, PathTimeFrom, AnimTake);
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
            _wallet.UpdateBalance(temp.Price);
            temp.DestroyThis();
            _particleSell.Play();
            yield return new WaitForSeconds(BreakeBetweenPut);
        }

        CurrentPut = null;
    }
}
