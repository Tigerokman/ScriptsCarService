using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SaleTrackerProgressView))]
public class SaleTracker : Building
{
    [SerializeField] private List<GameObject> _bagMaterials;
    [SerializeField] private Detail _engine;
    [SerializeField] private Detail _wheel;
    [SerializeField] private Detail _door;
    [SerializeField] private Detail _material;
    [SerializeField] private GameObject _trackToCreate;
    [SerializeField] private float _pathTimeSetPlus;
    [SerializeField] private GameObject _parent;

    private SaleTrackerProgressView _progressView;
    private Track _track;
    private bool _headComplete = false;
    private bool _bodyComplete = false;
    private bool _canSale = false;
    private bool _engineAdded = false;
    private int _wheelsComplete = 0;
    private int _doorsComplete = 0;
    private int _allDoors = 2;
    private int _allWheels = 4; //4
    private int _materialToHead = 5; //5
    private int _materialToBody = 25; //25
    private int _currentMaterialInColoumn = 0;
    private int _currentColoumn = 0;

    private bool _allComplete => _headComplete && _bodyComplete && _wheelsComplete == _allWheels && _doorsComplete == _allDoors;

    public Track Track => _track;
    public event UnityAction SaleTrackerOn;
    public event UnityAction<Detail> DetailGet;

    private void Start()
    {
        _progressView = GetComponent<SaleTrackerProgressView>();
        _progressView.Instantiate(_allDoors, _allWheels, _materialToHead, _materialToBody, this);
        InstantiateTrack();
    }

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
            if (CurrentTake == null && bag.CountDetail > 0 && FreePlace )
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

            if (_allComplete && _canSale == false)
                CanSale();

            CurrentTake = null;
        }
    }

    protected override IEnumerator Take(Bag bag)
    {
        Detail current = null;

        if (_bodyComplete == false || _headComplete == false)
        {
            while (bag.CountDetail > 0 && bag.LastDetail.NameID == _material.NameID && FreePlace)
            {
                int maxMaterialInColoumn = 5;
                current = bag.LastDetail;
                DetailGet?.Invoke(current);
                AddDetail(current);
                current.SetMoveInBag(_bagMaterials[_currentColoumn], CurrentPositionY, PathTimeFrom, AnimTake);
                _currentMaterialInColoumn++;
                UpdateBagPosition(current.SizeDetail);

                if (_currentMaterialInColoumn == maxMaterialInColoumn)
                {
                    _currentColoumn++;
                    UpdateBagPosition(-(current.SizeDetail * maxMaterialInColoumn));
                    _currentMaterialInColoumn = 0;
                }

                bag.UpdateBagPosition(-current.SizeDetail);
                bag.RemoveDetail();
                yield return new WaitForSeconds(BreakeBetweenTake);
            }

            if(bag.CountDetail > 0 && bag.LastDetail.NameID == _engine.NameID && _engineAdded == false)
            {      
                current = bag.LastDetail;
                DetailGet?.Invoke(current);
                current.SetMoveInBag(CurrentBag, 0, PathTimeFrom, AnimTake);
                bag.UpdateBagPosition(-current.SizeDetail);
                bag.RemoveDetail();
                _engineAdded = true;
                yield return new WaitForSeconds(BreakeBetweenTake);
            }

            CheckBodyAndHead(current);
        }

        if (_wheelsComplete < _allWheels || _doorsComplete < _allDoors)
        {
            while (bag.CountDetail > 0 && bag.LastDetail.NameID == _wheel.NameID && _wheelsComplete < _allWheels)
            {
                current = bag.LastDetail;
                DetailGet?.Invoke(current);
                current.SetMoveInTrack(_track.WheelsPoints[_wheelsComplete], PathTimeTo + _pathTimeSetPlus, _wheelsComplete);
                current.DetailSet += SetDetail;
                _wheelsComplete++;
                bag.UpdateBagPosition(-current.SizeDetail);
                bag.RemoveDetail();
                yield return new WaitForSeconds(BreakeBetweenTake + 1f);
            }

            while (bag.CountDetail > 0 && bag.LastDetail.NameID == _door.NameID && _doorsComplete < _allDoors && _headComplete)
            {
                current = bag.LastDetail;
                DetailGet?.Invoke(current);
                current.SetMoveInTrack(_track.DoorsPoints[_doorsComplete], PathTimeTo + _pathTimeSetPlus, _doorsComplete);
                current.DetailSet += SetDetail;
                _doorsComplete++;
                bag.UpdateBagPosition(-current.SizeDetail);
                bag.RemoveDetail();
                yield return new WaitForSeconds(BreakeBetweenTake + 1f);
            }
        }

        if (_allComplete && _canSale == false)
            CanSale();

        CurrentTake = null;
    }

    public void ResetTrack()
    {
        _track.TrackSaled -= ResetTrack;
        InstantiateTrack();
        _doorsComplete = 0;
        _wheelsComplete = 0;
        _engineAdded = false;
        _canSale = false;
        _bodyComplete = false;
        _headComplete = false;
        _progressView.ResetProgress();
    }

    private void CanSale()
    {
        SaleTrackerOn?.Invoke();
        _canSale = true;
    }

    private void InstantiateTrack()
    {
        GameObject temp = Instantiate(_trackToCreate, transform);
        temp.TryGetComponent<Track>(out Track track);
        _track = track;
        temp.transform.parent = _parent.transform;
        _track.TrackSaled += ResetTrack;
        UpgradeBag(_materialToBody + _materialToHead);
    }

    private void SetDetail(int nameID, int number, Detail detail)
    {
        if (nameID == _wheel.NameID)
        {
            _track.WheelOn(number);
            detail.DetailSet -= SetDetail;
        }
        else if (nameID == _door.NameID)
        {
            _track.DoorOn(number);
            detail.DetailSet -= SetDetail;
        }
    }

    private void CheckBodyAndHead(Detail detail)
    {

        if (CountDetail >= _materialToHead && _engineAdded == true && _headComplete == false)
        {
            DestroyChild();

            for(int i = 0; i < _materialToHead; i++)
            {
                CheckColoumn(detail);

                DestroyDetail();
                UpdateBagPosition(-detail.SizeDetail);
                _currentMaterialInColoumn--;

            }

            UpgradeBag(-_materialToHead);
            _track.HeadOn();
            _headComplete = true;
        }

        if (CountDetail >= _materialToBody && _bodyComplete == false)
        {
            for (int i = 0; i < _materialToBody; i++)
            {
                CheckColoumn(detail);

                DestroyDetail();
                UpdateBagPosition(-detail.SizeDetail);
                _currentMaterialInColoumn--;
            }

            UpgradeBag(-_materialToBody);
            _track.BodyOn();
            _bodyComplete = true;
        }
    }

    private void CheckColoumn(Detail detail)
    {
        int maxMaterialInColoumn = 5;

        if (_currentMaterialInColoumn == 0)
        {
            UpdateBagPosition(detail.SizeDetail * maxMaterialInColoumn);
            _currentMaterialInColoumn = maxMaterialInColoumn;
            _currentColoumn--;
        }
    }

    protected override IEnumerator PutInOrSell(Bag bag = null)
    {
        throw new System.NotImplementedException();
    }
}
