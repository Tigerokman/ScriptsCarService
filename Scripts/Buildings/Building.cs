using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Bag
{
    [SerializeField] protected float BreakeBetweenTake;
    [SerializeField] protected float BreakeBetweenPut;
    [SerializeField] protected float PathTimeTo;
    [SerializeField] protected float PathTimeFrom;
    [SerializeField] protected bool AnimTake;
    [SerializeField] protected bool AnimPut;

    protected Coroutine CurrentTake;
    protected Coroutine CurrentPut;

    protected abstract IEnumerator Take(Bag bag);
    protected abstract IEnumerator PutInOrSell(Bag bag = null);
}
