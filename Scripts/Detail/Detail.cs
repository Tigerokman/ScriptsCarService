using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Detail : MonoBehaviour
{
    [SerializeField] private GameObject _topPointY;
    [SerializeField] private int _nameID;
    [SerializeField] private int _price;

    private Animator _animator;
    private float _pathTimeTo;
    private float _sizeDetailY;
    private Vector3 _position;
    private GameObject _bagPosition;
    private float _duration = 0;

    public event UnityAction<int, int, Detail> DetailSet;

    public float SizeDetail => _sizeDetailY;
    public int NameID => _nameID;
    public int Price => _price;

    private void Awake()
    {
        _sizeDetailY = _topPointY.transform.position.y - transform.position.y;
        _animator = GetComponent<Animator>();
    }

    public void SetMoveInBag(GameObject bagPosition, float positionY, float pathTimeTo, bool anim, bool recycling = false)
    {

        if (recycling == true)
        {
            transform.position = new Vector3(_bagPosition.transform.position.x, _bagPosition.transform.position.y, _bagPosition.transform.position.z);
            positionY = 0;
        }

        _position = new Vector3(0, positionY, 0);
        _pathTimeTo = pathTimeTo;
        _bagPosition = bagPosition;

        StartCoroutine(MoveTo(anim, recycling));
    }

    public void SetMoveInTrack(GameObject pointPosition, float pathTimeTo, int number)
    {
        _pathTimeTo = pathTimeTo;
        _bagPosition = pointPosition;

        StartCoroutine(SetDetailToTrack(number));
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private IEnumerator SetDetailToTrack(int number)
    {
        transform.parent = null;
        _duration = 0;

        Vector3 tempPosition = transform.position;
        Vector3 tempAngels = transform.localEulerAngles;

        while (_duration < _pathTimeTo)
        {
            _duration += Time.deltaTime;
            transform.position = Vector3.Lerp(tempPosition, _bagPosition.transform.position, _duration / _pathTimeTo);
            transform.localEulerAngles = Vector3.Lerp(tempAngels, _bagPosition.transform.eulerAngles, _duration / _pathTimeTo);
            yield return null;
        }

        DetailSet.Invoke(NameID, number, this);
        DestroyThis();
    }

    private IEnumerator MoveTo(bool anim, bool recycling)
    {
        transform.parent = null;
        _duration = 0;
        Vector3 temp = transform.position;

        if (anim == true)
        {
            string jump = "Jump";
            _animator.SetTrigger(jump);
        }

        while (_duration < _pathTimeTo)
        {
            _duration += Time.deltaTime;
            transform.position = Vector3.Lerp(temp, _bagPosition.transform.position + _position, _duration / _pathTimeTo);

            yield return null;
        }

        if (recycling == true)
            DestroyThis();


        transform.parent = _bagPosition.transform;
        transform.localPosition = _position;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
