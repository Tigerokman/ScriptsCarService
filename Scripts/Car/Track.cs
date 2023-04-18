using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Track : MonoBehaviour
{
    [SerializeField] private List<GameObject> _wheels;
    [SerializeField] private List<GameObject> _wheelsPoints;
    [SerializeField] private List<GameObject> _doors;
    [SerializeField] private List<GameObject> _doorsPoints;
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _body;
    [SerializeField] private int _pricePlusForComplete;

    private Animator _animator;
    public int Price => _pricePlusForComplete;
    public List<GameObject> WheelsPoints => _wheelsPoints;
    public List<GameObject> DoorsPoints => _doorsPoints;
    public event UnityAction TrackSaled;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void HeadOn()
    {
        _head.SetActive(true);
    }

    public void BodyOn()
    {
        _body.SetActive(true);
    }

    public void DoorOn(int number)
    {
        _doors[number].SetActive(true);
    }

    public void WheelOn(int number)
    {
        _wheels[number].SetActive(true);
    }

    public void Saling()
    {
        string destroy = "Destroy";
        _animator.SetTrigger(destroy);
    }

    public void Saled()
    {
        TrackSaled?.Invoke();
        Destroy(gameObject);
    }


}
