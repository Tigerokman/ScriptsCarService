using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private GameObject _bag;
    [SerializeField] private int _maxBagSize;

    protected Stack<Detail> Details = new Stack<Detail>();

    private float _currentPositionY = 0;
    private int _upgradeBagSize = 0;

    public int MaxBagSize => _maxBagSize + _upgradeBagSize;
    public int CountDetail => Details.Count;
    public bool FreePlace => MaxBagSize > CountDetail;
    public Detail LastDetail => Details.Peek();
    public GameObject CurrentBag => _bag;
    public float CurrentPositionY => _currentPositionY;
    public event UnityAction BagUp;


    public void UpdateBagPosition(float positionY)
    {
        _currentPositionY += positionY;
    }

    public virtual void AddDetail(Detail detail)
    {
        Details.Push(detail);
    }

    public virtual void RemoveDetail()
    {
        Details.Pop();
    }

    public void DestroyDetail()
    {
        Detail temp = Details.Pop();
        temp.DestroyThis();
    }

    public void DestroyChild()
    {
        _bag.transform.GetChild(_bag.transform.childCount - 1).TryGetComponent<Detail>(out Detail temp);
        temp.DestroyThis();
    }

    public virtual void UpgradeBag(int upgrade)
    {
        _upgradeBagSize += upgrade;
        BagUp?.Invoke();
    }
}
